using AdminScreen.ViewModels;
using Mapsui.Projections;
using Mapsui.UI.Maui;
using Color = Microsoft.Maui.Graphics.Color;
using System.Diagnostics;
using Mapsui;
using Mapsui.Extensions;
using System.Threading.Tasks;
using Mapsui.Widgets.Zoom;
using static Google.Android.Material.Tabs.TabLayout;
using Mapsui.UI;
using Mapsui.Layers;
using Mapsui.Providers;
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
                sorts.Add("REPTYP", DIMSortOrder.Ascending);

                var tksegInfoList = await dataService.Adapter.SelectModelDataAsync<TksegModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.TksegModelList",
                                        whereCondition, sorts, QueryCacheType.None);

                var Lon = tksegInfoList[0].LON.ToString();
                var Lat = tksegInfoList[0].LAT.ToString();

                double LonData = Double.Parse(Lon);
                double LatData = Double.Parse(Lat);

                //확대 및 중앙
                var centerOfLondonOntario = new MPoint((double)LonData, (double)LatData);
                var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(centerOfLondonOntario.X, centerOfLondonOntario.Y).ToMPoint();
                mapControl.Map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, n.Resolutions[10]);

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

                    if (item.REPTYP == "C08000A" || item.REPTYP == "C08000B" || item.REPTYP == "C08000C" || item.REPTYP == "C08000D" || item.REPTYP == "C08000E")
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
                await Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
            }
        }

        //핀 추가 이벤트
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
                Scale = 0.7F,
                Color = c,
            };
            myPin.Callout.Title = formattedDate;
            myPin.Callout.Subtitle = RepTYP;
            myPin.Callout.Content = 1;
            mapViewElement.Pins.Add(myPin);
        }


        private object loadingObj = new object();
        private bool isLoading;

        private void OnPinClicked(object sender, PinClickedEventArgs e)
        {

            lock (loadingObj)
            {
                if (isLoading == true)
                    return;
                isLoading = true;
            }
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var toast = Toast.Make($"발생 시간 : {e.Pin.Callout.Title} \n유형 : {e.Pin.Callout.Subtitle} ", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            toast.Show(cancellationTokenSource.Token);
            isLoading = false;
        }
    }
}

