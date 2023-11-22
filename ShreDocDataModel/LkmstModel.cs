// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "lkmst" -output "LkmstModel.cs" -nspace "ShreDoc.DataModel"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;


namespace ShreDoc.DataModel
{
    /// <summary>
    /// LkmstModel(lkmst) Model class
    /// </summary>
    [DataContract]
    public class LkmstModel : XNDIMModel
    {
        /// <summary>
        /// LSN Field
        /// </summary>
        [DataMember(Name = "LSN")]
        [Field(null, PrimeryKey = true )]
        public string? lsn
        {
            get => _lsn;
            set
            {
                var _prvlsn = _lsn;
                _lsn = value; 
                OnChangedValue("lsn", value, _prvlsn);
            }
        }
        public bool lsnChanged => GetChangedValue("lsn");
        public string? lsnOldValue => (string?)GetValue("lsn", ModelValueVersion.OldVersion);
        private string? _lsn = default(string?);

        /// <summary>
        /// ILSID Field
        /// </summary>
        [DataMember(Name = "ILSID")]
        [Field(null, PrimeryKey = false )]
        public string? ilsid
        {
            get => _ilsid;
            set
            {
                var _prvilsid = _ilsid;
                _ilsid = value; 
                OnChangedValue("ilsid", value, _prvilsid);
            }
        }
        public bool ilsidChanged => GetChangedValue("ilsid");
        public string? ilsidOldValue => (string?)GetValue("ilsid", ModelValueVersion.OldVersion);
        private string? _ilsid = default(string?);

        /// <summary>
        /// TKID Field
        /// </summary>
        [DataMember(Name = "TKID")]
        [Field(null, PrimeryKey = false )]
        public string? tkid
        {
            get => _tkid;
            set
            {
                var _prvtkid = _tkid;
                _tkid = value; 
                OnChangedValue("tkid", value, _prvtkid);
            }
        }
        public bool tkidChanged => GetChangedValue("tkid");
        public string? tkidOldValue => (string?)GetValue("tkid", ModelValueVersion.OldVersion);
        private string? _tkid = default(string?);

        /// <summary>
        /// MAC Field
        /// </summary>
        [DataMember(Name = "MAC")]
        [Field(null, PrimeryKey = false )]
        public string? mac
        {
            get => _mac;
            set
            {
                var _prvmac = _mac;
                _mac = value; 
                OnChangedValue("mac", value, _prvmac);
            }
        }
        public bool macChanged => GetChangedValue("mac");
        public string? macOldValue => (string?)GetValue("mac", ModelValueVersion.OldVersion);
        private string? _mac = default(string?);

        /// <summary>
        /// LKTYP Field
        /// </summary>
        [DataMember(Name = "LKTYP")]
        [Field(null, PrimeryKey = false )]
        public string? lktyp
        {
            get => _lktyp;
            set
            {
                var _prvlktyp = _lktyp;
                _lktyp = value; 
                OnChangedValue("lktyp", value, _prvlktyp);
            }
        }
        public bool lktypChanged => GetChangedValue("lktyp");
        public string? lktypOldValue => (string?)GetValue("lktyp", ModelValueVersion.OldVersion);
        private string? _lktyp = default(string?);

        /// <summary>
        /// LKNM Field
        /// </summary>
        [DataMember(Name = "LKNM")]
        [Field(null, PrimeryKey = false )]
        public string? lknm
        {
            get => _lknm;
            set
            {
                var _prvlknm = _lknm;
                _lknm = value; 
                OnChangedValue("lknm", value, _prvlknm);
            }
        }
        public bool lknmChanged => GetChangedValue("lknm");
        public string? lknmOldValue => (string?)GetValue("lknm", ModelValueVersion.OldVersion);
        private string? _lknm = default(string?);

