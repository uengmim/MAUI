using WorkerScreen.Common;
using WorkerScreen.Models;
using WorkerScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShreDoc.ProxyModel;
using XNSC.DD.EX;
using XNSC.Net;
using WorkerScreen.Utils;
using Camera.MAUI.Test;
using XNSC.Net.NOKE;
using System.Collections.ObjectModel;
using Google.Protobuf.WellKnownTypes;

namespace WorkerScreen.ViewModel
{
    public class QRRecogViewModel : BaseViewModel
    {
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        public ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";
        private string _confno = "";

        private LockInfom selectedItem;

        public LockInfom SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    if (SelectedItem != null)
                    {
                        PerformNavigation(SelectedItem.LockData, SelectedItem.LockName);
                    }
                }
            }
        }

        private async void PerformNavigation(string Data, string Name)
        {
            if (SelectedItem != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LockClosePage(BoxNum, Location, Data, Name, CONFNO));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("오류", "선택된 자물쇠가 없습니다.", "OK");
                SelectedItem = null;
                return;
            }
        }
        public string Name => LoginInfo.Name;

        #region Properties

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
        /// 현재 로그인한 사용자 정보입니다.
        /// </summary>
        public LoginInfo LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                OnPropertyChanged();
            }
        }
        private LoginInfo loginInfo;

        public LockInfom LockInfom
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged();
            }
        }
        private LockInfom lockinfo;

        public string BoxNum
        {
            get => _boxNum;
            set
            {
                _boxNum = value;
                OnPropertyChanged(nameof(BoxNum));
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
        public string LockName
        {
            get => _lockName;
            set
            {
                _lockName = value;
                OnPropertyChanged(nameof(LockName));
            }
        }
        public string CONFNO
        {
            get => _confno;
            set
            {
                _confno = value;
                OnPropertyChanged(nameof(CONFNO));
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
                OnPropertyChanged();
            }
        }

        private bool isSaveNumber = false;

        #endregion

        #region Commands
        public async Task ExecuteMyCommand()
        {
            try
            {
                LockDataModel = new ObservableCollection<LockInfom>();
                var dataService = ImateHelper.GetSingleTone();

                var lkCode = BoxNum;

                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
    {
                    new DIMWhereFieldCondition{ fieldName = "ILSID" , value = lkCode, condition = DIMWhereCondition.Equal}
    }
                };

                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (lkMstData.Count > 0)
                {
                    foreach (var item in lkMstData)
                    {
                        LockDataModel.Add(new LockInfom(item.LSN, item.LKNM));
                        //lockinfo.Add(new LockInfom(item.LSN, item.LKNM));
                    }
                    return;
                }
                if (lkMstData.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("오류", "검색된 자물쇠가 없습니다.", "OK");

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }

        }
        #endregion

        #region QRRecogPage
        public QRRecogViewModel()
        {
            this.LoginInfo = new LoginInfo();
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockInfom");

            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                this.IsSaveNumber = true;
                this.LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                this.LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");

            }

        }
        #endregion

    }
}