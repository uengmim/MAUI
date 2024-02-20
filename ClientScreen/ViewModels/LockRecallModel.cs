using ClientScreen.Model;
using ClientScreen.Views;
using NetTopologySuite.Index.HPRtree;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Linq;
using XNSC.DD.EX;
using Command = Microsoft.Maui.Controls.Command;


namespace ClientScreen.ViewModels
{
    public class LockRecallModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LockRecall> RecallModel { get { return recallModel; } set { recallModel = value; OnPropertyChanged(nameof(RecallModel)); } }

        public ObservableCollection<LockRecall> recallModel = new ObservableCollection<LockRecall>();

        private string _deptId = "";
        private string _wkpl = "";
        private string _empNo = "";
        private string _refda2 = "";

        //PropertyChanged 변경
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                    if (selectedItem != null)
                    {
                        PerformNavigation(SelectedItem.BoxName, SelectedItem.Location, SelectedItem.EreqId);
                    }
                }
            }
        }

        private async void PerformNavigation(string BoxName, string Location, string EREQID)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                var dataService = ImateHelper.GetSingleTone();
                string formattedDate = currentDate.ToString("yyyyMMdd");
                var sireqmodelList = new SireqModelList();

                if (SelectedItem != null)
                {
                    //즉시 수거 요청
                    if (EREQID == "")
                    {
                        bool answer = await Application.Current.MainPage.DisplayAlert("알림", $"[{BoxName}]보안문서함의 수거 요청을 하시겠습니까?", "확인", "취소");

                        if (answer == false)
                        {
                            SelectedItem = null;
                            return;
                        }

                        //스토어드 프로시저
                        var queryParams = new QueryParameter[]
                        { new QueryParameter(){name = "prefix", dataType = QueryDataType.String, value= formattedDate } };

                        var queryMsg = new QueryMessage()
                        {
                            queryMethod = QueryRunMethod.Alone,
                            queryName = "getDocSeq",
                            dataSource = App.ServerID, //<--- 프리페어런스의 값으로 변경하여야 함
                            queryTemplate = "SELECT `getDocSeq`('sireq', @prefix) AS seq",
                            parameters = queryParams,
                            cacheType = QueryCacheType.None
                        };
                        var queryData = await ImateHelper.GetSingleTone().Adapter.DbSelectToDataSetAsync(new List<QueryMessage>(new QueryMessage[] { queryMsg }));
                        var seq = queryData.Tables[0].Rows[0].Field<decimal>("seq");
                        var seqData = seq.ToString().PadLeft(5, '0');

                        //SIREQ
                        sireqmodelList.Add(new SireqModel()
                        {
                            EREQID = formattedDate + seqData,
                            EREQTYP = "C07000Q",
                            WDEPTID = DEPTID,
                            REFDA1 = BoxName,
                            WEMPID = EMPNO,
                            EREQDT = currentDate,
                            ModelStatus = DIMModelStatus.Add
                        });

                        var sireqResult = await dataService.Adapter.ModifyModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList", sireqmodelList);

                        //RecallModel.Add(new LockRecall(BoxName, Location, sireqmodelList[0].EREQID));


                        foreach (var recallItem in RecallModel)
                        {
                            if (recallItem.BoxName == BoxName)
                            {
                                recallItem.BoxName = BoxName;
                                recallItem.Location = Location;
                                recallItem.EreqId = sireqmodelList[0].EREQID;
                                recallItem.Status = "수거 요청";
                            }
                        }
                        await Application.Current.MainPage.DisplayAlert("알림", $"[{BoxName}]보안문서함의 수거를 요청하였습니다.", "확인");
                        isLoading = true;
                        RecallModel = new ObservableCollection<LockRecall>();
                        await ExecuteMyCommand();
                        SelectedItem = null;
                        isLoading = false;

                    }

                    else
                    {

                        bool answer = await Application.Current.MainPage.DisplayAlert("알림", $"[{BoxName}]보안문서함의 수거 요청을 취소하시겠습니까?", "확인", "취소");

                        if (answer == false)
                        {
                            SelectedItem = null;
                            return;
                        }


                        //true일때
                        var whereSireqCondition = new DIMGroupFieldCondtion()
                         {
                             condition = DIMGroupCondtion.AND,
                             joinCondtion = DIMGroupCondtion.AND,
                             whereFieldConditions = new DIMWhereFieldCondition[]
                             {
                                   new DIMWhereFieldCondition{ fieldName = "WDEPTID" , value = DEPTID, condition = DIMWhereCondition.Equal},
                                   new DIMWhereFieldCondition{ fieldName = "WEMPID" , value = EMPNO, condition = DIMWhereCondition.Equal},
                                   new DIMWhereFieldCondition{ fieldName = "REFDA1" , value = BoxName, condition = DIMWhereCondition.Equal},
                             }
                        };

                        var sireqData = await dataService.Adapter.SelectModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList",
                                   whereSireqCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);


                        //Siehis 조회
                        var whereSiehisCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                            new DIMWhereFieldCondition{ fieldName = "EREQID" , value = sireqData[0].EREQID, condition = DIMWhereCondition.Equal},
                            }
                        };
                        var siehisData = await dataService.Adapter.SelectModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList",
                                    whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                        if(siehisData.Count > 0)
                        {
                            await Application.Current.MainPage.DisplayAlert("알림", $"[{BoxName}]보안문서함은 작업 진행 중입니다.", "확인");
                            SelectedItem = null;
                            isLoading = false;
                            return;
                        }

                        if (sireqData.Count > 0)
                         {
                            //SIREQ
                             sireqmodelList.Add(new SireqModel()
                             {
                                   EREQID = sireqData[0].EREQID,
                                   ModelStatus = DIMModelStatus.Delete
                             });

                          var sireqResult = await dataService.Adapter.ModifyModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList", sireqmodelList);

                            foreach (var cancelItem in RecallModel)
                            {
                               if (cancelItem.BoxName == BoxName)
                               {
                                    cancelItem.BoxName = BoxName;
                                    cancelItem.Location = Location;
                                    cancelItem.EreqId = "";
                                    cancelItem.Status = "수거 요청 취소";
                               }
                            }

                            await Application.Current.MainPage.DisplayAlert("알림", $"[{BoxName}]보안문서함의 수거를 요청을 취소하였습니다.", "확인");
                            isLoading = true;
                            RecallModel = new ObservableCollection<LockRecall>();
                            await ExecuteMyCommand();
                            SelectedItem = null;
                            isLoading = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                SelectedItem = null;
                isLoading = false;
                return;
            }
        }

        public LockRecall LockRecall
        {
            get { return lockrecall; }
            set
            {
                lockrecall = value;
                OnPropertyChanged(nameof(lockrecall));
            }
        }
        private LockRecall lockrecall;

        //수거 요청 조회
        public async Task ExecuteMyCommand()
        {
            IsLoading = true;

            try
            {
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

                        //srieqp 데이터가 있으면
                        if (sireqData.Count > 0)
                            RecallModel.Add(new LockRecall(IlsMasterData[0].REFDA2, item.AREANM, sireqData[0].EREQID, "수거 요청","","","","",""));

                        else
                            RecallModel.Add(new LockRecall(IlsMasterData[0].REFDA2, item.AREANM, "", "", "", "", "", "", ""));
                    }
                    IsLoading = false;

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                IsLoading = false;
                return;
            }
        }
    }
  }