using AdminScreen.Models;
using AdminScreen.Views;
using AdminScreen.Model;
using ShreDoc.Utils;
using SmartLock.TT;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Command = Microsoft.Maui.Controls.Command;
using SmartLock.TT.Common;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// Lock 등록 모델
    /// </summary>
    public class LockRegistInputViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// lock 데이터 모델
        /// </summary>
        private ObservableCollection<LockInfomation> lockDataModel = new ObservableCollection<LockInfomation>();

        /// <summary>
        /// Mac 주소
        /// </summary>
        private string _mac = "";
        private string _lockname = "";

        /// <summary>
        /// Lock 정보
        /// </summary>
        private LockInfomation lockinfo;

        /// <summary>
        /// Lock 번호
        /// </summary>
        public string LockNumber { get; set; } = "";

        /// <summary>
        /// Lock 이름
        /// </summary>
        public string Locknm { get; set; } = "";

        /// <summary>
        /// isLoading
        /// </summary>
        private bool isLoading;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        /// <summary>
        /// Lock Data 모델 
        /// </summary>
        public ObservableCollection<LockInfomation> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        /// <summary>
        /// Lock Info
        /// </summary>
        public LockInfomation LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfomation));
            }
        }
        /// <summary>
        /// 로그인한 사용자 정보입니다.
        /// </summary>
        public LoginInfo LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                OnPropertyChanged(nameof(LoginInfo));
            }
        }
        private LoginInfo loginInfo;
        /// <summary>
        /// Mac Address
        /// </summary>
        public string Mac
        {
            get => _mac;
            set
            {
                _mac = value;
                OnPropertyChanged(nameof(Mac));
            }
        }

        public string LockName
        {
            get => _lockname;
            set
            {
                _lockname = value;
                OnPropertyChanged(nameof(LockName));
            }
        }

        /// <summary>
        /// TTLOCK Helper
        /// </summary>
        private TTlockHelper ttlockHelper;

        // 실행 중인지 여부를 나타내는 플래그 추가
        private bool _isBusy = false;

        /// <summary>
        /// 생성자
        /// </summary>
        public LockRegistInputViewModel()
        {
            LoginInfo = new LoginInfo();

            ttlockHelper = new TTlockHelper();
            //Lock 초기화 Event
            ttlockHelper.LockInitResultEvent += TlockHelper_LockInitResultEvent;
        }

        private async void TlockHelper_LockInitResultEvent(SmartLock.Event.LockInitResultEventArgs e)
        {
            if(!e.IsSuccess)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                   await ShowCustomAlert("알림", e.Error.ErrorMessage, "확인", "");
                });

                return;
            }

            try
            {
                Preferences.Default.Set("INIT_LOCKDATA", e.LockData);
                Preferences.Default.Set("INIT_MACDATA", LockInfo.LockMac);

                // 메시지 표시
                bool answer = await ShowCustomAlert("알림", "자물쇠를 등록 하시겠습니까?", "확인", "취소");

                if (!answer)
                {
                    IsLoading = false;
                    return; // 취소 선택 시 함수 종료
                }

                var dataService = ImateHelper.GetSingleTone();

                var result = dataService.Ttlock.LockInitialize(new XNSC.Net.Ttlock.TtlockInfo()
                {
                    lockName = LockInfo.LockName,
                    lockMacAddr = LockInfo.LockMac,
                    lockAka = Locknm, //LOCK 별명 관리 이름
                    lockData = e.LockData
                });

                await ShowCustomAlert("알림", "자물쇠 등록이 완료되었습니다.", "확인", "");

                await Application.Current.MainPage.Navigation.PopModalAsync(animated: false);
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage(LoginInfo));
                IsLoading = false;

            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");

                //
                //초기화 오류시 공장 초기화 상태로 되돌린다.
                //
                ttlockHelper.LockDeviceReset(new LockDevice { Address = LockInfo.LockMac }, e.LockData);
            }
            finally
            {
                try
                {
                    ttlockHelper.StopLockDeviceScan();
                    ttlockHelper.StopBluetoothService();
                }
                catch (Exception ie)
                {
                    Console.WriteLine(ie.ToString());
                }

            }
        }

        #region INotifyPropertyChanged 구현

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand RecognitionCommand => new Command(OnRegistClicked);

        #endregion

        /// <summary>
        /// 등록 버턴 클릭
        /// </summary>
        public async void OnRegistClicked()
        {
            try
            {
                if (_isBusy) // 실행 중인 경우 더 이상 실행하지 않음
                    return;

                _isBusy = true; // 실행 중 플래그 설정

                if (Locknm == null || Locknm == "")
                {
                    await ShowCustomAlert("알림", "자물쇠 이름을 입력해주세요.", "확인", "");
                    return;
                }

                ttlockHelper.InitLockDevice(new SmartLock.TT.Common.LockDevice { Address = lockinfo.LockMac, Name = lockinfo.LockName });

                _isBusy = false; // 실행 완료 후 플래그 해제

            }
            catch (System.Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
                return;
            }
        }

        /// <summary>
        /// 프로퍼티 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 팝업 표시 상태를 나타내는 플래그
        private bool isAlertShowing = false;

        // 팝업
        private async Task<bool> ShowCustomAlert(string title, string message, string accept, string cancel)
        {
            // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
            if (isAlertShowing)
            {
                return false;
            }

            isAlertShowing = true; // 경고 팝업 표시 중임을 표시

            try
            {
                var tcs = new TaskCompletionSource<bool>();

                var alertPage = new CustomAlertPage(title, message, accept, cancel);

                // 확인 버튼 클릭 시 처리
                alertPage.AcceptButtonClicked += (sender, e) =>
                {
                    isAlertShowing = false;
                    tcs.SetResult(true); // true 반환
                };

                // 취소 버튼 클릭 시 처리
                alertPage.CancelButtonClicked += (sender, e) =>
                {
                    isAlertShowing = false;
                    tcs.SetResult(false); // false 반환
                };

                alertPage.Disappearing += (sender, e) => isAlertShowing = false;

                await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);

                return await tcs.Task;
            }
            finally
            {
                isAlertShowing = false; // 팝업 닫힘을 표시
            }
        }
    }
}