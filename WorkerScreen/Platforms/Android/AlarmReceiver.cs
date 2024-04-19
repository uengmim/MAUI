using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Timers;
using XNSC.DD.EX;

namespace WorkerScreen
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmReceiver : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            var CONFNO = intent.GetStringExtra("CONFNO");
            var EMPNO = intent.GetStringExtra("EMPNO");

            // 알람이 실행될 때 호출됩니다.
            // 여기에서 백그라운드 작업을 수행합니다.
            _ = TrackLocation(CONFNO, EMPNO);
        }
        /// <summary>
        /// 타이머 이벤트(위치 추적 전송)
        /// </summary>
        async Task TrackLocation(string CONFNO, string EMPNO)
        {
            //ManualResetEventSlim WaitEvent = new ManualResetEventSlim(false);
            //while (true)
            //{
                try
                {
                    //WaitEvent.Wait(10000);

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
                }
                catch (Exception ex)
                {
                    Log.Error("Error", ex.Message);
                }
            //    finally
            //    {
            //        WaitEvent.Reset();
            //    }
            //}
        }
    }
}
