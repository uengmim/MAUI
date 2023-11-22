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
using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using WorkerScreen.Views;
using WorkerScreen.Models;
using XNSC.DD.EX;
using ShreDoc.Utils;
using ShreDoc.ProxyModel;
using WorkerScreen;
using Org.BouncyCastle.Asn1.Ocsp;
using WorkerScreen.Common;
using XNSC.Net.NOKE;
using XNSC;
using XNSC.Net;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Org.BouncyCastle.Ocsp;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;

namespace Camera.MAUI.Test;

public class QRCodeViewModel : INotifyPropertyChanged
{

    private string _name = "";
    private string _boxNum = "";
    private string _location = "";
    private string _lockData = "";
    private string _lockName = "";
    private Guid _guid;
    public Guid Guid
    {
        get
        {
            if (_guid == default(Guid))
            {
                _guid = Guid.NewGuid();
            }

            return _guid;
        }
        set
        {
            _guid = value;
        }
    }
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
    public string Name { get; set; }

    public string PhoneNumber { get; set; }

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
    #region Commands
    /// <summary>
    /// QR 인식 Command 입니다.
    /// </summary>
    public ICommand RecognitionCommand => new Command(QRRecognition);
    #endregion

    // Methods
    #region QRRecognition

    private object loadingObj = new object();

