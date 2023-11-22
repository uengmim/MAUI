// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "TtlockKeyInfo" -output "TtlockKeyInfoModel.cs" -nspace "ShreDoc.DataModel"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;


namespace ShreDoc.DataModel
{
    /// <summary>
    /// TtlockKeyInfoModel(TtlockKeyInfo) Model class
    /// </summary>
    [DataContract]
    public class TtlockKeyInfoModel : XNDIMModel
    {
        /// <summary>
        /// KEYID Field
        /// </summary>
        [DataMember(Name = "KEYID")]
        [Field(null, PrimeryKey = true )]
        public int? keyid
        {
            get => _keyid;
            set
            {
                var _prvkeyid = _keyid;
                _keyid = value; 
                OnChangedValue("keyid", value, _prvkeyid);
            }
        }
        public bool keyidChanged => GetChangedValue("keyid");
        public int? keyidOldValue => (int?)GetValue("keyid", ModelValueVersion.OldVersion);
        private int? _keyid = default(int?);

        /// <summary>
        /// LOCKDATA Field
        /// </summary>
        [DataMember(Name = "LOCKDATA")]
        [Field(null, PrimeryKey = false )]
        public string? lockdata
        {
            get => _lockdata;
            set
            {
                var _prvlockdata = _lockdata;
                _lockdata = value; 
                OnChangedValue("lockdata", value, _prvlockdata);
            }
        }
        public bool lockdataChanged => GetChangedValue("lockdata");
        public string? lockdataOldValue => (string?)GetValue("lockdata", ModelValueVersion.OldVersion);
        private string? _lockdata = default(string?);

        /// <summary>
        /// LOCKID Field
        /// </summary>
        [DataMember(Name = "LOCKID")]
        [Field(null, PrimeryKey = false )]
        public int? lockid
        {
            get => _lockid;
            set
            {
                var _prvlockid = _lockid;
                _lockid = value; 
                OnChangedValue("lockid", value, _prvlockid);
            }
        }
        public bool lockidChanged => GetChangedValue("lockid");
        public int? lockidOldValue => (int?)GetValue("lockid", ModelValueVersion.OldVersion);
        private int? _lockid = default(int?);

        /// <summary>
        /// USERTYPE Field
        /// </summary>
        [DataMember(Name = "USERTYPE")]
        [Field(null, PrimeryKey = false )]
        public string? usertype
        {
            get => _usertype;
            set
            {
                var _prvrusertype = _usertype;
                _usertype = value; 
                OnChangedValue("usertype", value, _prvrusertype);
            }
        }
        public bool usertypeChanged => GetChangedValue("usertype");
        public string? usertypeOldValue => (string?)GetValue("usertype", ModelValueVersion.OldVersion);
        private string? _usertype = default(string?);

        /// <summary>
        /// KEYSTATUS Field
        /// </summary>
        [DataMember(Name = "KEYSTATUS")]
        [Field(null, PrimeryKey = false )]
        public string? keystatus
        {
            get => _keystatus;
            set
            {
                var _prvkeystatus = _keystatus;
                _keystatus = value; 
                OnChangedValue("keystatus", value, _prvkeystatus);
            }
        }
        public bool keystatusChanged => GetChangedValue("keystatus");
        public string? keystatusOldValue => (string?)GetValue("keystatus", ModelValueVersion.OldVersion);
        private string? _keystatus = default(string?);

        /// <summary>
        /// LOCKNAME Field
        /// </summary>
        [DataMember(Name = "LOCKNAME")]
        [Field(null, PrimeryKey = false )]
        public string? lockname
        {
            get => _lockname;
            set
            {
                var _prvlockname = _lockname;
                _lockname = value; 
                OnChangedValue("lockname", value, _prvlockname);
            }
        }
        public bool locknameChanged => GetChangedValue("lockname");
        public string? locknameOldValue => (string?)GetValue("lockname", ModelValueVersion.OldVersion);
        private string? _lockname = default(string?);

