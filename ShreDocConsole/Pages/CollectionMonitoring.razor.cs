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
    /// 회수팀 및 회수차량 위치 실시간 모니터링
    /// </summary>
    public partial class CollectionMonitoring
    {
        /// <summary>
        /// 디테일오픈
        /// </summary>
        bool AutoCollapseDetailRow { get; set; }

        /// <summary>
        /// 헤더 Grid Data 참조
        /// </summary>
        private IGrid? MonitoringGrid;

        /// <summary>
        /// 아이템 Grid Data 참조
        /// </summary>
        private IGrid? MonitoringItemGrid;

        //------------------------------------------------------------
        /// <summary>
        /// 헤더 Api 정보
        /// </summary>
        private MonitoringModelList? MonitoringHeaderList;

        /// <summary>
        /// 아이템 Api 정보
        /// </summary>
        private MonitoringItemModelList? MonitoringItemList;

        /// <summary>
        /// 서머리 Api 정보
        /// </summary>
        private MonitoringSummaryModelList? MonitoringSummary;

        //--------------------------------------------------------------

        /// <summary>
        /// 선택된 부서와 하위 모델과 하위 리스트
        /// </summary>
        private List<DeptmstModel>? deptDatas = null;

        /// <summary>
        /// 부서 리스트
        /// </summary>
        private DeptmstModelList? deptDatList = null;

        /// <summary>
        /// 사원 역활 대상 모델 리스트
        /// </summary>
        private EmployeeRoleTargetModelList? empRoleTargetModelList = null;

        /// <summary>
        /// 개별 역활 대상 리스트
        /// </summary>
        private IList<EmployeeRoleTargetModel>? groupTargetList = null;

        /// <summary>
        /// 개별 역활 대상 리스트
        /// </summary>
        private IList<EmployeeRoleTargetModel>? indivTargetList = null;

        //------------------------------------------------------------------------

        /// <summary>
        /// 부거 유형 정보
        /// </summary>
        private IEnumerable<CdmstModel>? deptTypeDatas = null;

        /// <summary>
        /// 레벨 정보
        /// </summary>
        private IEnumerable<CdmstModel>? ilsTypes = null;

        /// <summary>
        /// 역활(Rank)
        /// </summary>
        private IEnumerable<CodeInfo> roles = ShreDocUtility.Roles();

        /// <summary>
        /// 권한(Permission)
        /// </summary>
        private IEnumerable<CodeInfo> permissions = ShreDocUtility.Permissions();


        /// <summary>
        /// 역활(Rank)
        /// </summary>
        private IEnumerable<CodeInfo> rolesRank = ShreDocUtility.RolesRank();


        /// <summary>
        /// 권한(Permission)
        /// </summary>
        private IEnumerable<CodeInfo> rolePermissions = ShreDocUtility.Permissions("");

        /// <summary>
        /// 필터 정보
        /// </summary>
        private IEnumerable<CodeInfo> selCmdFilterOption = ShreDocUtility.SelectCommandFilterOption();

        /// <summary>
        /// Prefix 보이기
        /// </summary>
        private bool prefixVisible = true;
        /// <summary>
        /// 편집모드
        /// </summary>
        private bool editMode = false;
        /// <summary>
        /// 순번 커럼 SPAN
        /// </summary>
        private int seqColSpan = 4;

        //----------------------------------------------------------------------

        /// <summary>
        /// 출입가능 작업자
        /// </summary>
        private bool ShowActiveData { get; set; } = true;
        /// <summary>
        /// 출입제한 작업자
        /// </summary>
        private bool ShowDeactiveData { get; set; } = false;
        /// <summary>
        /// 모든 자료
        /// </summary>
        private bool ShowAllData { get; set; } = false;

        /// <summary>
        /// POPUP Edit 제목
        /// </summary>
        private string EditPopupHeaderText = "";

        /// <summary>
        /// 선택한 Tab
        /// </summary>
        private int ActiveTabIndex { get; set; } = 0;

        /// <summary>
        /// 콤보박스 데이터
        /// </summary>
        /// <returns></returns>
        private WkplmstModelList? wkplDatas = null;
        /// <summary>
        /// 선택한 지역
        /// </summary>
        private WkplmstModel? selectedWkpl = null;

        /// <summary>
        /// 선택한 시작날짜
        /// </summary>
        private DateTime selectedSDate = DateTime.Now.AddDays(-7);

        /// <summary>
        /// 선택한 종료날짜
        /// </summary>
        private DateTime selectedEDate = DateTime.Now;

        /// <summary>
        /// 선택된 작업자(사원)
        /// </summary>
        private WorkerModel? selectedWorker = null;

        /// <summary>
        /// 작업장 컬럼 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        private string GetWorkplaceCaption(string property) => ImateAdapterUtility.GetXNDisplayName<WkplmstModel>(property) ?? property;

        /// <summary>
        /// 컬럼 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetWorkerEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<WorkerFormEditContext>(property);

        /// <summary>
        /// 컬럼 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetDeptCaption(string property) => ImateAdapterUtility.GetXNDisplayName<DeptmstModel>(property);

        /// <summary>
        /// 컬럼 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetRoleEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<EmployeeRoleEditModel>(property);

        /// <summary>
        /// 컬럼 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetRoleTargetEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<EmployeeRoleTargetModel>(property);


        /// <summary>
        /// 컬럼 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetCaption(string property) => ImateAdapterUtility.GetXNDisplayName<MonitoringModel>(property);

        /// <summary>
        /// 서머리 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetSummaryCaption(string property) => ImateAdapterUtility.GetXNDisplayName<MonitoringSummaryModel>(property);

        /// <summary>
        /// Role(Rank)이름
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetRoleName(WorkerModel dataItem) => rolesRank.FirstOrDefault(r => r.code == dataItem?._Role)?.name ?? "";

        /// <summary>
        /// Permision(Position)이름
        /// </summary>
        /// <param name="dataItem"></param>
        /// <returns></returns>
        public string GetPermissionName(WorkerModel dataItem) => permissions.FirstOrDefault(r => r.code == dataItem?._Permission)?.name ?? "";

        //// <summary>
        /// 부서유형을 부서유형명
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private string GetDeptTypeName(WorkerModel worker) => deptTypeDatas?.FirstOrDefault(d => d.CODE == worker?.depttyp)?.CDNM ?? "";

        //// <summary>
        /// 부서유형을 부부서유형명
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        private string GetIlsTypeName(EmployeeRoleTargetModel ils) => ilsTypes?.FirstOrDefault(d => d.CODE == ils?.ilstype)?.CDNM ?? "";

        /// <summary>
        /// 컬럼 이름(캡션)
        /// </summary>
        /// <param name = "property"></param>
        /// <returns></returns>
        public string GetWorkerSafeEditCaption(string property) => ImateAdapterUtility.GetXNDisplayName<WorkerSafeFormEditContext>(property);

        /// <summary>
        /// 이수 상태
        /// </summary>
        private IEnumerable<CodeInfo> certificationStatus =ShreDocUtility.CertificationStatus();

        /// <summary>
        /// 사용자 ID
        /// </summary>
        private string userId = "";

        /// <summary>
        /// 선택된 작업자
        /// </summary>
        private WorkerModel? selectedWorkerModel = null;

        private MonitoringModel? selectedMonitoringModel = null;

        private string selectConfno = "";

        /// <summary>
        /// 안전관리 메뉴 존재 여부
        /// </summary>
        private bool existsSafeMgr = false;

        /// <summary>
        /// 지도 위치 팝업 오픈 여부
        /// </summary>
        private bool mapLocationPopupVisible = false;

        /// <summary>
        /// 지도위치 컴포넌트
        /// </summary>
        private RealTimeMap mapLocationComponent { get; set; }

        /// <summary>
        /// 지도위치 컴포넌트 파라미터
        /// </summary>
        private RealTimeMap.LoadParameters mapParam { get; set; } = new RealTimeMap.LoadParameters();

        /// <summary>
        /// 지도위치 포인트 심볼
        /// </summary>
        private RealTimeMap.PointSymbol symbol { get; set; }

        /// <summary>
        /// 모달 서비스 객체
        /// </summary>
        [CascadingParameter] IModalService Modal { get; set; } = default!;

        /// <summary>
        /// 봉인수
        /// </summary>
        private string SummaryA { get; set; } = "";

        /// <summary>
        /// 상차수
        /// </summary>
        private string SummaryB { get; set; } = "";

        /// <summary>
        /// 하차수
        /// </summary>
        private string SummaryC { get; set; } = "";

        /// <summary>
        /// 봉인해제수
        /// </summary>
        private string SummaryD { get; set; } = "";

        /// <summary>
        /// 파쇄수
        /// </summary>
        private string SummaryE { get; set; } = "";

        /// <summary>
        /// 화면 비율(PC/모바일) 구분
        /// </summary>
        private bool isXSmallScreen { get; set; }

        /// <summary>
        /// 모니터링 맵 넓이 문자열
        /// </summary>
        private string mapWidth { get; set; } = "";

        /// <summary>
        /// 초기화
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //로그인 식별자
            var imateAuthStateProvider = (ImateAuthStateProvider)AuthenticationStateProvider;
            var authState = imateAuthStateProvider.CurrentAuthenticationState??imateAuthStateProvider.UnAuthenticationState;
            
            //사용자 ID
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
                    //사업소
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
                //            title = "FCM테스트",
                //            body = "테스트테스트"
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
                    value = "테스트",
                    template = "",
                    lineTerminateChar = "",
                    prefix = "",
                    surfix = ""
                };

                var messageParam = new QueryParameter()
                {
                    name = "message",
                    dataType = QueryDataType.String,
                    value = "테스트메시지",
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
                    Modal.Alert($"전송성공 // {dt.Rows[0].Field<string>("MESSAGE")}", RES.STR_CONFIRM);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}.CFTest: {ex.Message}");
                Modal.Alert($"{ex.Message}(CFTest)", RES.STR_ERROR);
            }
        }


        private async void OnGirdMapBtnClicked(MonitoringModel selectRow)
        {
            //Modal.Alert(selectRow.CONFNO, "클릭");
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
        /// 사업소 변경
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
        /// 전체자료
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
        /// Cell 커스텀 
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
        /// Row를 업데이터 한다.
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