    /// <summary>S
    /// QR 수동입력 메뉴를 나타냅니다.
    /// </summary>
    public async void QRRecognition()
    {
        try
        {

            OnPropertyChanged(nameof(StopCamera));
            TakeSnapshot = false;
            TakeSnapshot = true;
            //카메라 정지
            AutoStartPreview = false;
            OnPropertyChanged(nameof(AutoStartPreview));
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyyMMdd");
            var sireqmodelList = new SireqModelList();
            var silockmodelList = new SilockModelList();
            var siehismodelList = new SiehisModelList();

            lock (loadingObj)
            {
                if (isLoading)
                    return;
                IsLoading = true;
            }

            if (this.BarcodeText == null || this.BarcodeText == "")
            {
                await Application.Current.MainPage.DisplayAlert("오류", "문서함 번호를 입력하세요", "OK");
                IsLoading = false;
                return;
            }
            var dataService = ImateHelper.GetSingleTone();
            var ilsWhereCondition = new DIMGroupFieldCondtion()
            {
                condition = DIMGroupCondtion.AND,
                joinCondtion = DIMGroupCondtion.AND,
                whereFieldConditions = new DIMWhereFieldCondition[]
                {new DIMWhereFieldCondition{ fieldName = "ILSID" , value = this.BarcodeText, condition = DIMWhereCondition.Equal}}
            };
            //문서함 번호
            var IlsMasterData = await dataService.Adapter.SelectModelDataAsync<IlsmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.IlsmstModelList",
                                    ilsWhereCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

            if (IlsMasterData.Count > 0)
            {
                if (IlsMasterData[0].ILSID == this.BarcodeText)
                {
                    var BarcodeData = IlsMasterData[0];

                    var areaCode = BarcodeData.AREA;
                    //자물쇠 위치
                    var whereIlsCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "AREA" , value = areaCode, condition = DIMWhereCondition.Equal}}
                    };
                    var areaData = await dataService.Adapter.SelectModelDataAsync<AreamstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.AreamstModelList",
                                whereIlsCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    if (areaData.Count > 0)
                    {
                        BoxNum = BarcodeData.ILSID;
                        Location = areaData[0].AREANM;
                    }
                    var lkCode = BoxNum;
                    //자물쇠 이름
                    var whereLKCondition = new DIMGroupFieldCondtion()
                    {
                        condition = DIMGroupCondtion.AND,
                        joinCondtion = DIMGroupCondtion.AND,
                        whereFieldConditions = new DIMWhereFieldCondition[]
                        {new DIMWhereFieldCondition{ fieldName = "ILSID" , value = lkCode, condition = DIMWhereCondition.Equal}}
                    };
                    var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                                whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                    if (lkMstData.Count > 0)
                    {
                        LockData = lkMstData[0].LSN;
                        LockName = lkMstData[0].LKNM;
                    }
                    try
                    {
                        var whereEmpCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {new DIMWhereFieldCondition{ fieldName = "PIN" , value = PhoneNumber, condition = DIMWhereCondition.Equal}}
                        };
                        var empMstData = await dataService.Adapter.SelectModelDataAsync<EmpmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.EmpmstModelList",
                                    whereEmpCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
                        if (empMstData.Count > 0)
                        {
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


                            string guidStr = Guid.NewGuid().ToString();
                            //SIREQ
                            sireqmodelList.Add(new SireqModel()
                            {
                                EREQID = formattedDate + seqData,
                                EREQTYP = "C07000C",
                                WDEPTID = empMstData[0].DEPTID,
                                WEMPID = empMstData[0].EMPNO,
                                EREQDT = currentDate,
                                ModelStatus = DIMModelStatus.Add
                            });

                            //SILOCK
                            silockmodelList.Add(new SilockModel()
                            {
                                EREQID = sireqmodelList[0].EREQID,
                                LSN = LockData,
                                AREA = areaData[0].AREA,
                                ILSID = BoxNum,
                                ASTATUS = "A",
                                CONFNO = guidStr,
                                ModelStatus = DIMModelStatus.Add
                            });

                            //SIEHIS
                            siehismodelList.Add(new SiehisModel()
                            {
                                CONFNO = guidStr,
                                EVTDT = currentDate,
                                EREQID = sireqmodelList[0].EREQID,
                                LSN = LockData,
                                ILSID = BoxNum,
                                ASTATUS = "A",
                                REFDA1 = Name,
                                ModelStatus = DIMModelStatus.Add
                            }); ;

                            var sireqResult = await dataService.Adapter.ModifyModelDataAsync<SireqModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SireqModelList", sireqmodelList);
                            var silockResult = await dataService.Adapter.ModifyModelDataAsync<SilockModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SilockModelList", silockmodelList);
                            var siehisResult = await dataService.Adapter.ModifyModelDataAsync<SiehisModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.SiehisModelList", siehismodelList);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                        IsLoading = false;
                        return;

                    }
                }
                await Application.Current.MainPage.Navigation.PushAsync(new QRRecogPage(BoxNum, Location, LockData, LockName, siehismodelList[0].CONFNO));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("오류", "문서함 번호가 정확하지 않습니다.", "OK");
                IsLoading = false;
                return;

            }

        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
            return;
        }
        finally
        {
            IsLoading = false;
        }

    }
    #endregion

    private CameraInfo camera = null;
    public CameraInfo Camera
    {
        get => camera;
        set
        {
            camera = value;
            OnPropertyChanged(nameof(Camera));
            AutoStartPreview = false;
            OnPropertyChanged(nameof(AutoStartPreview));
            AutoStartPreview = true;
            OnPropertyChanged(nameof(AutoStartPreview));
        }
    }
    private ObservableCollection<CameraInfo> cameras = new();
    public ObservableCollection<CameraInfo> Cameras
    {
        get => cameras;
        set
        {
            cameras = value;
            OnPropertyChanged(nameof(Cameras));
        }
    }
    public int NumCameras
    {
        set
        {
            if (value > 0)
                Camera = Cameras.First();
        }
    }
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
    private bool takeSnapshot = false;
    public bool TakeSnapshot
    {
        get => takeSnapshot;
        set
        {
            takeSnapshot = value;
            OnPropertyChanged(nameof(TakeSnapshot));
        }
    }
    public BarcodeDecodeOptions BarCodeOptions { get; set; }
    public string BarcodeText { get; set; } = "";
    public bool AutoStartPreview { get; set; } = false;
    public bool AutoStartRecording { get; set; } = false;

    private Result[] barCodeResults;

    public Result[] BarCodeResults
    {
        get => barCodeResults;
        set
        {
            barCodeResults = value;
            if (barCodeResults != null && barCodeResults.Length > 0)
            {
                BarcodeText = barCodeResults[0].Text;
                if (MainThread.IsMainThread)
                {
                    OnPropertyChanged(nameof(StopCamera));
                    QRRecognition();
                }
                else
                {
                    OnPropertyChanged(nameof(StopCamera));
                    MainThread.BeginInvokeOnMainThread(QRRecognition);
                }
                //Shell.Current.GoToAsync(nameof(QRRecogPage));
            }
            OnPropertyChanged(nameof(BarcodeText));
        }
    }

    public Command StartCamera { get; set; }
    public Command StopCamera { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public QRCodeViewModel()
    {
        this.LoginInfo = new LoginInfo();
        BarCodeOptions = new ZXingHelper.BarcodeDecodeOptions
        {
            AutoRotate = true,
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE },
            ReadMultipleCodes = false,
            TryHarder = true,
            TryInverted = true
        };
        OnPropertyChanged(nameof(BarCodeOptions));
        StartCamera = new Command(() =>
        {
            AutoStartPreview = true;
            OnPropertyChanged(nameof(AutoStartPreview));
        });
        StopCamera = new Command(() =>
        {
            AutoStartPreview = false;
            OnPropertyChanged(nameof(AutoStartPreview));
        });
        OnPropertyChanged(nameof(StartCamera));
        OnPropertyChanged(nameof(StopCamera));
    }
}
