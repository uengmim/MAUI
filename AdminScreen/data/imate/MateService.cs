//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Threading;

//namespace AdminScreen.data.imate
//{
//    public class MateService : IMateService
//    {

//        /// <summary>
//        /// Smart LOCK 공용 사용자
//        /// </summary>
//        public const string TTLOCK_USER = "ttlock";

//        public async Task<Result<User>> UserCheck(string id)
//        {
//            Result<User> result = new Result<User>();
//            try
//            {
//                User user = new User();
//                DataSet dataSet = await App.Adapter.DbSelectToDataSetAsync(QueryHelper.UserCheck(id));

//                if (dataSet == null)
//                {
//                    result.Message = QueryHelper.DATA_NOT_FOUND;
//                    return result;
//                }

//                var row = dataSet.Tables[nameof(QueryHelper.UserCheck)].Rows[0];
//                user.ID = row["EMPNO"] as string;
//                user.Name = row["EMPNM"] as string;
//                user.Team = row["DEPTNM"] as string;
//                user.TeamID = row["DEPTID"] as string;
//                user.OfficeID = row["WKPL"] as string;
//                user.WorkPlace = row["WKPLNM"] as string;
//                user.Pin = row["PIN"] as string;
//                user.IsAdmin = row["RANK"] as string == "R1";
//                user.IsAudit = row["RANK"] as string == "A1";
//                user.IsSysAdmin = row["POSITION"] as string == "S1";
//                user.Status = row["RECSTA"] as string;
//                result.Message = QueryHelper.SUCCESS;
//                result.Success = user;

//                return result;
//            }
//            catch (Exception ex)
//            {
//                result.Error = ex;
//                result.Message = App.isNetwork
//                            ? ex.Message.Contains("There is no row at position 0.") ? QueryHelper.DATA_NOT_FOUND : ex.Message
//                            : QueryHelper.NOT_NETWORK;
//                return result;
//            }
//        }

//    }
//}
