using AdminScreen.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Command = Microsoft.Maui.Controls.Command;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// Lock 등록 모델
    /// </summary>
    public class LockRegigterModel : INotifyPropertyChanged
    {
        /// <summary>
        /// lock 데이터 모델
        /// </summary>
        private ObservableCollection<LockInfo> lockDataModel = new ObservableCollection<LockInfo>();

        /// <summary>
        /// Mac 주소
        /// </summary>
        private string _mac = "";

        /// <summary>
        /// Lock 정보
        /// </summary>
        private LockInfo lockinfo;

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
        public ObservableCollection<LockInfo> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        /// <summary>
        /// Lock Info
        /// </summary>
        public LockInfo LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
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

                if (LockNumber == null || LockNumber == "")
                {
                    await Application.Current.MainPage.DisplayAlert("알림", "자물쇠 자산 관리번호를 입력해주세요.", "확인");
                    return;
                }

                var intCallback = new InitLockCallback(this);
                TTlockHelper.InitLock(LockInfo.Device, intCallback);

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