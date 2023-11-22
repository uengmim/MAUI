using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerScreen.data;

namespace WorkerScreen.data.imate
{
    public interface IMateService
    {
        Task<Result<User>> UserCheck(string id);
    }
}