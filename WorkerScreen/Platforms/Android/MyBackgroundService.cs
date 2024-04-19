using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Timers;
using XNSC.DD.EX;

namespace WorkerScreen
{
    [Service(ForegroundServiceType = ForegroundService.TypeLocation)]
    public class MyBackgroundService : Service
    {
        const int ALARM_REQUEST_CODE = 0;
        const int NOTIFICATION_ID = 1;

        const string NOTIFICATION_CHANNEL_ID = "7424";
        const string NOTIFICATION_CHANNEL_NAME = "LocationService";

        //private bool isStop = false;
        //private ManualResetEventSlim WaitEvent = new ManualResetEventSlim(false);

        public static string CONFNO
        {
            get; set;
        }
        public static string EMPNO
        {
            get; set;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        /// <summary>
        /// BackGround 이벤트
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="flags"></param>
        /// <param name="startId"></param>
        /// <returns></returns>
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            CONFNO = intent.GetStringExtra("CONFNO");
            EMPNO = intent.GetStringExtra("EMPNO");

            //var notiManager = GetSystemService(Context.NotificationService) as NotificationManager;

            ////노티임포턴스는 팝업과 소리알림까지 하려면 MAX로 바꾸면 됨
            //var channel = new NotificationChannel(NOTIFICATION_CHANNEL_ID, NOTIFICATION_CHANNEL_NAME, NotificationImportance.Default);
            //notiManager.CreateNotificationChannel(channel);

            //// Foreground service용 알림 생성
            //var notification = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID)
            //    .SetContentTitle("My Background Service")
            //    .SetContentText("Running...")
            //    .SetOngoing(true)
            //    .SetAutoCancel(false)
            //    .Build();

            //// 서비스를 foreground로 전환
            //StartForeground(NOTIFICATION_ID, notification);

            // AlarmManager를 설정합니다.
            var alarmIntent = new Intent(this, typeof(AlarmReceiver));
            alarmIntent.PutExtra("CONFNO", CONFNO);
            alarmIntent.PutExtra("EMPNO", EMPNO);
            var pendingIntent = PendingIntent.GetBroadcast(this, ALARM_REQUEST_CODE, alarmIntent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

            var alarmManager = (AlarmManager)GetSystemService(AlarmService);

            // 5초마다 알람을 반복합니다.
            var interval = 5000L;

            alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, SystemClock.ElapsedRealtime() + interval, interval, pendingIntent);

            //// 여기에서 백그라운드 작업을 수행합니다.
            //_ = TrackLocation();

            return StartCommandResult.Sticky;
        }

        ///// <summary>
        ///// 타이머 이벤트(위치 추적 전송)
        ///// </summary>
        //async Task TrackLocation()
        //{
        //    while (!isStop)
        //    {
        //        try
        //        {
        //            WaitEvent.Wait(30000);
        //            if (isStop)
        //                break;

        //            //수행할 타이머 이벤트
        //            var dataService = ImateHelper.GetSingleTone();
        //            DateTime currentDate = DateTime.Now;

        //            //TTLOCK
        //            string ttGuidStr = Guid.NewGuid().ToString();
        //            var location = await Geolocation.GetLastKnownLocationAsync();
        //            if (location == null)
        //            {
        //                location = await Geolocation.GetLocationAsync(new GeolocationRequest()
        //                {
        //                    DesiredAccuracy = GeolocationAccuracy.High,
        //                    Timeout = TimeSpan.FromSeconds(30)
        //                });
        //            }

        //            //TKSEG 조회
        //            var whereCondition = new DIMGroupFieldCondtion()
        //            {
        //                condition = DIMGroupCondtion.AND,
        //                joinCondtion = DIMGroupCondtion.AND,
        //                whereFieldConditions = new DIMWhereFieldCondition[]
        //{
        //            new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = CONFNO, condition = DIMWhereCondition.Equal},
        //            new DIMWhereFieldCondition{ fieldName = "REPTYP" , value = "C08000B", condition = DIMWhereCondition.Equal},
        //}
        //            };
        //            var tksegInfoList = await ImateHelper.SelectModelData<TksegModelList>(App.ServerID, whereCondition, new Dictionary<string, XNSC.DIMSortOrder>());

        //            dataService.Ttlock.ActiveLog(new XNSC.Net.NOKE.LockActivity()
        //            {
        //                trackingKey = ttGuidStr,
        //                lockSN = tksegInfoList[0].LSN,
        //                ilsId = tksegInfoList[0].ILSID,
        //                eventTime = currentDate,
        //                userId = EMPNO,
        //                ConfirmNo = tksegInfoList[0].CONFNO,
        //                ReportType = "Trace",
        //                lockStatus = "L",
        //                Longitude = location.Longitude,
        //                Latitude = location.Latitude,
        //                opeationMode = "ON",
        //                connectTime = currentDate,
        //            });
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error("Error", ex.Message);
        //        }
        //        finally
        //        {
        //            WaitEvent.Reset();
        //        }
        //    }
        //}

        /// <summary>
        /// 타이머 중단
        /// </summary>
        public override void OnDestroy()
        {
            //isStop = true;
            //WaitEvent.Set();

            base.OnDestroy();
            StopAlarm();
        }

        public void StopAlarm()
        {
            var alarmIntent = new Intent(this, typeof(AlarmReceiver));
            var pendingIntent = PendingIntent.GetBroadcast(this, ALARM_REQUEST_CODE, alarmIntent, PendingIntentFlags.NoCreate | PendingIntentFlags.Immutable);

            if (pendingIntent != null)
            {
                var alarmManager = (AlarmManager)GetSystemService(AlarmService);
                alarmManager.Cancel(pendingIntent);
            }
        }
    }
}
