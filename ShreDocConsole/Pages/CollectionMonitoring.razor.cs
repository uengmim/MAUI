using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DevExpress.Blazor;
using Blazored.Modal.Services;
using System.Security.Claims;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components.Forms;
using DevExpress.Export;

using ShreDoc.ProxyModel;
using ShreDocConsole.Data;
using ShreDocConsole.Utility;
using ShreDocConsole.Service;

using XNSC.Net;
using XNSC.DD.EX;

using RES = ShreDocConsole.Properties.Resources;
using DevExpress.Data.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.Channels;
using Microsoft.Extensions.Logging;
using DevExpress.Xpo.DB;
using LeafletForBlazor;
using static LeafletForBlazor.Map;
using LeafletForBlazor.RealTime.points;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraPrinting.Native;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ShreDoc.DataModel.CustomFunction.SubModel;
using System.Text.Json;
using System.Text;
using Google.Apis.Auth.OAuth2;
using System.Net.Http;
using System.Data;
using static ShreDoc.DataModel.CustomFunction.SubModel.FCMMessageModel;

namespace ShreDocConsole.Pages
{
    /// <summary>
    /// ȸ���� �� ȸ������ ��ġ �ǽð� ����͸�
    /// </summary>
    public partial class CollectionMonitoring
    {
        /// <summary>
        /// �����Ͽ���
        /// </summary>
        bool AutoCollapseDetailRow { get; set; }

        /// <summary>
        /// ��� Grid Data ����
        /// </summary>
        private IGrid? MonitoringGrid;

        /// <summary>
        /// ������ Grid Data ����
        /// </summary>
        private IGrid? MonitoringItemGrid;

        //------------------------------------------------------------
        /// <summary>
        /// ��� Api ����
        /// </summary>
        private MonitoringModelList? MonitoringHeaderList;

        /// <summary>
        /// ������ Api ����
        /// </summary>
        private MonitoringItemModelList? MonitoringItemList;

        /// <summary>
        /// ���Ӹ� Api ����
        /// </summary>
        private MonitoringSummaryModelList? MonitoringSummary;

        //--------------------------------------------------------------

        /// <summary>
        /// ���õ� �μ��� ���� �𵨰� ���� ����Ʈ
        /// </summary>
        private List<DeptmstModel>? deptDatas = null;

        /// <summary>
        /// �μ� ����Ʈ
        /// </summary>
        private DeptmstModelList? deptDatList = null;

        /// <summary>
        /// ��� ��Ȱ ��� �� ����Ʈ
        /// </summary>
        private EmployeeRoleTargetModelList? empRoleTargetModelList = null;

        /// <summary>
        /// ���� ��Ȱ ��� ����Ʈ
        /// </summary>
        private IList<EmployeeRoleTargetModel>? groupTargetList = null;

        /// <summary>
        /// ���� ��Ȱ ��� ����Ʈ
        /// </summary>
        private IList<EmployeeRoleTargetModel>? indivTargetList = null;

        //------------------------------------------------------------------------

        /// <summary>
        /// �ΰ� ���� ����
        /// </summary>
        private IEnumerable<CdmstModel>? deptTypeDatas = null;

        /// <summary>
        /// ���� ����
        /// </summary>
        private IEnumerable<CdmstModel>? ilsTypes = null;

        /// <summary>
        /// ��Ȱ(Rank)
        /// </summary>
        private IEnumerable<CodeInfo> roles = ShreDocUtility.Roles();

        /// <summary>
        /// ����(Permission)
        /// </summary>
        private IEnumerable<CodeInfo> permissions = ShreDocUtility.Permissions();


        /// <summary>
        /// ��Ȱ(Rank)
        /// </summary>
        private IEnumerable<CodeInfo> rolesRank = ShreDocUtility.RolesRank();


        /// <summary>
        /// ����(Permission)
        /// </summary>
        private IEnumerable<CodeInfo> rolePermissions = ShreDocUtility.Permissions("");

        /// <summary>
        /// ���� ����
        /// </summary>
        private IEnumerable<CodeInfo> selCmdFilterOption = ShreDocUtility.SelectCommandFilterOption();

        /// <summary>
        /// Prefix ���̱�
        /// </summary>
        private bool prefixVisible = true;
        /// <summary>
        /// �������
        /// </summary>
        private bool editMode = false;
        /// <summary>
        /// ���� Ŀ�� SPAN
        /// </summary>
        private int seqColSpan = 4;

