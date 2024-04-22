using BackgroundTasks;
using Foundation;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using UIKit;
using XNSC.DD.EX;

namespace WorkerScreen;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    public static string CONFNO
    {
        get; set;
    }
    public static string EMPNO
    {
        get; set;
    }

    [Export("application:performFetchWithCompletionHandler:")]
    public override async void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
    {
        //수행할 타이머 이벤트
        var dataService = ImateHelper.GetSingleTone();
        DateTime currentDate = DateTime.Now;

        //TTLOCK
        string ttGuidStr = Guid.NewGuid().ToString();
        var location = await Geolocation.GetLastKnownLocationAsync();
        if (location == null)
        {
            location = await Geolocation.GetLocationAsync(new GeolocationRequest()
            {
                DesiredAccuracy = GeolocationAccuracy.High,
                Timeout = TimeSpan.FromSeconds(30)
            });
        }

        //TKSEG 조회
        var whereCondition = new DIMGroupFieldCondtion()
        {
            condition = DIMGroupCondtion.AND,
            joinCondtion = DIMGroupCondtion.AND,
            whereFieldConditions = new DIMWhereFieldCondition[]
            {
            new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = CONFNO, condition = DIMWhereCondition.Equal},
            new DIMWhereFieldCondition{ fieldName = "REPTYP" , value = "C08000B", condition = DIMWhereCondition.Equal},
            }
        };
        var tksegInfoList = await ImateHelper.SelectModelData<TksegModelList>(App.ServerID, whereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

        dataService.Ttlock.ActiveLog(new XNSC.Net.NOKE.LockActivity()
        {
            trackingKey = ttGuidStr,
            lockSN = tksegInfoList[0].LSN,
            ilsId = tksegInfoList[0].ILSID,
            eventTime = currentDate,
            userId = EMPNO,
            ConfirmNo = tksegInfoList[0].CONFNO,
            ReportType = "Trace",
            lockStatus = "L",
            Longitude = location.Longitude,
            Latitude = location.Latitude,
            opeationMode = "ON",
            connectTime = currentDate,
        });

        // 작업이 완료되면 completionHandler를 호출하여 작업의 결과를 시스템에 알립니다.
        completionHandler(UIBackgroundFetchResult.NewData);
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        BGTaskScheduler.Shared.Register("co.kr.istn.Worker.refresh", null, async task =>
        {
            //수행할 타이머 이벤트
            var dataService = ImateHelper.GetSingleTone();
            DateTime currentDate = DateTime.Now;

            //TTLOCK
            string ttGuidStr = Guid.NewGuid().ToString();
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                {
                    DesiredAccuracy = GeolocationAccuracy.High,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            //TKSEG 조회
            var whereCondition = new DIMGroupFieldCondtion()
            {
                condition = DIMGroupCondtion.AND,
                joinCondtion = DIMGroupCondtion.AND,
                whereFieldConditions = new DIMWhereFieldCondition[]
                {
            new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = CONFNO, condition = DIMWhereCondition.Equal},
            new DIMWhereFieldCondition{ fieldName = "REPTYP" , value = "C08000B", condition = DIMWhereCondition.Equal},
                }
            };
            var tksegInfoList = await ImateHelper.SelectModelData<TksegModelList>(App.ServerID, whereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

            dataService.Ttlock.ActiveLog(new XNSC.Net.NOKE.LockActivity()
            {
                trackingKey = ttGuidStr,
                lockSN = tksegInfoList[0].LSN,
                ilsId = tksegInfoList[0].ILSID,
                eventTime = currentDate,
                userId = EMPNO,
                ConfirmNo = tksegInfoList[0].CONFNO,
                ReportType = "Trace",
                lockStatus = "L",
                Longitude = location.Longitude,
                Latitude = location.Latitude,
                opeationMode = "ON",
                connectTime = currentDate,
            });

            // 작업이 완료되면 task.SetTaskCompleted 메서드를 호출하여 작업의 결과를 시스템에 알립니다.
            task.SetTaskCompleted(true);
        });

        var request = new BGAppRefreshTaskRequest("co.kr.istn.Worker.refresh")
        {
            EarliestBeginDate = (NSDate)DateTime.Now.AddMinutes(1)
        };

        BGTaskScheduler.Shared.Submit(request, out NSError error);

        return true;
    }

}
