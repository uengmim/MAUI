using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerScreen
{
    public interface IBackgroundTaskService
    {
        void StartBackgroundTask();
        void StopBackgroundTask();
    }
}