        //----------------------------------------------------------------------

        /// <summary>
        /// ���԰��� �۾���
        /// </summary>
        private bool ShowActiveData { get; set; } = true;
        /// <summary>
        /// �������� �۾���
        /// </summary>
        private bool ShowDeactiveData { get; set; } = false;
        /// <summary>
        /// ��� �ڷ�
        /// </summary>
        private bool ShowAllData { get; set; } = false;

        /// <summary>
        /// POPUP Edit ����
        /// </summary>
        private string EditPopupHeaderText = "";

        /// <summary>
        /// ������ Tab
        /// </summary>
        private int ActiveTabIndex { get; set; } = 0;

        /// <summary>
        /// �޺��ڽ� ������
        /// </summary>
        /// <returns></returns>
        private WkplmstModelList? wkplDatas = null;
        /// <summary>
        /// ������ ����
        /// </summary>
        private WkplmstModel? selectedWkpl = null;

        /// <summary>
        /// ������ ���۳�¥
        /// </summary>
        private DateTime selectedSDate = DateTime.Now.AddDays(-7);

        /// <summary>
        /// ������ ���ᳯ¥
        /// </summary>
        private DateTime selectedEDate = DateTime.Now;

        /// <summary>
        /// ���õ� �۾���(���)
        /// </summary>
        private WorkerModel? selectedWorker = null;

        /// <summary>
        /// �۾��� �÷� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        private string GetWorkplaceCaption(string property) => ImateAdapterUtility.GetXNDisplayName<WkplmstModel>(property) ?? property;

        /// <summary>
        /// �÷� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetWorkerEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<WorkerFormEditContext>(property);

        /// <summary>
        /// �÷� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetDeptCaption(string property) => ImateAdapterUtility.GetXNDisplayName<DeptmstModel>(property);

        /// <summary>
        /// �÷� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetRoleEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<EmployeeRoleEditModel>(property);

        /// <summary>
        /// �÷� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetRoleTargetEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<EmployeeRoleTargetModel>(property);


        /// <summary>
        /// �÷� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetCaption(string property) => ImateAdapterUtility.GetXNDisplayName<MonitoringModel>(property);

        /// <summary>
        /// ���Ӹ� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetSummaryCaption(string property) => ImateAdapterUtility.GetXNDisplayName<MonitoringSummaryModel>(property);

        /// <summary>
        /// Role(Rank)�̸�
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetRoleName(WorkerModel dataItem) => rolesRank.FirstOrDefault(r => r.code == dataItem?._Role)?.name ?? "";

        /// <summary>
        /// Permision(Position)�̸�
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetPermissionName(WorkerModel dataItem) => permissions.FirstOrDefault(r => r.code == dataItem?._Permission)?.name ?? "";

        //// <summary>
        /// �μ������� �μ�������
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private string GetDeptTypeName(WorkerModel worker) => deptTypeDatas?.FirstOrDefault(d => d.CODE == worker?.depttyp)?.CDNM ?? "";

        //// <summary>
        /// �μ������� �κμ�������
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private string GetIlsTypeName(EmployeeRoleTargetModel ils) => ilsTypes?.FirstOrDefault(d => d.CODE == ils?.ilstype)?.CDNM ?? "";

        /// <summary>
        /// �÷� �̸�(ĸ��)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetWorkerSafeEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<WorkerSafeFormEditContext>(property);

        /// <summary>
        /// �̼� ����
        /// </summary>
        private IEnumerable<CodeInfo> certificationStatus =ShreDocUtility.CertificationStatus();

        /// <summary>
        /// ����� ID
        /// </summary>
        private string userId = "";

        /// <summary>
        /// ���õ� �۾���
        /// </summary>
        private WorkerModel? selectedWorkerModel = null;

        private MonitoringModel? selectedMonitoringModel = null;

        private string selectConfno = "";

        /// <summary>
        /// �������� �޴� ���� ����
        /// </summary>
        private bool existsSafeMgr = false;

        /// <summary>
        /// ���� ��ġ �˾� ���� ����
        /// </summary>
        private bool mapLocationPopupVisible = false;

        /// <summary>
        /// ������ġ ������Ʈ
        /// </summary>
        private RealTimeMap mapLocationComponent { get; set; }

        /// <summary>
        /// ������ġ ������Ʈ �Ķ����
        /// </summary>
        private RealTimeMap.LoadParameters mapParam { get; set; } = new RealTimeMap.LoadParameters();

