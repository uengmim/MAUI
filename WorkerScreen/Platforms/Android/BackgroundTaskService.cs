using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WorkerScreen.IBackgroundTaskService;
[assembly: Dependency(typeof(BackgroundTaskService))]

namespace WorkerScreen
{
    public interface IBackgroundTaskService
    {
        public class BackgroundTaskService : IBackgroundTaskService
        {
            public void StartBackgroundTask()
            {
                var intent = new Intent(Android.App.Application.Context, typeof(MyBackgroundService));
                Android.App.Application.Context.StartService(intent);
            }

            public void StopBackgroundTask()
            {
                var intent = new Intent(Android.App.Application.Context, typeof(MyBackgroundService));
                Android.App.Application.Context.StopService(intent);
            }
        }
    }
}
