using AdminScreen.Models;
using AdminScreen.Views;
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


        /// <summary>
        /// TTLOCK Helper
        /// </summary>
        private TTlockHelper ttlockHelper;


        /// <summary>
        /// 생성자
        /// </summary>
        public LockRegistInputViewModel()
        {
            ttlockHelper = new TTlockHelper();
            //Lock 초기화 Event
            ttlockHelper.LockInitResultEvent += TlockHelper_LockInitResultEvent;
        }

        private async void TlockHelper_LockInitResultEvent(SmartLock.Event.LockInitResultEventArgs e)
        {
            if(!e.IsSuccess)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage.DisplayAlert("오류", e.Error.ErrorMessage, "OK");
                });

                return;
            }

            try
            {
                Preferences.Default.Set("INIT_LOCKDATA", e.LockData);
                Preferences.Default.Set("INIT_MACDATA", LockInfo.LockMac);

                var dataService = ImateHelper.GetSingleTone();

                var result = dataService.Ttlock.LockInitialize(new XNSC.Net.Ttlock.TtlockInfo()
                {
                    lockName = LockInfo.LockName,
                    lockMacAddr = LockInfo.LockMac,
                    lockAka = Locknm, //LOCK 별명 관리 이름
                    lockData = e.LockData
                });

                await Application.Current.MainPage.DisplayAlert("알림", "자물쇠 등록이 완료되었습니다.", "확인");
                await Application.Current.MainPage.Navigation.PushAsync(new LockRegistPage());

                //MainThread.InvokeOnMainThreadAsync(() =>
                //{
                //    Application.Current.MainPage.DisplayAlert("알림", "자물쇠 등록이 완료되었습니다.", "확인");
                //    Application.Current.MainPage.Navigation.PushAsync(Application.Current.MainPage);
                //});
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");

                //MainThread.InvokeOnMainThreadAsync(() =>
                //{
                //    Application.Current.MainPage.DisplayAlert("오류", e.Message, "OK");
                //});

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
                if (Locknm == null || Locknm == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "자물쇠 이름을 입력해주세요.", "확인");
                    return;
                }

                //if (LockNumber == null || LockNumber == "")
                //{
                //    await Application.Current.MainPage.DisplayAlert("알림", "자물쇠 자산 관리번호를 입력해주세요.", "확인");
                //    return;
                //}
                ttlockHelper.InitLockDevice(new SmartLock.TT.Common.LockDevice { Address = lockinfo.LockMac, Name = lockinfo.LockName });
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
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
    }
}