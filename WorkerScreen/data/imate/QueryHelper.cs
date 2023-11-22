using WorkerScreen.Models;
using WorkerScreen.data;
using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using XNSC.DD.EX;

namespace WorkerScreen.imate
{
    static internal class QueryHelper
    {
        internal static string DB_NAME = Preferences.Get("DB_NAME", "istnuser");
        internal static string SUCCESS = "OK";
        internal static string DATA_NOT_FOUND = "DATA_NOT_FOUND";
        internal static string NOT_NETWORK = "NOT_NETWORK";
        internal static List<QueryMessage> Init(User user) => new List<QueryMessage>(new QueryMessage[] {
            GetCodeMaster(),
            GetAreas(user.CRTUSR),
            GetEmployees(user.CRTUSR)
        });
        internal static List<QueryMessage> UserCheck(string id) => new List<QueryMessage>(new QueryMessage[] { MakeQuery_OneParam(nameof(UserCheck), id) });

        internal static List<QueryMessage> SearchRequests(string param, string param2) => new List<QueryMessage>(new QueryMessage[] { MakeQuery_TwoParam(nameof(SearchRequests), param, param2) });
        internal static List<QueryMessage> SearchLocksAllOffline(string param) => new List<QueryMessage>(new QueryMessage[] { MakeQuery_OneParam(nameof(SearchLocksAllOffline), param) });
        internal static List<QueryMessage> SearchLocks(User user, string param) => new List<QueryMessage>(new QueryMessage[] { MakeQuery_TwoParam(nameof(SearchLocks), user.EmpNo, param) });
        internal static List<QueryMessage> GetLockOfflineKeys(User user, string param) => new List<QueryMessage>(new QueryMessage[] { MakeQuery_OneParam(nameof(GetLockOfflineKeys), param) });
        internal static List<QueryMessage> GetLockInfo(string lsn, string param)
        {
            if (param.Equals("1 = 1"))
                return new List<QueryMessage>(new QueryMessage[] { MakeQuery_TwoParam("GetLockInfo_limit", lsn, param) });
            else
                return new List<QueryMessage>(new QueryMessage[] { MakeQuery_TwoParam(nameof(GetLockInfo), lsn, param) });

        }

        internal static QueryMessage GetCodeMaster() => MakeQuery(nameof(GetCodeMaster));
        internal static QueryMessage GetAreas(string officeID) => MakeQuery_OneParam(nameof(GetAreas), officeID);
        internal static QueryMessage GetEmployees(string officeID) => MakeQuery_OneParam(nameof(GetEmployees), officeID);

        private static QueryMessage MakeQuery(string query) =>
            new QueryMessage()
            {
                dataSource = DB_NAME,
                queryMethod = QueryRunMethod.Alone,
                queryName = query,
            };

        private static QueryMessage MakeQuery_OneParam(string query, string param1) =>
           new QueryMessage()
           {
               dataSource = DB_NAME,
               queryMethod = QueryRunMethod.Alone,
               queryName = query,
           };

        private static QueryMessage MakeQuery_TwoParam(string query, string param1, string param2) =>
            new QueryMessage()
            {
                dataSource = DB_NAME,
                queryMethod = QueryRunMethod.Alone,
                queryName = query,
            };
        private static QueryMessage MakeQuery_ThreeParam(string query, string param1, string param2, string param3) =>
            new QueryMessage()
            {
                dataSource = DB_NAME,
                queryMethod = QueryRunMethod.Alone,
                queryName = query,
            };
        private static QueryMessage MakeQuery_FourParam(string query, string param1, string param2, string param3, string param4) =>
            new QueryMessage()
            {
                dataSource = DB_NAME,
                queryMethod = QueryRunMethod.Alone,
                queryName = query,
            };

    }
}
