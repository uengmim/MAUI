using Mapsui.Projections;
using Mapsui.UI.Maui;
using Color = Microsoft.Maui.Graphics.Color;
using Mapsui;
using Mapsui.Extensions;
using CommunityToolkit.Maui.Alerts;
using ShreDoc.Utils;
using XNSC.DD.EX;
using ShreDoc.ProxyModel;
using XNSC;

namespace AdminScreen.Views
{
    public partial class HistoryMapPage : ContentPage
    {
        public Location location;
        MapControl mapControl = new MapControl();

        public HistoryMapPage(string confno)
        {
            InitializeComponent();
            mapViewElement.PinClicked += OnPinClicked;

            location = new();
            DrawMap(confno);
        }

        public async void DrawMap(string confno)
        {

            try
            {
                var dataService = ImateHelper.GetSingleTone();
                //TKSEG 조회
                var whereCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                        new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = confno, condition = DIMWhereCondition.Equal}
                    }
                };
                Dictionary<string, DIMSortOrder> sorts = new Dictionary<string, DIMSortOrder>();
                sorts.Add("CRTDT", DIMSortOrder.Ascending);

                var tksegInfoList = await dataService.Adapter.SelectModelDataAsync<TksegModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.TksegModelList",
                                        whereCondition, sorts, QueryCacheType.None);

                var Lon = tksegInfoList[0].LON.ToString();
                var Lat = tksegInfoList[0].LAT.ToString();

                double LonData = Double.Parse(Lon);
                double LatData = Double.Parse(Lat);

                //확대 및 중앙
                var centerOfLondonOntario = new MPoint((double)LonData, (double)LatData);
                var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(centerOfLondonOntario.X, centerOfLondonOntario.Y).ToMPoint();
                mapControl.Map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, n.Resolutions[13]);

                //맵 추가
                mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
                mapViewElement.Map = mapControl.Map;

                //지도 회전 금지
                mapControl.Map.Navigator.RotationLock = true;

                //폴리라인
                var line = new Mapsui.UI.Maui.Polyline { StrokeWidth = 3, StrokeColor = Mapsui.UI.Maui.KnownColor.Red, IsClickable = true };

                var reptyp = "";
                foreach (var item in tksegInfoList)
                {

                    if (item.REPTYP == "C08000A")
                        reptyp = "봉인";
                    else if (item.REPTYP == "C08000B")
                        reptyp = "상차";
                    else if (item.REPTYP == "Trace")
                        reptyp = "이동 중..";
                    else if (item.REPTYP == "C08000C")
                        reptyp = "하차";
                    else if (item.REPTYP == "C08000D")
                        reptyp = "봉인 해제";
                    else if (item.REPTYP == "C08000E")
                        reptyp = "파쇄";

                    Lon = item.LON.ToString();
                    Lat = item.LAT.ToString();

                    LonData = Double.Parse(Lon);
                    LatData = Double.Parse(Lat);

                    if (item.REPTYP == "C08000A" || item.REPTYP == "C08000B" || item.REPTYP == "Trace" || item.REPTYP == "C08000C" || item.REPTYP == "C08000D" || item.REPTYP == "C08000E")
                    {
                        //핀 추가
                        AddPin((double)LatData, (double)LonData, Colors.Blue, item.EVTDT, reptyp);
                        line.Positions.Add(new Position((double)LatData, (double)LonData));
                    }

                }
                mapViewElement.Drawables.Add(line);
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
        }

        /// <summary>
        /// 핀 추가 이벤트
        /// </summary>
        public void AddPin(double latitude, double longitude, Color c, DateTime? PinTime, string RepTYP)
        {

            string formattedDate = PinTime.ToString();


            var myPin = new Pin(mapViewElement)
            {
                Position = new Position(latitude, longitude),
                Type = PinType.Pin,
                Label = formattedDate,
                Address = "more text",
                IsVisible = true,
                Scale = 0.8F,
                Color = c,
            };
            myPin.Callout.Title = formattedDate;
            myPin.Callout.Subtitle = RepTYP;
            myPin.Callout.Content = 1;
            mapViewElement.Pins.Add(myPin);
        }


        private object loadingObj = new object();
        private bool isLoading;

        /// <summary>
        /// 핀 클릭 이벤트
        /// </summary>
        private async void OnPinClicked(object sender, PinClickedEventArgs e)
        {
            // isLoading 변수를 사용하여 중복 클릭 방지
            if (isLoading)
                return;

            // 핀 클릭 상태를 표시하기 위해 isLoading을 true로 설정
            isLoading = true;

            try
            {
                string message = $"발생 시간 : {e.Pin.Callout.Title} \n유형 : {e.Pin.Callout.Subtitle} ";

                // 토스트 메시지 내용을 설정
                toastLabel.Text = message;

                // 토스트 메시지 표시
                toastMessage.IsVisible = true;
                await toastMessage.FadeTo(1, 250);

                // 토스트 메시지 표시 후 1.5초 대기
                await Task.Delay(1500);

                // 토스트 메시지 닫기
                await toastMessage.FadeTo(0, 250);
                toastMessage.IsVisible = false;
            }
            finally
            {
                // isLoading 변수를 false로 설정하여 다시 핀 클릭을 활성화
                isLoading = false;
            }
        }

        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }

        // 팝업 표시 상태를 나타내는 플래그
        private bool isAlertShowing = false;

        //팝업
        private async Task ShowCustomAlert(string title, string message, string accept, string cancle)
        {
            // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
            if (isAlertShowing)
            {
                return;
            }

            isAlertShowing = true; // 경고 팝업 표시 중임을 표시

            // 팝업 애니메이션 비활성화
            try
            {
                var alertPage = new CustomAlertPage(title, message, accept, cancle);
                alertPage.Disappearing += (sender, e) => isAlertShowing = false;
                await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);
            }
            finally
            {
                isAlertShowing = true;
            }
        }
    }
}


