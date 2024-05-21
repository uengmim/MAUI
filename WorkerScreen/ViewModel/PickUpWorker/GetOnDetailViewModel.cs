using WorkerScreen.Common;
using WorkerScreen.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ShreDoc.Utils;
using XNSC.DD.EX;
using ShreDoc.ProxyModel;

namespace WorkerScreen.ViewModel.PickUpWorker
{
    /// <summary>
    /// 상차 처리 화면입니다.
    /// </summary>
    public class GetOnDetailViewModel : INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        /// 자물쇠 정보의 모델입니다.
        /// </summary>
        private ObservableCollection<GetOnInfo> getoninfoModel = new ObservableCollection<GetOnInfo>();
        public ObservableCollection<GetOnInfo> GetOnInfoModel { get { return getoninfoModel; } set { getoninfoModel = value; OnPropertyChanged(nameof(GetOnInfoModel)); } }

        /// <summary>
        /// 로그인한 사용자 정보입니다.
        /// </summary>
        private string _boxNum = "";
        private string _location = "";
        private string _lockData = "";
        private string _lockName = "";

        /// <summary>
        /// 선택 자물쇠의 정보입니다.
        /// </summary>
        private GetOnInfo selectedItem;

        public GetOnInfo SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public string Name { get; set; }
        /// <summary>
        /// 로딩 패널입니다.
        /// </summary>
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
        private bool isLoading;
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
        /// 상차,하차 처리 모델입니다.
        /// </summary>
        public GetOnInfo GetOnInfo
        {
            get { return getoninfo; }
            set
            {
                getoninfo = value;
                OnPropertyChanged(nameof(GetOnInfo));
            }
        }
        private GetOnInfo getoninfo;

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
        public SiehisModelList siehisData { get; set; } = new SiehisModelList();
        public SierepModelList sierepData { get; set; } = new SierepModelList();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public INavigation Navigation { get; set; }
        /// <summary>
        /// 자물쇠 로딩 이벤트입니다.
        /// </summary>
        public async Task ExecuteMyCommand()
        {
            IsLoading = true;
            try
            {
                GetOnInfoModel = new ObservableCollection<GetOnInfo>();

                var dataService = ImateHelper.GetSingleTone();


                //SIEHIS 조회
                var whereSiehisCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                {
                    new DIMWhereFieldCondition{ fieldName = "CSTATUS" , value = "L", condition = DIMWhereCondition.Equal },
                    new DIMWhereFieldCondition{ fieldName = "REFDA1" , value = LoginInfo.Name, condition = DIMWhereCondition.Equal }

                }
                };

                siehisData = await ImateHelper.SelectModelData<SiehisModelList>(App.ServerID, whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                //SIEREP 조회
                var whereSierepCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                {new DIMWhereFieldCondition{ fieldName = "REPTYP" , value = "C08000A", condition = DIMWhereCondition.Equal}}
                };

                sierepData = await ImateHelper.SelectModelData<SierepModelList>(App.ServerID, whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                if (siehisData.Count > 0)
                {
                    foreach (var item in siehisData)
                    {
                        //자물쇠 마스터
                        var whereLKCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "LSN" , value = item.LSN, condition = DIMWhereCondition.Equal}}
                        };
                        var lkMstData = await ImateHelper.SelectModelData<LkmstModelList>(App.ServerID, whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        //문서함 번호
                        var ilsWhereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "ILSID" , value = item.ILSID, condition = DIMWhereCondition.Equal}}
                        };
                        var IlsMasterData = await ImateHelper.SelectModelData<IlsmstModelList>(App.ServerID, ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        //자물쇠 위치
                        var whereIlsCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "AREA" , value = IlsMasterData[0].AREA, condition = DIMWhereCondition.Equal}}
                        };
                        var areaData = await ImateHelper.SelectModelData<AreamstModelList>(App.ServerID, whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>());


                        GetOnInfoModel.Add(new GetOnInfo(LoginInfo.Name, item.ILSID, areaData[0].AREANM, item.LSN, lkMstData[0].LKNM, item.CONFNO));
                    }
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("오류", "상차할 자물쇠가 없습니다.", "OK");
                    IsLoading = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }
        #endregion

        #region GetOnDetailViewModel
        public GetOnDetailViewModel()
        {
            LoginInfo = new LoginInfo();
            CollectionView collectionView = new CollectionView();
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockInfom");

            // 저장한 경우 처리
            if (Preferences.ContainsKey(Constants.SavePhoneNumberKey))
            {
                IsSaveNumber = true;
                LoginInfo.PhoneNumber = Preferences.Get(Constants.SavePhoneNumberKey, "");
                LoginInfo.Name = Preferences.Get(Constants.SaveLoginNameKey, "");
                LoginInfo.EMPROLE = Preferences.Get(Constants.SaveLoginRoleKey, "");

            }

        }
        #endregion



    }
}