        /// <summary>
        /// ������ġ ����Ʈ �ɺ�
        /// </summary>
        private RealTimeMap.PointSymbol symbol { get; set; }

        /// <summary>
        /// ��� ���� ��ü
        /// </summary>
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        /// <summary>
        /// ���μ�
        /// </summary>
        private string SummaryA { get; set; } = "";

        /// <summary>
        /// ������
        /// </summary>
        private string SummaryB { get; set; } = "";

        /// <summary>
        /// ������
        /// </summary>
        private string SummaryC { get; set; } = "";

        /// <summary>
        /// ����������
        /// </summary>
        private string SummaryD { get; set; } = "";

        /// <summary>
        /// �ļ��
        /// </summary>
        private string SummaryE { get; set; } = "";

        /// <summary>
        /// ȭ�� ����(PC/�����) ����
        /// </summary>
        private bool isXSmallScreen { get; set; }

        /// <summary>
        /// ����͸� �� ���� ���ڿ�
        /// </summary>
        private string mapWidth { get; set; } = "";

        /// <summary>
        /// �ʱ�ȭ
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //�α��� �ĺ���
            var imateAuthStateProvider = (ImateAuthStateProvider)AuthenticationStateProvider;
            var authState = imateAuthStateProvider.CurrentAuthenticationState??imateAuthStateProvider.UnAuthenticationState;
            
            //����� ID
            userId = authState.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (userId == ImateAuthStateProvider.unauthentication)
            {
                try
                {
                    NavManager.NavigateTo("logout", true);
                    return;
                }
                catch (Exception)
                {
                    return;
                }
            }
            mapLocationPopupVisible = true;
            mapLocationPopupVisible = false;

            existsSafeMgr = false;

            mapParam = new RealTimeMap.LoadParameters()  //general map settings
            {
                zoom_level = 12

            };

            await Reload(true);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isInit"></param>
        /// <returns></returns>
        private async Task Reload(bool isInit)
        {
            try
            {
                if (isInit)
                {
                    //�����
                    wkplDatas = await ShreDocUtilService.GetWorkplace(true);
                    if (ShreDocUtility.LastSlectedWkpl != null)
                        selectedWkpl = ShreDocUtility.LastSlectedWkpl;
                }

                if (selectedWkpl == null)
                    return;

                MonitoringHeaderList = await ShreDocUtilService.GetMonitoringHeader(selectedWkpl?.WKPL??string.Empty, selectedSDate.ToString("yyyy-MM-dd"), selectedEDate.ToString("yyyy-MM-dd"));

                //MonitoringGrid.AutoFitColumnWidths();
                MonitoringSummary = await ShreDocUtilService.GetMonitoringSummary(selectedWkpl?.WKPL ?? string.Empty, selectedSDate.ToString("yyyy-MM-dd"), selectedEDate.ToString("yyyy-MM-dd"));
                foreach(var row in MonitoringSummary)
                {
                    SummaryA = row.SUMA.ToString()??"";
                    SummaryB = row.SUMB.ToString() ?? "";
                    SummaryC = row.SUMC.ToString() ?? "";
                    SummaryD = row.SUMD.ToString() ?? "";
                    SummaryE = row.SUME.ToString() ?? "";
                }

                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}.Reload: {ex.Message}");
                Modal.Alert($"{ex.Message}(Reload)", RES.STR_ERROR);
            }
        }

        private void changeMapWidth(string size)
        {
            mapLocationComponent.width = size??"";
        }

