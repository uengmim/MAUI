using AdminScreen.ViewModel;
using CommunityToolkit.Maui.Alerts;

using SmartLock.TT;
using SmartLock.TT.Common;

namespace AdminScreen.Views
{
    public partial class LockInitPage : ContentPage
    {
        private string _lockData = "";
        private string _lockName = "";
        private string _lockMac = "";
        int count = 0;
        public string LockData
        {
            get => _lockData;
            set
            {
                _lockData = value;
                OnPropertyChanged(nameof(LockData));
            }
        }
        public string LockName
        {
            get => _lockName;
            set
            {
                _lockName = value;
                OnPropertyChanged(nameof(LockName));
            }
        }
        public string LockMacData
        {
            get => _lockMac;
            set
            {
                _lockMac = value;
                OnPropertyChanged(nameof(LockMacData));
            }
        }

        /// <summary>
        /// TTLock API Helper
        /// </summary>
        private TTlockHelper ttlockHelper;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="Data">Lock 데이터</param>
        /// <param name="Name">Lock 이름</param>
        /// <param name="LockMac">Lock Mac</param>
        public LockInitPage(string Data, string Name, string LockMac)
        {
            InitializeComponent();

            //Preferences.Default.Set<string>("INIT_LOCKDATA", "테스트 LOCK DATA");
            //Preferences.Default.Set<string>("INIT_MACDATA", "테스트 MAC DATA");

            LockName = Name;
            LockData = Data;
            LockMacData = LockMac;
            LockInitGoViewModel lockInitGoViewModel = new LockInitGoViewModel();
            lockInitGoViewModel.LockData = Data;
            lockInitGoViewModel.LockName = Name;
            this.BindingContext = lockInitGoViewModel;

            //TTLock Helper
            ttlockHelper = new TTlockHelper();
            //Reset 결과 이벤트
            ttlockHelper.LockResetResultEvent += TtlockHelper_LockResetResultEvent;
        }


        /// <summary>
        /// Reset 결과 이벤트
        /// </summary>
        /// <param name="e"></param>
        private void TtlockHelper_LockResetResultEvent(SmartLock.Event.LockResetResultEventArgs e)
        {
            //오류 발생
            if (!e.IsSuccess)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage.DisplayAlert("초기화 오류", e.Error.ErrorMessage, "OK");
                });

                return;
            }

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current.MainPage.DisplayAlert("확인", "Lock 초기화를 하였습니다.", "OK");

                //버튼 텍스트 변경
                SetCounterBtnText("자물쇠가 초기화 되었습니다.");
            });
        }

        /// <summary>
        /// Counter Button 텍스트 셋팅
        /// </summary>
        /// <param name="Msg"></param>
        public void SetCounterBtnText(string Msg)
        {
            CounterBtn.Text = Msg;
            CounterBtn.IsEnabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCounterClicked(object sender, EventArgs e)
        {
            try
            {
                count++;

                if (count >= 1)
                    CounterBtn.Text = $"현재 {count} 번 클릭하셨습니다.";

                if (count == 10)
                {
                    //Toast 메시지
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    var initLockData = Preferences.Default.Get<string>("INIT_LOCKDATA", string.Empty);
                    var initMacData = Preferences.Default.Get<string>("INIT_MACDATA", string.Empty);

                    if(string.IsNullOrEmpty(initLockData) || string.IsNullOrEmpty(initMacData))
                    {
                        var toastInit = Toast.Make($"자물쇠의 LOCKDATA 또는 MAC정보가 없습니다.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        toastInit.Show(cancellationTokenSource.Token);

                        return;
                    }

                    //TTLOCK RESET 
                    ttlockHelper.LockDeviceReset(new LockDevice{ Address = initMacData }, initLockData);

                    return;
                }

                SemanticScreenReader.Announce(CounterBtn.Text);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
        }
    }
}