        /// <summary>
        /// QCCD Field
        /// </summary>
        [DataMember(Name = "QCCD")]
        [Field(null, PrimeryKey = false )]
        public string? qccd
        {
            get => _qccd;
            set
            {
                var _prvqccd = _qccd;
                _qccd = value; 
                OnChangedValue("qccd", value, _prvqccd);
            }
        }
        public bool qccdChanged => GetChangedValue("qccd");
        public string? qccdOldValue => (string?)GetValue("qccd", ModelValueVersion.OldVersion);
        private string? _qccd = default(string?);

        /// <summary>
        /// QCTOT Field
        /// </summary>
        [DataMember(Name = "QCTOT")]
        [Field(null, PrimeryKey = false )]
        public int? qctot
        {
            get => _qctot;
            set
            {
                var _prvqctot = _qctot;
                _qctot = value; 
                OnChangedValue("qctot", value, _prvqctot);
            }
        }
        public bool qctotChanged => GetChangedValue("qctot");
        public int? qctotOldValue => (int?)GetValue("qctot", ModelValueVersion.OldVersion);
        private int? _qctot = default(int?);

        /// <summary>
        /// QCCNT Field
        /// </summary>
        [DataMember(Name = "QCCNT")]
        [Field(null, PrimeryKey = false )]
        public int? qccnt
        {
            get => _qccnt;
            set
            {
                var _prvqccnt = _qccnt;
                _qccnt = value; 
                OnChangedValue("qccnt", value, _prvqccnt);
            }
        }
        public bool qccntChanged => GetChangedValue("qccnt");
        public int? qccntOldValue => (int?)GetValue("qccnt", ModelValueVersion.OldVersion);
        private int? _qccnt = default(int?);

        /// <summary>
        /// LKSTA Field
        /// </summary>
        [DataMember(Name = "LKSTA")]
        [Field(null, PrimeryKey = false )]
        public string? lksta
        {
            get => _lksta;
            set
            {
                var _prvlksta = _lksta;
                _lksta = value; 
                OnChangedValue("lksta", value, _prvlksta);
            }
        }
        public bool lkstaChanged => GetChangedValue("lksta");
        public string? lkstaOldValue => (string?)GetValue("lksta", ModelValueVersion.OldVersion);
        private string? _lksta = default(string?);

        /// <summary>
        /// LKAKA Field
        /// </summary>
        [DataMember(Name = "LKAKA")]
        [Field(null, PrimeryKey = false )]
        public string? lkaka
        {
            get => _lkaka;
            set
            {
                var _prvlkaka = _lkaka;
                _lkaka = value; 
                OnChangedValue("lkaka", value, _prvlkaka);
            }
        }
        public bool lkakaChanged => GetChangedValue("lkaka");
        public string? lkakaOldValue => (string?)GetValue("lkaka", ModelValueVersion.OldVersion);
        private string? _lkaka = default(string?);

        /// <summary>
        /// RECSTA Field
        /// </summary>
        [DataMember(Name = "RECSTA")]
        [Field(null, PrimeryKey = false )]
        public string? recsta
        {
            get => _recsta;
            set
            {
                var _prvrecsta = _recsta;
                _recsta = value; 
                OnChangedValue("recsta", value, _prvrecsta);
            }
        }
        public bool recstaChanged => GetChangedValue("recsta");
        public string? recstaOldValue => (string?)GetValue("recsta", ModelValueVersion.OldVersion);
        private string? _recsta = default(string?);

        /// <summary>
        /// ASSETS Field
        /// </summary>
        [DataMember(Name = "ASSETS")]
        [Field(null, PrimeryKey = false )]
        public string? assets
        {
            get => _assets;
            set
            {
                var _prvassets = _assets;
                _assets = value; 
                OnChangedValue("assets", value, _prvassets);
            }
        }
        public bool assetsChanged => GetChangedValue("assets");
        public string? assetsOldValue => (string?)GetValue("assets", ModelValueVersion.OldVersion);
        private string? _assets = default(string?);

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
    /// LkmstModel(lkmst) Model List Class
    /// </summary>    
    public class LkmstModelList : XNDIMModelList<LkmstModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public LkmstModelList()
        {
            TableName = "lkmst";
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public LkmstModelList(string dataSource): base("lkmst", dataSource)
        {
            return;
        }
    }

}