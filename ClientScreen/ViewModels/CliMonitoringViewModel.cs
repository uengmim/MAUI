using ClientScreen.Model;
using ClientScreen.Models;
using ClientScreen.Views;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;
using XNSC.DD.EX;

namespace ClientScreen.ViewModels
{
    public class CliMonitoringViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LockRecall> RecallModel { get { return recallModel; } set { recallModel = value; OnPropertyChanged(nameof(RecallModel)); } }

        public ObservableCollection<LockRecall> recallModel = new ObservableCollection<LockRecall>();


        public ObservableCollection<CliLockInfoData> CliLockDataModel { get { return clilockDataModel; } set { clilockDataModel = value; OnPropertyChanged(nameof(CliLockDataModel)); } }

        public ObservableCollection<CliLockInfoData> clilockDataModel = new ObservableCollection<CliLockInfoData>();

        private LockRecall selectedItem;

        public LockRecall SelectedItem
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
                        PerformNavigation(SelectedItem.LSN, SelectedItem.LKNM, SelectedItem.MAC, SelectedItem.LKTYP, SelectedItem.CONFNO);
                    }
                }
            }
        }

        private async void PerformNavigation(string LSN, string LKNM, string MAC, string LKTYP, string CONFNO)
        {
            if (SelectedItem != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CliTaskMonitoringDetailPage(LSN, LKNM, MAC, LKTYP, CONFNO));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("오류", "선택된 자물쇠가 없습니다.", "OK");
                SelectedItem = null;
                return;
            }
        }
        private string _deptId = "";
        private string _wkpl = "";
        private string _empNo = "";
        private string _refda2 = "";
        private string _confno = "";

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

        public string DEPTID
        {
            get => _deptId;
            set
            {
                _deptId = value;
                OnPropertyChanged(nameof(DEPTID));
            }
        }
        public string WKPL
        {
            get => _wkpl;
            set
            {
                _wkpl = value;
                OnPropertyChanged(nameof(WKPL));
            }
        }
        public string EMPNO
        {
            get => _empNo;
            set
            {
                _empNo = value;
                OnPropertyChanged(nameof(EMPNO));
            }
        }
        public string REFDA2
        {
            get => _refda2;
            set
            {
                _refda2 = value;
                OnPropertyChanged(nameof(REFDA2));
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
        private Color backgroundColorSet;
        public Color BackgroundColorSet
        {
            get { return backgroundColorSet; }
            set
            {
                if (backgroundColorSet != value)
                {
                    backgroundColorSet = value;
                    OnPropertyChanged(nameof(BackgroundColorSet));
                }
            }
        }
        private Color stackBackgroundColor;
        public Color StackBackgroundColor
        {
            get { return stackBackgroundColor; }
            set
            {
                if (stackBackgroundColor != value)
                {
                    stackBackgroundColor = value;
                    OnPropertyChanged(nameof(StackBackgroundColor));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
       // public ICommand SearchEmptyLoadContactCommand { get; private set; }

        private LockRecall lockrecall;

        //수거 요청 조회
        public async Task ExecuteMyCommand()
        {
            try
            {
                IsLoading = true;

                RecallModel = new ObservableCollection<LockRecall>();

                var lockStatus = "";
                var dataService = ImateHelper.GetSingleTone();

                //areamst조회
                var areawhereIlsCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "DEPTID" , value = DEPTID, condition = DIMWhereCondition.Equal},
                    }
                };

                var areaData = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList",
                            areawhereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (areaData.Count > 0)
                {
                    foreach (var item in areaData)
                    {
                        var ilsWhereCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "AREA" , value = item.AREA, condition = DIMWhereCondition.Equal},
                            }
                        };

                        //문서함 번호
                        var IlsMasterData = await dataService.Adapter.SelectModelDataAsync<IlsmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.IlsmstModelList",
                                                ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        var whereLKCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "ILSID" , value = IlsMasterData[0].REFDA2, condition = DIMWhereCondition.Equal}
                            }
                        };

                        var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                                    whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        if (lkMstData.Count > 0)
                        {
                            foreach (var lkItem in lkMstData)
                            {
                                //sireq조회
                                var whereSireqCondition = new DIMGroupFieldCondtion()
                                {
                                    condition = DIMGroupCondtion.AND,
                                    joinCondtion = DIMGroupCondtion.AND,
                                    whereFieldConditions = new DIMWhereFieldCondition[]
                                    {
                                        new DIMWhereFieldCondition{ fieldName = "EREQTYP" , value = "C07000Q", condition = DIMWhereCondition.Equal},
                                        new DIMWhereFieldCondition{ fieldName = "WEMPID" , value = EMPNO, condition = DIMWhereCondition.Equal},
                                        new DIMWhereFieldCondition{ fieldName = "REFDA1" , value = IlsMasterData[0].REFDA2, condition = DIMWhereCondition.Equal}
                                    }
                                };

                                var sireqData = await dataService.Adapter.SelectModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList",
                                            whereSireqCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);


                                var whereSiehisCondition = new DIMGroupFieldCondtion()
                                {
                                    condition = DIMGroupCondtion.AND,
                                    joinCondtion = DIMGroupCondtion.AND,
                                    whereFieldConditions = new DIMWhereFieldCondition[]
                                    {
                                        new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkItem.LSN, condition = DIMWhereCondition.Equal}
                                    }
                                };

                                var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                            whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                                var siehisConfNo = "";

                                if (siehisData.Count == 0)
                                    siehisConfNo = null;
                                else if (siehisData.Count > 0)
                                    siehisConfNo = siehisData[0].CONFNO;

                                //SIEREP 조회
                                var whereSierepCondition = new DIMGroupFieldCondtion()
                                {
                                    condition = DIMGroupCondtion.AND,
                                    joinCondtion = DIMGroupCondtion.AND,
                                    whereFieldConditions = new DIMWhereFieldCondition[]
                                    {
                                        new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = siehisConfNo, condition = DIMWhereCondition.Equal}
                                    }
                                };

                                var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
                                            whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                                var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
                                var sierepREPTYPB = sierepData.FirstOrDefault(h => h.REPTYP == "C08000B");
                                var sierepREPTYPC = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");
                                var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
                                var sierepREPTYPE = sierepData.FirstOrDefault(h => h.REPTYP == "C08000E");

                                StackBackgroundColor = Colors.DodgerBlue;


                                if (lkItem.ILSID == null && lkItem.REFDA1 == null && siehisData.Count > 0)
                                    lockStatus = "회수";

                                else if (lkItem.ILSID == null && lkItem.REFDA1 != null)
                                    lockStatus = "지급";

                                else if (lkItem.ILSID != null && sierepData.Count == 0)
                                    lockStatus = "봉인준비";

                                else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPA != null && sierepREPTYPB == null && sierepREPTYPC == null && sierepREPTYPD == null && sierepREPTYPE == null)
                                {
                                    lockStatus = "봉인";
                                    StackBackgroundColor = Colors.Crimson;
                                }

                                else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPB != null && sierepREPTYPC == null && sierepREPTYPD == null && sierepREPTYPE == null)
                                {
                                    lockStatus = "상차";
                                    StackBackgroundColor = Colors.Crimson;
                                }

                                else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPC != null && sierepREPTYPD == null && sierepREPTYPE == null)
                                {
                                    lockStatus = "하차";
                                    StackBackgroundColor = Colors.OliveDrab;
                                }

                                else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPD != null && sierepREPTYPE == null)
                                    lockStatus = "봉인해제";

                                else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPE != null)
                                {
                                    lockStatus = "파쇄";
                                    StackBackgroundColor = Colors.OliveDrab;
                                }

                                else if (sireqData.Count == 0)
                                    lockStatus = "등록";

                                //srieqp 데이터가 있으면
                                if (sireqData.Count > 0)
                                {
                                    RecallModel.Add(new LockRecall(IlsMasterData[0].REFDA2, item.AREANM, "", "수거 요청", lkItem.LSN, lkItem.MAC, lockStatus, lkItem.LKNM, siehisConfNo));
                                }
                                else
                                {
                                    RecallModel.Add(new LockRecall(IlsMasterData[0].REFDA2, item.AREANM, "", "", lkItem.LSN, lkItem.MAC, lockStatus, lkItem.LKNM, siehisConfNo));
                                }
                            }
                            IsLoading = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }

        //자물쇠 로딩
        //public async void clickEvent()
        //{
        //    IsLoading = true;
        //    var lockStatus = "";

        //    try
        //    {
        //        var dataService = ImateHelper.GetSingleTone();

        //        //areamst조회
        //        var areawhereIlsCondition = new DIMGroupFieldCondtion()
        //        {
        //            condition = DIMGroupCondtion.AND,
        //            joinCondtion = DIMGroupCondtion.AND,
        //            whereFieldConditions = new DIMWhereFieldCondition[]
        //            {
        //                new DIMWhereFieldCondition{ fieldName = "DEPTID" , value = DEPTID, condition = DIMWhereCondition.Equal},
        //            }
        //        };

        //        var areaData = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList",
        //                    areawhereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

        //        if (areaData.Count > 0)
        //        {
        //            foreach (var item in areaData)
        //            {
        //                var ilsWhereCondition = new DIMGroupFieldCondtion()
        //                {
        //                    condition = DIMGroupCondtion.AND,
        //                    joinCondtion = DIMGroupCondtion.AND,
        //                    whereFieldConditions = new DIMWhereFieldCondition[]
        //                    {
        //                        new DIMWhereFieldCondition{ fieldName = "AREA" , value = item.AREA, condition = DIMWhereCondition.Equal},
        //                    }
        //                };

        //                //문서함 번호
        //                var IlsMasterData = await dataService.Adapter.SelectModelDataAsync<IlsmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.IlsmstModelList",
        //                                        ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

        //                var whereLKCondition = new DIMGroupFieldCondtion()
        //                {
        //                    condition = DIMGroupCondtion.AND,
        //                    joinCondtion = DIMGroupCondtion.AND,
        //                    whereFieldConditions = new DIMWhereFieldCondition[]
        //                    {
        //                        new DIMWhereFieldCondition{ fieldName = "ILSID" , value = IlsMasterData[0].REFDA2, condition = DIMWhereCondition.Equal}
        //                    }
        //                };

        //                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
        //                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

        //                if (lkMstData.Count > 0)
        //                {
        //                    foreach (var lkItem in lkMstData)
        //                    {

        //                        //sireq조회
        //                        var whereSireqCondition = new DIMGroupFieldCondtion()
        //                        {
        //                            condition = DIMGroupCondtion.AND,
        //                            joinCondtion = DIMGroupCondtion.AND,
        //                            whereFieldConditions = new DIMWhereFieldCondition[]
        //                            {
        //                                new DIMWhereFieldCondition{ fieldName = "EREQTYP" , value = "C07000Q", condition = DIMWhereCondition.Equal},
        //                                new DIMWhereFieldCondition{ fieldName = "WEMPID" , value = EMPNO, condition = DIMWhereCondition.Equal},
        //                                new DIMWhereFieldCondition{ fieldName = "REFDA1" , value = IlsMasterData[0].REFDA2, condition = DIMWhereCondition.Equal}
        //                            }
        //                        };

        //                        var sireqData = await dataService.Adapter.SelectModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList",
        //                                    whereSireqCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

        //                        var whereSiehisCondition = new DIMGroupFieldCondtion()
        //                        {
        //                            condition = DIMGroupCondtion.AND,
        //                            joinCondtion = DIMGroupCondtion.AND,
        //                            whereFieldConditions = new DIMWhereFieldCondition[]
        //                            {
        //                                new DIMWhereFieldCondition{ fieldName = "LSN" , value = lkItem.LSN, condition = DIMWhereCondition.Equal}
        //                            }
        //                        };

        //                        var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
        //                                    whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

        //                        var siehisConfNo = "";

        //                        if (siehisData.Count == 0)
        //                            siehisConfNo = null;
        //                        else if (siehisData.Count > 0)
        //                            siehisConfNo = siehisData[0].CONFNO;

        //                        //SIEREP 조회
        //                        var whereSierepCondition = new DIMGroupFieldCondtion()
        //                        {
        //                            condition = DIMGroupCondtion.AND,
        //                            joinCondtion = DIMGroupCondtion.AND,
        //                            whereFieldConditions = new DIMWhereFieldCondition[]
        //                            {
        //                                new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = siehisConfNo, condition = DIMWhereCondition.Equal}
        //                            }
        //                        };

        //                        var sierepData = await dataService.Adapter.SelectModelDataAsync<SierepModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SierepModelList",
        //                                    whereSierepCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

        //                        var sierepREPTYPA = sierepData.FirstOrDefault(h => h.REPTYP == "C08000A");
        //                        var sierepREPTYPB = sierepData.FirstOrDefault(h => h.REPTYP == "C08000B");
        //                        var sierepREPTYPC = sierepData.FirstOrDefault(h => h.REPTYP == "C08000C");
        //                        var sierepREPTYPD = sierepData.FirstOrDefault(h => h.REPTYP == "C08000D");
        //                        var sierepREPTYPE = sierepData.FirstOrDefault(h => h.REPTYP == "C08000E");

        //                        StackBackgroundColor = Colors.DodgerBlue;


        //                        if (lkItem.ILSID == null && lkItem.REFDA1 == null && siehisData.Count > 0)
        //                            lockStatus = "회수";

        //                        else if (lkItem.ILSID == null && lkItem.REFDA1 != null)
        //                            lockStatus = "지급";

        //                        else if (lkItem.ILSID != null && sierepData.Count == 0)
        //                            lockStatus = "봉인준비";

        //                        else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPA != null && sierepREPTYPB == null && sierepREPTYPC == null && sierepREPTYPD == null && sierepREPTYPE == null)
        //                        {
        //                            lockStatus = "봉인";
        //                            StackBackgroundColor = Colors.Crimson;
        //                        }

        //                        else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPB != null && sierepREPTYPC == null && sierepREPTYPD == null && sierepREPTYPE == null)
        //                        {
        //                            lockStatus = "상차";
        //                            StackBackgroundColor = Colors.Crimson;
        //                        }

        //                        else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPC != null && sierepREPTYPD == null && sierepREPTYPE == null)
        //                        {
        //                            lockStatus = "하차";
        //                            StackBackgroundColor = Colors.OliveDrab;
        //                        }

        //                        else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPD != null && sierepREPTYPE == null)
        //                            lockStatus = "봉인해제";

        //                        else if (lkItem.ILSID != null && sierepData.Count > 0 && sierepREPTYPE != null)
        //                        {
        //                            lockStatus = "파쇄";
        //                            StackBackgroundColor = Colors.OliveDrab;
        //                        }

        //                        else if (sireqData.Count == 0)
        //                            lockStatus = "등록";

        //                        //srieqp 데이터가 있으면
        //                        if (sireqData.Count > 0)
        //                        {
        //                            RecallModel.Add(new LockRecall(IlsMasterData[0].REFDA2, item.AREANM, "", "수거 요청", lkItem.LSN, lkItem.MAC, lockStatus, lkItem.LKNM, siehisConfNo));
        //                        }
        //                        else
        //                        {
        //                            RecallModel.Add(new LockRecall(IlsMasterData[0].REFDA2, item.AREANM, "", "", lkItem.LSN, lkItem.MAC, lockStatus, lkItem.LKNM, siehisConfNo));
        //                        }
        //                    }
        //                    IsLoading = false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
        //        IsLoading = false;
        //        return;
        //    }
        //}

        #endregion

        //#region MonitoringViewModel
        //public CliMonitoringViewModel()
        //{
        //    SearchEmptyLoadContactCommand = new Command(async () => await ExecuteMyCommand());
        //    CollectionView collectionView = new CollectionView();
        //    collectionView.SetBinding(ItemsView.ItemsSourceProperty, "LockInfom");
        //}

    }
    //#endregion
}