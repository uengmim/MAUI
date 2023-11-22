// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "sireq" -output "SIREQModel.cs" -nspace "ShreDoc.DataModel"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;


namespace ShreDoc.DataModel
{
    /// <summary>
    /// SireqModel(sireq) Model class
    /// </summary>
    [DataContract]
    public class SireqModel : XNDIMModel
    {
        /// <summary>
        /// EREQID Field
        /// </summary>
        [DataMember(Name = "EREQID")]
        [Field(null, PrimeryKey = true )]
        public string? ereqid
        {
            get => _ereqid;
            set
            {
                var _prvereqid = _ereqid;
                _ereqid = value; 
                OnChangedValue("ereqid", value, _prvereqid);
            }
        }
        public bool ereqidChanged => GetChangedValue("ereqid");
        public string? ereqidOldValue => (string?)GetValue("ereqid", ModelValueVersion.OldVersion);
        private string? _ereqid = default(string?);

        /// <summary>
        /// EREQTYP Field
        /// </summary>
        [DataMember(Name = "EREQTYP")]
        [Field(null, PrimeryKey = false )]
        public string? ereqtyp
        {
            get => _ereqtyp;
            set
            {
                var _prvereqtyp = _ereqtyp;
                _ereqtyp = value; 
                OnChangedValue("ereqtyp", value, _prvereqtyp);
            }
        }
        public bool ereqtypChanged => GetChangedValue("ereqtyp");
        public string? ereqtypOldValue => (string?)GetValue("ereqtyp", ModelValueVersion.OldVersion);
        private string? _ereqtyp = default(string?);

        /// <summary>
        /// WDEPTID Field
        /// </summary>
        [DataMember(Name = "WDEPTID")]
        [Field(null, PrimeryKey = false )]
        public string? wdeptid
        {
            get => _wdeptid;
            set
            {
                var _prvwdeptid = _wdeptid;
                _wdeptid = value; 
                OnChangedValue("wdeptid", value, _prvwdeptid);
            }
        }
        public bool wdeptidChanged => GetChangedValue("wdeptid");
        public string? wdeptidOldValue => (string?)GetValue("wdeptid", ModelValueVersion.OldVersion);
        private string? _wdeptid = default(string?);

        /// <summary>
        /// WEMPID Field
        /// </summary>
        [DataMember(Name = "WEMPID")]
        [Field(null, PrimeryKey = false )]
        public string? wempid
        {
            get => _wempid;
            set
            {
                var _prvwempid = _wempid;
                _wempid = value; 
                OnChangedValue("wempid", value, _prvwempid);
            }
        }
        public bool wempidChanged => GetChangedValue("wempid");
        public string? wempidOldValue => (string?)GetValue("wempid", ModelValueVersion.OldVersion);
        private string? _wempid = default(string?);

        /// <summary>
        /// ADEPTID Field
        /// </summary>
        [DataMember(Name = "ADEPTID")]
        [Field(null, PrimeryKey = false )]
        public string? adeptid
        {
            get => _adeptid;
            set
            {
                var _prvadeptid = _adeptid;
                _adeptid = value; 
                OnChangedValue("adeptid", value, _prvadeptid);
            }
        }
        public bool adeptidChanged => GetChangedValue("adeptid");
        public string? adeptidOldValue => (string?)GetValue("adeptid", ModelValueVersion.OldVersion);
        private string? _adeptid = default(string?);

        /// <summary>
        /// AEMPID Field
        /// </summary>
        [DataMember(Name = "AEMPID")]
        [Field(null, PrimeryKey = false )]
        public string? aempid
        {
            get => _aempid;
            set
            {
                var _prvaempid = _aempid;
                _aempid = value; 
                OnChangedValue("aempid", value, _prvaempid);
            }
        }
        public bool aempidChanged => GetChangedValue("aempid");
        public string? aempidOldValue => (string?)GetValue("aempid", ModelValueVersion.OldVersion);
        private string? _aempid = default(string?);

