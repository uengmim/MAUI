using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using SmartLock.TT.Common;
using System.ComponentModel;
using XNSC.DD.EX;

namespace AdminScreen.Models
{
    /// <summary>
    /// LOCK ITEM
    /// </summary>
    public class LockInfomation : INotifyPropertyChanged
    {
        /// <summary>
        /// Lock 이름
        /// </summary>
        private string lockName;

        /// <summary>
        /// Lock Mac 어드레스
        /// </summary>
        private string lockMac;

        /// <summary>
        /// Lock Id
        /// </summary>
        private string lockid;

        /// <summary>
        /// Lock Id
        /// </summary>
        private string worker;

        /// <summary>
        /// lock 이름
        /// </summary>
        public string LockName { get { return lockName; } set { lockName = value; OnPropertyChanged(nameof(LockName)); } }

        /// <summary>
        /// lock MacAddress
        /// </summary>
        public string LockMac { get { return lockMac; } set { lockMac = value; OnPropertyChanged(nameof(LockMac)); } }

        /// <summary>
        /// lock MacAddress
        /// </summary>
        public string Lockid { get { return lockid; } set { lockid = value; OnPropertyChanged(nameof(Lockid)); } }

        /// <summary>
        /// lock MacAddress
        /// </summary>
        public string Worker { get { return worker; } set { worker = value; OnPropertyChanged(nameof(Worker)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public LockInfomation(LockDevice device)
        {
            LockName = device.Name;
            LockMac = device.Address;
            Worker = string.Empty;
        }

        /// <summary>
        /// 특성 변경 이벤드
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName">프로퍼티 이름</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="args">특성변경 인수</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Lock 정보를 셋팅 한다.
        /// </summary>
        public async void SetLockInfo()
        {
            var whereLKCondition = new DIMGroupFieldCondtion()
            {
                condition = DIMGroupCondtion.AND,
                joinCondtion = DIMGroupCondtion.AND,
                whereFieldConditions = new DIMWhereFieldCondition[]
                {
                    new DIMWhereFieldCondition{ fieldName = "MAC" , value = lockMac, condition = DIMWhereCondition.Equal}
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