        /// <summary>
        /// LOCKALIAS Field
        /// </summary>
        [DataMember(Name = "LOCKALIAS")]
        [Field(null, PrimeryKey = false )]
        public string? lockalias
        {
            get => _lockalias;
            set
            {
                var _prvlockalias = _lockalias;
                _lockalias = value; 
                OnChangedValue("lockalias", value, _prvlockalias);
            }
        }
        public bool lockaliasChanged => GetChangedValue("lockalias");
        public string? lockaliasOldValue => (string?)GetValue("lockalias", ModelValueVersion.OldVersion);
        private string? _lockalias = default(string?);

        /// <summary>
        /// LOCKMAC Field
        /// </summary>
        [DataMember(Name = "LOCKMAC")]
        [Field(null, PrimeryKey = false )]
        public string? lockmac
        {
            get => _lockmac;
            set
            {
                var _prvlockmac = _lockmac;
                _lockmac = value; 
                OnChangedValue("lockmac", value, _prvlockmac);
            }
        }
        public bool lockmacChanged => GetChangedValue("lockmac");
        public string? lockmac4OldValue => (string?)GetValue("lockmac", ModelValueVersion.OldVersion);
        private string? _lockmac = default(string?);

        /// <summary>
        /// NOKEYPWD Field
        /// </summary>
        [DataMember(Name = "NOKEYPWD")]
        [Field(null, PrimeryKey = false )]
        public string? nokeypwd
        {
            get => _nokeypwd;
            set
            {
                var _prvnokeypwd = _nokeypwd;
                _nokeypwd = value; 
                OnChangedValue("nokeypwd", value, _prvnokeypwd);
            }
        }
        public bool nokeypwdChanged => GetChangedValue("nokeypwd");
        public string? nokeypwdOldValue => (string?)GetValue("nokeypwd", ModelValueVersion.OldVersion);
        private string? _nokeypwd = default(string?);

        /// <summary>
        /// ELECTRICQUANYITY Field
        /// </summary>
        [DataMember(Name = "ELECTRICQUANYITY")]
        [Field(null, PrimeryKey = false )]
        public int? electricquantity
        {
            get => _electricquantity;
            set
            {
                var _prvelectricquantity = _electricquantity;
                _electricquantity = value; 
                OnChangedValue("electricquantity", value, _prvelectricquantity);
            }
        }
        public bool electricquantityChanged => GetChangedValue("electricquantity");
        public int? electricquantityOldValue => (int?)GetValue("electricquantity", ModelValueVersion.OldVersion);
        private int? _electricquantity = default(int?);

        /// <summary>
        /// STARTDATE Field
        /// </summary>
        [DataMember(Name = "STARTDATE")]
        [Field(null, PrimeryKey = false )]
        public DateTime? STARTDATE
        {
            get => _STARTDATE;
            set
            {
                var _prvSTARTDATE = _STARTDATE;
                _STARTDATE = value; 
                OnChangedValue("STARTDATE", value, _prvSTARTDATE);
            }
        }
        public bool STARTDATEChanged => GetChangedValue("STARTDATE");
        public DateTime? STARTDATEOldValue => (DateTime?)GetValue("STARTDATE", ModelValueVersion.OldVersion);
        private DateTime? _STARTDATE = default(DateTime?);

        /// <summary>
        /// ENDDATE Field
        /// </summary>
        [DataMember(Name = "ENDDATE")]
        [Field(null, PrimeryKey = false )]
        public DateTime? ENDDATE
        {
            get => _ENDDATE;
            set
            {
                var _prvENDDATE = _ENDDATE;
                _ENDDATE = value; 
                OnChangedValue("ENDDATE", value, _prvENDDATE);
            }
        }
        public bool ENDDATEChanged => GetChangedValue("ENDDATE");
        public DateTime? ENDDATEOldValue => (DateTime?)GetValue("ENDDATE", ModelValueVersion.OldVersion);
        private DateTime? _ENDDATE = default(DateTime?);

        /// <summary>
        /// REMARKS Field
        /// </summary>
        [DataMember(Name = "REMARKS")]
        [Field(null, PrimeryKey = false )]
        public string? remarks
        {
            get => _remarks;
            set
            {
                var _prvremarks = _remarks;
                _remarks = value; 
                OnChangedValue("remarks", value, _prvremarks);
            }
        }
        public bool remarksChanged => GetChangedValue("remarks");
        public string? remarksOldValue => (string?)GetValue("remarks", ModelValueVersion.OldVersion);
        private string? _remarks = default(string?);