        private async void CFTest()
        {
            try
            {
                //var data = new FCMMessageModel()
                //{
                //    message = new FCMMessageModel.Message()
                //    {
                //        token = "e9HskBX1T2GZjiobOjplrQ:APA91bEzl0jR9fL69-mtZtLveHDpMY01rL26ZjlyVTo92wpkW4AGcRBQHemSzcuwkLtgX_63TMJHmwzyeg0qNmzMkv2L4tDc53Ka_1n3ryIAyNoHaviGgo5ix_QzwjQC3Y4OxpwXOnAl",
                //        notification = new FCMMessageModel.Message.Notification()
                //        {
                //            title = "FCM�׽�Ʈ",
                //            body = "�׽�Ʈ�׽�Ʈ"
                //        }
                //    }
                //};

                //string json = JsonSerializer.Serialize(data);
                //Byte[] toArray = Encoding.UTF8.GetBytes(json);

                var profile = ShreDocUtility.Profile;


                var dbtitleParam = new QueryParameter()
                {
                    name = "dbtitle",
                    dataType = QueryDataType.String,
                    value = "shredoc",
                    template = "",
                    lineTerminateChar = "",
                    prefix = "",
                    surfix = ""
                };

                var deptParam = new QueryParameter()
                {
                    name = "dept",
                    dataType = QueryDataType.String,
                    value = "ISTN01",
                    template = "",
                    lineTerminateChar = "",
                    prefix = "",
                    surfix = ""
                };

                var titleParam = new QueryParameter()
                {
                    name = "title",
                    dataType = QueryDataType.String,
                    value = "�׽�Ʈ",
                    template = "",
                    lineTerminateChar = "",
                    prefix = "",
                    surfix = ""
                };

                var messageParam = new QueryParameter()
                {
                    name = "message",
                    dataType = QueryDataType.String,
                    value = "�׽�Ʈ�޽���",
                    template = "",
                    lineTerminateChar = "",
                    prefix = "",
                    surfix = ""
                };


                var TestResultQuery = new QueryMessage()
                {
                    queryMethod = QueryRunMethod.Alone,
                    cacheType = QueryCacheType.None,
                    queryName = "testResult",
                    dataSource = "#func",
                    queryTemplate = "ShreDocDataModel@ShreDoc.DataModel.CustomFunction.SendFCMInterface",
                    dependQuery = [],
                    parameters = [dbtitleParam, deptParam, titleParam, messageParam]
                };
                var resultSet = await ShreDocUtilService.sendFCM(TestResultQuery);

                var dt = resultSet.Tables[0];
                if(dt.Rows[0].Field<string>("TYPE") != "S")
                    Modal.Alert($"{dt.Rows[0].Field<string>("MESSAGE")}", RES.STR_ERROR);
                else
                    Modal.Alert($"���ۼ��� // {dt.Rows[0].Field<string>("MESSAGE")}", RES.STR_CONFIRM);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}.CFTest: {ex.Message}");
                Modal.Alert($"{ex.Message}(CFTest)", RES.STR_ERROR);
            }
        }


        private async void OnGirdMapBtnClicked(MonitoringModel selectRow)
        {
            //Modal.Alert(selectRow.CONFNO, "Ŭ��");
            mapLocationPopupVisible = true;
            //mapLocationComponent.Parameters.zoom_level = 15;

            symbol = new RealTimeMap.PointSymbol()
            {
                color = "black",
                fillColor = "red",
                radius = 10,
                weight = 1,
                opacity = 1,
                fillOpacity = 1
            };

            //await mapLocationComponent.Geometric.Points.delete();

            var polySymbol = new RealTimeMap.PolygonSymbol()
            {
                color = "transparent",
                fillColor = "transparent",
                weight = 4,
                opacity = 0.4,
                fillOpacity = 0.4
            };

            MonitoringItemModelList itemList = await ShreDocUtilService.GetMonitoringItem(selectRow.CONFNO??"");

            if (itemList.Count == 0) { return; }

            setMonitoringMap(itemList); 
        }

        private void setMonitoringMap(MonitoringItemModelList itemList)
        {
            symbol = new RealTimeMap.PointSymbol()
            {
                color = "black",
                fillColor = "red",
                radius = 10,
                weight = 1,
                opacity = 1,
                fillOpacity = 1
            };

            //mapLocationComponent.Geometric.Points.delete();

            var polySymbol = new RealTimeMap.PolygonSymbol()
            {
                color = "black",
                fillColor = "red",
                weight = 4,
                opacity = 0.4,
                fillOpacity = 0.4
            };
            

            List<double[]> coordinates = new List<double[]>();
            var points = new List<RealTimeMap.StreamPoint>();

            for (int i = 0; i < itemList.Count; i++)
            {
                
                coordinates.Add(new double[] { itemList[i].LAT ?? 0, itemList[i].LON ?? 0 });
                points.Add(new RealTimeMap.StreamPoint() { latitude = itemList[i].LAT ?? 0, longitude = itemList[i].LON ?? 0, type = "Point", value = itemList[i].REPTYPNM ?? "" });
                //await mapLocationComponent.Geometric.DisplayPointsFromArray.add(new double[] { itemList[i].LAT ?? 0, itemList[i].LON ?? 0 }, symbol);
                //mapLocationComponent.addPoint(new RealTimeMap.CoordinatePoint() { coordinate = coordinates[i] });
                //mapLocationComponent.Geometric.Points.add(points[i]);
                //await mapLocationComponent.movePoint(new double[] { itemList[i].LAT ?? 0, itemList[i].LON ?? 0 });
                mapLocationComponent.Geometric.DisplayPointsFromArray.addPoint(new double[] { itemList[i].LAT ?? 0, itemList[i].LON ?? 0 }, symbol);
                
            }

            //mapLocationComponent.Geometric.Points.add(points.ToArray());
            //mapLocationComponent.Geometric.Points.moveTo(points[0]);
            //await mapLocationComponent.Geometric.Points.add(points.ToArray());
            //mapLocationComponent.Geometric.Points.upload(points, true, symbol);



            //await mapLocationComponent.movePoint(coordinates[itemList.Count - 1]);


            //await mapLocationComponent.Geometric.Points.add(points.ToArray());



            //await mapLocationComponent.movePoint(coordinates[itemList.Count - 1]);

            //mapLocationComponent.Parameters.zoom_level = 18;

            //mapLocationComponent.Geometric.Points.Appearance().points = points;

            mapLocationComponent.Geometric.Points.Appearance().pattern = new RealTimeMap.PointSymbol()
            {
                color = "black",
                fillColor = "red",
                fillOpacity = 0.5,
                radius = 10,
            };

            //mapLocationComponent.Geometric.DisplayPolygonsFromArray.add(coordinates, polySymbol);

            mapLocationComponent.movePoint(coordinates[coordinates.Count - 1]);

            mapLocationComponent.Geometric.Points.AppearanceOnType().refresh();

            InvokeAsync(StateHasChanged);
        }


        /// <summary>
        /// ����� ����
        /// </summary>
        /// <param name = ""></param>
        private async Task OnSelectedWkplChanged(WkplmstModel wkplData)
        {
            selectedWkpl = wkplData;

            if(!(selectedWkpl.REFDA1?.Equals("sys", StringComparison.OrdinalIgnoreCase)??false))
                ShreDocUtility.LastSlectedWkpl = selectedWkpl;
            
            await Reload(false);
        }

        /// <summary>
        /// ��ü�ڷ�
        /// </summary>
        /// <param name = "isChecked"></param>
        private async void ShowAllDataChanged(bool isChecked)
        {
            ShowAllData = isChecked;
            if (ShowAllData)
                await Reload(false);
        }

        /// <summary>
        /// EXCEL Download
        /// </summary>
        private async Task OnDownLoadXLS()
        {
            try
            {
                await MonitoringGrid!.ExportToXlsxAsync("Moitoringlist.xlsx", new GridXlExportOptions()
                {
                    ExportDisplayText = true,
                    CustomizeCell = OnCustomizeCell
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}.OnDownLoadXLS: {ex.Message}");
                Modal.Alert($"{ex.Message}(OnDownLoadXLS)", RES.STR_ERROR);
            }
        }

        private void selectRowChange(MonitoringModel monitoringModel)
        {
            if(selectedMonitoringModel != null)
                selectConfno = selectedMonitoringModel.CONFNO;
        }


        /// <summary>
        /// Cell Ŀ���� 
        /// </summary>
        /// <param name="args"></param>
        void OnCustomizeCell(GridExportCustomizeCellEventArgs args)
        {
            if (args.ColumnFieldName == nameof(WorkerModel._Role) && args.AreaType == SheetAreaType.DataArea)
            {
                args.Value = roles!.FirstOrDefault(a => a.code == (args.Value as string))?.name ?? args.Value;
                args.Handled = true;
            }
            else if (args.ColumnFieldName == nameof(WorkerModel._Permission) && args.AreaType == SheetAreaType.DataArea)
            {
                args.Value = permissions!.FirstOrDefault(a => a.code == (args.Value as string))?.name ?? args.Value;
                args.Handled = true;
            }
        }

        /// <summary>
        /// Row�� �������� �Ѵ�.
        /// </summary>
        /// <param name = "colName"></param>
        /// <param name = "dataItem"></param>
        /// <param name = "newValue"></param>
        private async Task OnSuperVisorChanged(string colName, WorkerModel dataItem, bool newValue)
        {
            await ShreDocUtilService.UpdateRnakPosition(colName, dataItem, newValue);
        }

        //----------------------------------------------------------
    }
}