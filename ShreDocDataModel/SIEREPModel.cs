// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "sierep" -output "SIEREPModel.cs" -nspace "ShreDoc.DataModel"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;


namespace ShreDoc.DataModel
{
    /// <summary>
    /// SierepModel(sierep) Model class
    /// </summary>
    [DataContract]
    public class SierepModel : XNDIMModel
    {
        /// <summary>
        /// CONFNO Field
        /// </summary>
        [DataMember(Name = "CONFNO")]
        [Field(null, PrimeryKey = true )]
        public string? confno
        {
            get => _confno;
            set
            {
                var _prvconfno = _confno;
                _confno = value; 
                OnChangedValue("confno", value, _prvconfno);
            }
        }
        public bool confnoChanged => GetChangedValue("confno");
        public string? confnoOldValue => (string?)GetValue("confno", ModelValueVersion.OldVersion);
        private string? _confno = default(string?);

        /// <summary>
        /// REPTYP Field
        /// </summary>
        [DataMember(Name = "REPTYP")]
        [Field(null, PrimeryKey = true )]
        public string? reptyp
        {
            get => _reptyp;
            set
            {
                var _prvreptyp = _reptyp;
                _reptyp = value; 
                OnChangedValue("reptyp", value, _prvreptyp);
            }
        }
        public bool reptypChanged => GetChangedValue("reptyp");
        public string? reptypOldValue => (string?)GetValue("reptyp", ModelValueVersion.OldVersion);
        private string? _reptyp = default(string?);

        /// <summary>
        /// REPDAT Field
        /// </summary>
        [DataMember(Name = "REPDAT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? repdat
        {
            get => _repdat;
            set
            {
                var _prvrepdat = _repdat;
                _repdat = value; 
                OnChangedValue("repdat", value, _prvrepdat);
            }
        }
        public bool repdatChanged => GetChangedValue("repdat");
        public DateTime? repdatOldValue => (DateTime?)GetValue("repdat", ModelValueVersion.OldVersion);
        private DateTime? _repdat = default(DateTime?);

        /// <summary>
        /// ETYPE Field
        /// </summary>
        [DataMember(Name = "ETYPE")]
        [Field(null, PrimeryKey = false )]
        public string? etype
        {
            get => _etype;
            set
            {
                var _prvetype = _etype;
                _etype = value; 
                OnChangedValue("etype", value, _prvetype);
            }
        }
        public bool etypeChanged => GetChangedValue("etype");
        public string? etypeOldValue => (string?)GetValue("etype", ModelValueVersion.OldVersion);
        private string? _etype = default(string?);

        /// <summary>
        /// FTYPE Field
        /// </summary>
        [DataMember(Name = "FTYPE")]
        [Field(null, PrimeryKey = false )]
        public string? ftype
        {
            get => _ftype;
            set
            {
                var _prvftype = _ftype;
                _ftype = value; 
                OnChangedValue("ftype", value, _prvftype);
            }
        }
        public bool ftypeChanged => GetChangedValue("ftype");
        public string? ftypeOldValue => (string?)GetValue("ftype", ModelValueVersion.OldVersion);
        private string? _ftype = default(string?);

        /// <summary>
        /// FRMK Field
        /// </summary>
        [DataMember(Name = "FRMK")]
        [Field(null, PrimeryKey = false )]
        public string? frmk
        {
            get => _frmk;
            set
            {
                var _prvfrmk = _frmk;
                _frmk = value; 
                OnChangedValue("frmk", value, _prvfrmk);
            }
        }
        public bool frmkChanged => GetChangedValue("frmk");
        public string? frmkOldValue => (string?)GetValue("frmk", ModelValueVersion.OldVersion);
        private string? _frmk = default(string?);

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

   
    }

    /// <summary>
    /// SierepModel(sierep) Model List Class
    /// </summary>    
    public class SierepModelList : XNDIMModelList<SierepModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public SierepModelList()
        {
            TableName = "sierep";
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public SierepModelList(string dataSource): base("sierep", dataSource)
        {
            return;
        }
    }

}