        /// <summary>
        /// KEYRIGHT Field
        /// </summary>
        [DataMember(Name = "KEYRIGHT")]
        [Field(null, PrimeryKey = false)]
        public int? keyright
        {
            get => _keyright;
            set
            {
                var _prvkeyright = _keyright;
                _keyright = value;
                OnChangedValue("keyright", value, _prvkeyright);
            }
        }
        public bool keyrightChanged => GetChangedValue("keyright");
        public int? keyrightOldValue => (int?)GetValue("keyright", ModelValueVersion.OldVersion);
        private int? _keyright = default(int?);

        /// <summary>
        /// FEATUREVALUE Field
        /// </summary>
        [DataMember(Name = "FEATUREVALUE")]
        [Field(null, PrimeryKey = false )]
        public string? featurevalue
        {
            get => _featurevalue;
            set
            {
                var _prvfeaturevalue = _featurevalue;
                _featurevalue = value; 
                OnChangedValue("featurevalue", value, _prvfeaturevalue);
            }
        }
        public bool featurevalueChanged => GetChangedValue("featurevalue");
        public string? featurevalueOldValue => (string?)GetValue("featurevalue", ModelValueVersion.OldVersion);
        private string? _featurevalue = default(string?);

        /// <summary>
        /// REMOTEENABLE Field
        /// </summary>
        [DataMember(Name = "REMOTEENABLE")]
        [Field(null, PrimeryKey = false)]
        public int? remoteenable
        {
            get => _remoteenable;
            set
            {
                var _prvremoteenable = _remoteenable;
                _remoteenable = value;
                OnChangedValue("remoteenable", value, _prvremoteenable);
            }
        }
        public bool remoteenableChanged => GetChangedValue("remoteenable");
        public int? remoteenableOldValue => (int?)GetValue("remoteenable", ModelValueVersion.OldVersion);
        private int? _remoteenable = default(int?);

        /// <summary>
        /// PASSAGEMODE Field
        /// </summary>
        [DataMember(Name = "PASSAGEMODE")]
        [Field(null, PrimeryKey = false)]
        public int? passagemode
        {
            get => _passagemode;
            set
            {
                var _prvpassageMode = _passagemode;
                _passagemode = value;
                OnChangedValue("passageMode", value, _prvpassageMode);
            }
        }
        public bool passagemodeChanged => GetChangedValue("passagemode");
        public int? passagemodeOldValue => (int?)GetValue("passagemode", ModelValueVersion.OldVersion);
        private int? _passagemode = default(int?);

        /// <summary>
        /// GROUPID Field
        /// </summary>
        [DataMember(Name = "GROUPID")]
        [Field(null, PrimeryKey = false)]
        public int? groupid
        {
            get => _groupid;
            set
            {
                var _prvgroupid = _groupid;
                _groupid = value;
                OnChangedValue("groupid", value, _prvgroupid);
            }
        }
        public bool groupidChanged => GetChangedValue("groupid");
        public int? groupidOldValue => (int?)GetValue("groupid", ModelValueVersion.OldVersion);
        private int? _groupid = default(int?);

        /// <summary>
        /// GROUPNAME Field
        /// </summary>
        [DataMember(Name = "GROUPNAME")]
        [Field(null, PrimeryKey = false)]
        public string? groupname
        {
            get => _groupname;
            set
            {
                var _prvgroupname = _groupname;
                _groupname = value;
                OnChangedValue("groupname", value, _prvgroupname);
            }
        }
        public bool groupnameChanged => GetChangedValue("groupname");
        public string? groupnameOldValue => (string?)GetValue("groupname", ModelValueVersion.OldVersion);
        private string? _groupname = default(string?);
    }

    /// <summary>
    /// AreamstModel(areamst) Model List Class
    /// </summary>    
    public class TtlockKeyInfoModelList : XNDIMModelList<TtlockKeyInfoModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public TtlockKeyInfoModelList()
        {
            TableName = "TtlockKeyInfo";
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public TtlockKeyInfoModelList(string dataSource): base("TtlockKeyInfo", dataSource)
        {
            return;
        }
    }

}