        /// <summary>
        /// EREQDT Field
        /// </summary>
        [DataMember(Name = "EREQDT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? ereqdt
        {
            get => _ereqdt;
            set
            {
                var _prvereqdt = _ereqdt;
                _ereqdt = value; 
                OnChangedValue("ereqdt", value, _prvereqdt);
            }
        }
        public bool ereqdtChanged => GetChangedValue("ereqdt");
        public DateTime? ereqdtOldValue => (DateTime?)GetValue("ereqdt", ModelValueVersion.OldVersion);
        private DateTime? _ereqdt = default(DateTime?);

        /// <summary>
        /// STRDT Field
        /// </summary>
        [DataMember(Name = "STRDT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? strdt
        {
            get => _strdt;
            set
            {
                var _prvstrdt = _strdt;
                _strdt = value; 
                OnChangedValue("strdt", value, _prvstrdt);
            }
        }
        public bool strdtChanged => GetChangedValue("strdt");
        public DateTime? strdtOldValue => (DateTime?)GetValue("strdt", ModelValueVersion.OldVersion);
        private DateTime? _strdt = default(DateTime?);

        /// <summary>
        /// ENDDT Field
        /// </summary>
        [DataMember(Name = "ENDDT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? enddt
        {
            get => _enddt;
            set
            {
                var _prvenddt = _enddt;
                _enddt = value; 
                OnChangedValue("enddt", value, _prvenddt);
            }
        }
        public bool enddtChanged => GetChangedValue("enddt");
        public DateTime? enddtOldValue => (DateTime?)GetValue("enddt", ModelValueVersion.OldVersion);
        private DateTime? _enddt = default(DateTime?);

        /// <summary>
        /// ENTRMK Field
        /// </summary>
        [DataMember(Name = "ENTRMK")]
        [Field(null, PrimeryKey = false )]
        public string? entrmk
        {
            get => _entrmk;
            set
            {
                var _prventrmk = _entrmk;
                _entrmk = value; 
                OnChangedValue("entrmk", value, _prventrmk);
            }
        }
        public bool entrmkChanged => GetChangedValue("entrmk");
        public string? entrmkOldValue => (string?)GetValue("entrmk", ModelValueVersion.OldVersion);
        private string? _entrmk = default(string?);

        /// <summary>
        /// SAFEEDU Field
        /// </summary>
        [DataMember(Name = "SAFEEDU")]
        [Field(null, PrimeryKey = false )]
        public string? safeedu
        {
            get => _safeedu;
            set
            {
                var _prvsafeedu = _safeedu;
                _safeedu = value; 
                OnChangedValue("safeedu", value, _prvsafeedu);
            }
        }
        public bool safeeduChanged => GetChangedValue("safeedu");
        public string? safeeduOldValue => (string?)GetValue("safeedu", ModelValueVersion.OldVersion);
        private string? _safeedu = default(string?);

        /// <summary>
        /// REFDA1 Field
        /// </summary>
        [DataMember(Name = "REFDA1")]
        [Field(null, PrimeryKey = false )]
        public string? rEFDA1
        {
            get => _rEFDA1;
            set
            {
                var _prvrEFDA1 = _rEFDA1;
                _rEFDA1 = value; 
                OnChangedValue("rEFDA1", value, _prvrEFDA1);
            }
        }
        public bool rEFDA1Changed => GetChangedValue("rEFDA1");
        public string? rEFDA1OldValue => (string?)GetValue("rEFDA1", ModelValueVersion.OldVersion);
        private string? _rEFDA1 = default(string?);

        /// <summary>
        /// REFDA2 Field
        /// </summary>
        [DataMember(Name = "REFDA2")]
        [Field(null, PrimeryKey = false )]
        public string? rEFDA2
        {
            get => _rEFDA2;
            set
            {
                var _prvrEFDA2 = _rEFDA2;
                _rEFDA2 = value; 
                OnChangedValue("rEFDA2", value, _prvrEFDA2);
            }
        }
        public bool rEFDA2Changed => GetChangedValue("rEFDA2");
        public string? rEFDA2OldValue => (string?)GetValue("rEFDA2", ModelValueVersion.OldVersion);
        private string? _rEFDA2 = default(string?);

        /// <summary>
        /// REFDA3 Field
        /// </summary>
        [DataMember(Name = "REFDA3")]
        [Field(null, PrimeryKey = false )]
        public string? rEFDA3
        {
            get => _rEFDA3;
            set
            {
                var _prvrEFDA3 = _rEFDA3;
                _rEFDA3 = value; 
                OnChangedValue("rEFDA3", value, _prvrEFDA3);
            }
        }
        public bool rEFDA3Changed => GetChangedValue("rEFDA3");
        public string? rEFDA3OldValue => (string?)GetValue("rEFDA3", ModelValueVersion.OldVersion);
        private string? _rEFDA3 = default(string?);

        /// <summary>
        /// REFDA4 Field
        /// </summary>
        [DataMember(Name = "REFDA4")]
        [Field(null, PrimeryKey = false )]
        public string? rEFDA4
        {
            get => _rEFDA4;
            set
            {
                var _prvrEFDA4 = _rEFDA4;
                _rEFDA4 = value; 
                OnChangedValue("rEFDA4", value, _prvrEFDA4);
            }
        }
        public bool rEFDA4Changed => GetChangedValue("rEFDA4");
        public string? rEFDA4OldValue => (string?)GetValue("rEFDA4", ModelValueVersion.OldVersion);
        private string? _rEFDA4 = default(string?);

        /// <summary>
        /// REFDA5 Field
        /// </summary>
        [DataMember(Name = "REFDA5")]
        [Field(null, PrimeryKey = false )]
        public string? rEFDA5
        {
            get => _rEFDA5;
            set
            {
                var _prvrEFDA5 = _rEFDA5;
                _rEFDA5 = value; 
                OnChangedValue("rEFDA5", value, _prvrEFDA5);
            }
        }
        public bool rEFDA5Changed => GetChangedValue("rEFDA5");
        public string? rEFDA5OldValue => (string?)GetValue("rEFDA5", ModelValueVersion.OldVersion);
        private string? _rEFDA5 = default(string?);

        /// <summary>
        /// REFDT1 Field
        /// </summary>
        [DataMember(Name = "REFDT1")]
        [Field(null, PrimeryKey = false )]
        public DateTime? rEFDT1
        {
            get => _rEFDT1;
            set
            {
                var _prvrEFDT1 = _rEFDT1;
                _rEFDT1 = value; 
                OnChangedValue("rEFDT1", value, _prvrEFDT1);
            }
        }
        public bool rEFDT1Changed => GetChangedValue("rEFDT1");
        public DateTime? rEFDT1OldValue => (DateTime?)GetValue("rEFDT1", ModelValueVersion.OldVersion);
        private DateTime? _rEFDT1 = default(DateTime?);

        /// <summary>
        /// REFDT2 Field
        /// </summary>
        [DataMember(Name = "REFDT2")]
        [Field(null, PrimeryKey = false )]
        public DateTime? rEFDT2
        {
            get => _rEFDT2;
            set
            {
                var _prvrEFDT2 = _rEFDT2;
                _rEFDT2 = value; 
                OnChangedValue("rEFDT2", value, _prvrEFDT2);
            }
        }
        public bool rEFDT2Changed => GetChangedValue("rEFDT2");
        public DateTime? rEFDT2OldValue => (DateTime?)GetValue("rEFDT2", ModelValueVersion.OldVersion);
        private DateTime? _rEFDT2 = default(DateTime?);

        /// <summary>
        /// REFDT3 Field
        /// </summary>
        [DataMember(Name = "REFDT3")]
        [Field(null, PrimeryKey = false )]
        public DateTime? rEFDT3
        {
            get => _rEFDT3;
            set
            {
                var _prvrEFDT3 = _rEFDT3;
                _rEFDT3 = value; 
                OnChangedValue("rEFDT3", value, _prvrEFDT3);
            }
        }
        public bool rEFDT3Changed => GetChangedValue("rEFDT3");
        public DateTime? rEFDT3OldValue => (DateTime?)GetValue("rEFDT3", ModelValueVersion.OldVersion);
        private DateTime? _rEFDT3 = default(DateTime?);

        /// <summary>
        /// CRTUSR Field
        /// </summary>
        [DataMember(Name = "CRTUSR")]
        [Field(null, PrimeryKey = false )]
        public string? crtusr
        {
            get => _crtusr;
            set
            {
                var _prvcrtusr = _crtusr;
                _crtusr = value; 
                OnChangedValue("crtusr", value, _prvcrtusr);
            }
        }
        public bool crtusrChanged => GetChangedValue("crtusr");
        public string? crtusrOldValue => (string?)GetValue("crtusr", ModelValueVersion.OldVersion);
        private string? _crtusr = default(string?);

        /// <summary>
        /// CRTDT Field
        /// </summary>
        [DataMember(Name = "CRTDT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? crtdt
        {
            get => _crtdt;
            set
            {
                var _prvcrtdt = _crtdt;
                _crtdt = value; 
                OnChangedValue("crtdt", value, _prvcrtdt);
            }
        }
        public bool crtdtChanged => GetChangedValue("crtdt");
        public DateTime? crtdtOldValue => (DateTime?)GetValue("crtdt", ModelValueVersion.OldVersion);
        private DateTime? _crtdt = default(DateTime?);

        /// <summary>
        /// UPDUSR Field
        /// </summary>
        [DataMember(Name = "UPDUSR")]
        [Field(null, PrimeryKey = false )]
        public string? updusr
        {
            get => _updusr;
            set
            {
                var _prvupdusr = _updusr;
                _updusr = value; 
                OnChangedValue("updusr", value, _prvupdusr);
            }
        }
        public bool updusrChanged => GetChangedValue("updusr");
        public string? updusrOldValue => (string?)GetValue("updusr", ModelValueVersion.OldVersion);
        private string? _updusr = default(string?);

        /// <summary>
        /// UPDDT Field
        /// </summary>
        [DataMember(Name = "UPDDT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? upddt
        {
            get => _upddt;
            set
            {
                var _prvupddt = _upddt;
                _upddt = value; 
                OnChangedValue("upddt", value, _prvupddt);
            }
        }
        public bool upddtChanged => GetChangedValue("upddt");
        public DateTime? upddtOldValue => (DateTime?)GetValue("upddt", ModelValueVersion.OldVersion);
        private DateTime? _upddt = default(DateTime?);

   
    }

    /// <summary>
    /// SireqModel(sireq) Model List Class
    /// </summary>    
    public class SireqModelList : XNDIMModelList<SireqModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public SireqModelList()
        {
            TableName = "sireq";
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SireqModelList(string dataSource): base("sireq", dataSource)
        {
            return;
        }
    }

}