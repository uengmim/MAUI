// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "TtlockKeyInfo" -output "TtlockKeyInfoProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;
using XNSC.DD.EX;

namespace ShreDoc.ProxyModel
{
/// <summary>
    /// AreamstModel(areamst) Proxy class
    /// </summary>
    public class TtlockKeyInfoModel
    {
        /// <summary>
        /// Ekey ID
        /// </summary>
        public int keyId { get; set; }
        /// <summary>
        /// Lock data, used to operate lock
        /// </summary>
        public string lockData { get; set; }
        /// <summary>
        /// Lock ID
        /// </summary>
        /// <remarks>
        /// https://euopen.ttlock.com/document/doc?urlName=cloud%2Flock%2FinitializeEn.html
        /// </remarks>
        public int lockId { get; set; }
        /// <summary>
        /// Ekey type:110301-admin ekey, 110302-common user ekey
        /// </summary>
        public string userType { get; set; }
        /// <summary>
        /// 키 상태
        /// </summary>
        /// <remarks>
        /// https://euopen.ttlock.com/document/doc?urlName=cloud%2Fekey%2FstatusEn.html
        /// 110401 Normal, 110402 Pending, 110405 Freezed, 110408 Deleted, 110410 Reseted
        /// </remarks>
        public string keyStatus { get; set; }
        /// <summary>
        /// Lock name
        /// </summary>
        public string lockName { get; set; }
        /// <summary>
        /// Lock alias
        /// </summary>
        public string lockAlias { get; set; }
        /// <summary>
        /// Lock MAC
        /// </summary>
        public string lockMac { get; set; }
        /// <summary>
        /// Super passcode, which only belongs to the admin ekey, can be entered on the keypad to unlock
        /// </summary>
        public string noKeyPwd { get; set; }
        /// <summary>
        /// Lock battery
        /// </summary>
        public int electricQuantity { get; set; }
        /// <summary>
        /// The time when it becomes valid (timestamp in millisecond)
        /// </summary>
        public long startDate { get; set; }
        /// <summary>
        /// The time when it is expired (timestamp in millisecond)
        /// </summary>
        public long endDate { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        public string remarks { get; set; }
        /// <summary>
        /// Is ekey authorized: 0-NO, 1-yes
        /// </summary>
        public int keyRight { get; set; }
        /// <summary>
        /// LOCK 사양
        /// </summary>
        /// <remarks>
        /// https://euopen.ttlock.com/document/doc?urlName=cloud%2Flock%2FfeatureValueEn.html
        /// </remarks>
        public string featureValue { get; set; }
        /// <summary>
        /// Is remote unlock enabled: 1-yes,2-no
        /// </summary>
        public int remoteEnable { get; set; }
        /// <summary>
        /// Passage mode：1-enable、2-disable
        /// </summary>
        public int passageMode { get; set; }
        /// <summary>
        /// Group id
        /// </summary>
        public int groupId { get; set; }
        /// <summary>
        /// Group name
        /// </summary>
        public string groupName { get; set; }
    }

    /// <summary>
    /// AreamstModel(areamst) Proxy List Class
    /// </summary>    
    public class TtlockKeyInfoModelList : List<TtlockKeyInfoModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public TtlockKeyInfoModelList()
        {
            return;
        }
    }

}