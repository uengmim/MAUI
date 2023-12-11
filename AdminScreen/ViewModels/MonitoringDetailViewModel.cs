using AdminScreen.Common;
using AdminScreen.Model;
using AdminScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShreDoc.ProxyModel;
using XNSC.DD.EX;
using XNSC.Net;
using XNSC.Net.NOKE;
using System.Collections.ObjectModel;
using AdminScreen.Models;
using ShreDoc.Utils;
using System.Reflection.Emit;
using static Android.Content.ClipData;
using GoogleGson;

namespace AdminScreen.ViewModels
{
    public class MonitoringDetailViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<MonitoringData> Items { get { return Item; } set { Item = value; OnPropertyChanged(nameof(Items)); } }

        public ObservableCollection<MonitoringData> Item = new ObservableCollection<MonitoringData>();



        #region Properties
        private string _lsn = "";
        private string _lknm = "";
        private string _mac = "";
        private string _lktyp = "";
        private string _confno = "";

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

        private DateTime selectedStartDate;

        public DateTime SelectedStartDate
        {
            get { return selectedStartDate; }
            set
            {
                if (selectedStartDate != value)
                {
                    selectedStartDate = value;
                    OnPropertyChanged(nameof(SelectedStartDate));
                }
            }
        }

        private DateTime selectedEndDate;

        public DateTime SelectedEndDate
        {
            get { return selectedEndDate; }
            set
            {
                if (selectedEndDate != value)
                {
                    selectedEndDate = value;
                    OnPropertyChanged(nameof(SelectedEndDate));
                }
            }
        }
        public LockInfoData LockInfo
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfo));
            }
        }
        private LockInfoData lockinfo;

        public string LSN
        {
            get => _lsn;
            set
            {
                _lsn = value;
                OnPropertyChanged(nameof(LSN));
            }
        }
        public string LKNM
        {
            get => _lknm;
            set
            {
                _lknm = value;
                OnPropertyChanged(nameof(LKNM));
            }
        }
        public string MAC
        {
            get => _mac;
            set
            {
                _mac = value;
                OnPropertyChanged(nameof(MAC));
            }
        }
        public string LKTYP
        {
            get => _lktyp;
            set
            {
                _lktyp = value;
                OnPropertyChanged(nameof(LKTYP));
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand MapCommand => new Command(MapShow);

        private async void MapShow()
        {
            if (CONFNO == null)
            {
                await Application.Current.MainPage.DisplayAlert("통보", "진행사항이 없습니다.", "OK");

                return;
            }
            await Application.Current.MainPage.Navigation.PushAsync(new TaskMonitoringMapPage(CONFNO));
        }
        //자물쇠 로딩
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;
                Items = new ObservableCollection<MonitoringData>();
                var dataService = ImateHelper.GetSingleTone();

                //자물쇠 마스터
                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {new DIMWhereFieldCondition{ fieldName = "MAC" , value = MAC, condition = DIMWhereCondition.Equal}}
                };

                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (lkMstData[0].REFDA1 != null)
                {
                    var whereCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
    {
                    new DIMWhereFieldCondition{ fieldName = "EMPNO" , value = lkMstData[0].REFDA1, condition = DIMWhereCondition.Equal}
    }
                    };

                    var empInfoList = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                        whereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                    if (lkMstData[0].ILSID != null)
                    {
                        //문서함 번호
                        var ilsWhereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "ILSID" , value = lkMstData[0].ILSID, condition = DIMWhereCondition.Equal}}
                        };
                        var IlsMasterData = await dataService.Adapter.SelectModelDataAsync<IlsmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.IlsmstModelList",
                                    ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        //자물쇠 위치
                        var whereIlsCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "AREA" , value = IlsMasterData[0].AREA, condition = DIMWhereCondition.Equal}}
                        };

                        var areaData = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList",
                                    whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        //Siehis 조회
                        var whereSiehisCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                            new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkMstData[0].LSN, condition = DIMWhereCondition.Equal},
                            new DIMWhereFieldCondition{ fieldName = "REFDA1" , value = empInfoList[0].EMPNM, condition = DIMWhereCondition.Equal}
                            }
                        };
                        var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                    whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                        if (siehisData.Count > 0)
                        {
                            //SIEREP 조회
                            var whereSierepCondition = new DIMGroupFieldCondtion()
                            {
                                condition = DIMGroupCondtion.AND,
                                joinCondtion = DIMGroupCondtion.AND,
                                whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = siehisData[0].CONFNO, condition = DIMWhereCondition.Equal}}
                            };
                            var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                        whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                            DateTime? lockClose = null;
                            DateTime? getOn = null;
                            DateTime? getOff = null;
                            DateTime? lockoff = null;
                            DateTime? crushing = null;

                            var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
                            var sierepREPTYPB = sierepData.FirstOrDefault(h => h.REPTYP == "C08000B");
                            var sierepREPTYPC = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");
                            var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                            var sierepREPTYPE = sierepData.FirstOrDefault(h => h.REPTYP == "C08000E");
                            if (sierepREPTYPA != null)
                                lockClose = sierepREPTYPA.CRTDT;

                            if (sierepREPTYPB != null)
                                getOn = sierepREPTYPB.CRTDT;

                            if (sierepREPTYPC != null)
                                getOff = sierepREPTYPC.CRTDT;

                            if (sierepREPTYPD != null)
                                lockoff = sierepREPTYPD.CRTDT;

                            if (sierepREPTYPE != null)
                                crushing = sierepREPTYPE.CRTDT;

                            Items.Add(new MonitoringData(lkMstData[0].REFDT1, LKTYP, empInfoList[0].EMPNM, lkMstData[0].ILSID, areaData[0].AREANM,
                            siehisData[0].EVTDT, lockClose, getOn, getOff, lockoff, crushing, CONFNO));

                        }
                        else
                        {
                            Items.Add(new MonitoringData(lkMstData[0].REFDT1, LKTYP, empInfoList[0].EMPNM, null, null,
                                       null, null, null, null, null, null, CONFNO));
                        }
                    }
                    else
                    {
                        Items.Add(new MonitoringData(lkMstData[0].REFDT1, LKTYP, empInfoList[0].EMPNM, null, null,
                                   null, null, null, null, null, null, CONFNO));
                    }
                }


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

        #region MonitoringDetailViewModel
        public MonitoringDetailViewModel()
        {
            SelectedStartDate = DateTime.Now.AddDays(-7);
            SelectedEndDate = DateTime.Now;
        }

    }
    #endregion


}