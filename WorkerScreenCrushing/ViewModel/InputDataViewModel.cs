using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using System.Windows.Markup;
using System.Collections.Specialized;
using Camera.MAUI.ZXingHelper;
using XNSC.DD.EX;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using WorkerScreenCrushing.Views;
using WorkerScreenCrushing.Models;
using Camera.MAUI;
using ShreDoc.Utils;
using WorkerScreenCrushing.Common;

namespace WorkerScreenCrushing.ViewModel
{

    public class InputDataViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _name = "";
        private string _workerName = "";
        private string _boxName = "";
        private string _location = "";
        private string _lockData = "";
        private DateTime _pickupDate;
        private DateTime _lockDate;
        private string _photoResult = "";

        private bool isLoading;

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
        public string Name => LoginInfo.Name;
        public string WorkerName
        {
            get => _workerName;
            set
            {
                _workerName = value;
                OnPropertyChanged(nameof(WorkerName));
            }
        }
        public string BoxName
        {
            get => _boxName;
            set
            {
                _boxName = value;
                OnPropertyChanged(nameof(BoxName));
            }
        }
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }


        public string LockData
        {
            get => _lockData;
            set
            {
                _lockData = value;
                OnPropertyChanged(nameof(LockData));
            }
        }
        public DateTime PickupDate
        {
            get => _pickupDate;
            set
            {
                _pickupDate = value;
                OnPropertyChanged(nameof(PickupDate));
            }
        }
        public DateTime LockDate
        {
            get => _lockDate;
            set
            {
                _lockDate = value;
                OnPropertyChanged(nameof(LockDate));
            }
        } 
        public DateTime newPickupDate
        {
            get => _lockDate;
            set
            {
                _lockDate = value;
                OnPropertyChanged(nameof(newPickupDate));
            }
        }
        public string PhotoResult
        {
            get => _photoResult;
            set
            {
                _photoResult = value;
                OnPropertyChanged(nameof(PhotoResult));
            }
        }
        public bool IsSaveNumber
        {
            get { return isSaveNumber; }
            set
            {
                isSaveNumber = value;

                if (!value)
                {
                    Preferences.Remove(Constants.SavePhoneNumberKey);
                }

                OnPropertyChanged(nameof(IsSaveNumber));
            }
        }

        private bool isSaveNumber = false;

        public ImageSource SnapShot { get; set; }
        public ImageSource PhotoData { get; set; }

        public string CrushDate { get; set; } = "";

        public string CrushWay { get; set; } = "";

        public string CrushNum { get; set; } = "";


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand InfoRegisterCommand => new Command(InfomRegister);
        #endregion

        // Methods
        #region InfoInput

        public async void InfomRegister()
        {
            try
            {
                IsLoading = true;
                await Application.Current.MainPage.DisplayAlert("알림", $"자물쇠 관리번호({LockData})의 봉인을 확인하였습니다.", "확인");
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage(LoginInfo));
                IsLoading = false;

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;

            }
        }
 
        #endregion

        public InputDataViewModel()
        {

            this.LoginInfo = new LoginInfo();
            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");

            }

        }
    }
}