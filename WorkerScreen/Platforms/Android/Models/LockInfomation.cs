using Com.Ttlock.BL.Sdk.Api;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;
using XNSC.Net;

namespace WorkerScreen.Models
{
    /// <summary>
    /// LOCK ITEM
    /// </summary>
    public partial class LockInfomation
    {
        /// <summary>
        /// Device 정보
        /// </summary>
        public ExtendedBluetoothDevice Device { get; private set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="device">TTlock Device 정보</param>
        public LockInfomation(ExtendedBluetoothDevice device)
        {
            Device = device;
        }

        /// <summary>
        /// Lock 정보 설정
        /// </summary>
        public async void SetLockInfo()
        {
            var whereLKCondition = new DIMGroupFieldCondtion()
            {
                condition = DIMGroupCondtion.AND,
                joinCondtion = DIMGroupCondtion.AND,
                whereFieldConditions = new DIMWhereFieldCondition[]
                {
                    new DIMWhereFieldCondition{ fieldName = "MAC" , value = Device.Address, condition = DIMWhereCondition.Equal}
                }
            };

            var dataService = ImateHelper.GetSingleTone().Adapter;
            var lkMstData = await dataService.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                        whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);
            if (lkMstData.Count > 0)
            {
                var lkInfo = lkMstData.First();

                LockMac = lkInfo.MAC;
                Lockid = lkInfo.LSN;
                LockName = lkInfo.LKNM;
                Worker = lkInfo.REFDA1 ?? "";
            }
        }
    }
}