// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "wkplmst" -output "WKPLMSTModel.cs" -nspace "ShreDoc.DataModel"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;


namespace ShreDoc.DataModel
{
    /// <summary>
    /// WkplmstModel(wkplmst) Model class
    /// </summary>
    [DataContract]
    public class WkplmstModel : XNDIMModel
    {
        /// <summary>
        /// WKPL Field
        /// </summary>
        [DataMember(Name = "WKPL")]
        [Field(null, PrimeryKey = true )]
        public string? wkpl
        {
            get => _wkpl;
            set
            {
                var _prvwkpl = _wkpl;
                _wkpl = value; 
                OnChangedValue("wkpl", value, _prvwkpl);
            }
        }
        public bool wkplChanged => GetChangedValue("wkpl");
        public string? wkplOldValue => (string?)GetValue("wkpl", ModelValueVersion.OldVersion);
        private string? _wkpl = default(string?);

        /// <summary>
        /// WKPLNM Field
        /// </summary>
        [DataMember(Name = "WKPLNM")]
        [Field(null, PrimeryKey = false )]
        public string? wkplnm
        {
            get => _wkplnm;
            set
            {
                var _prvwkplnm = _wkplnm;
                _wkplnm = value; 
                OnChangedValue("wkplnm", value, _prvwkplnm);
            }
        }
        public bool wkplnmChanged => GetChangedValue("wkplnm");
        public string? wkplnmOldValue => (string?)GetValue("wkplnm", ModelValueVersion.OldVersion);
        private string? _wkplnm = default(string?);

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
        /// REFDA1 Field
        /// </summary>
        [DataMember(Name = "REFDA1")]
        [Field(null, PrimeryKey = false )]
        public string? refda1
        {
            get => _refda1;
            set
            {
                var _prvrefda1 = _refda1;
                _refda1 = value; 
                OnChangedValue("refda1", value, _prvrefda1);
            }
        }
        public bool refda1Changed => GetChangedValue("refda1");
        public string? refda1OldValue => (string?)GetValue("refda1", ModelValueVersion.OldVersion);
        private string? _refda1 = default(string?);

        /// <summary>
        /// REFDA2 Field
        /// </summary>
        [DataMember(Name = "REFDA2")]
        [Field(null, PrimeryKey = false)]
        public string? refda2
        {
            get => _refda2;
            set
            {
                var _prvrefda2 = _refda2;
                _refda2 = value;
                OnChangedValue("refda2", value, _prvrefda2);
            }
        }
        public bool refda2Changed => GetChangedValue("refda2");
        public string? refda2OldValue => (string?)GetValue("refda2", ModelValueVersion.OldVersion);
        private string? _refda2 = default(string?);

        /// <summary>
        /// REFDA3 Field
        /// </summary>
        [DataMember(Name = "REFDA3")]
        [Field(null, PrimeryKey = false)]
        public string? refda3
        {
            get => _refda3;
            set
            {
                var _prvrefda3 = _refda3;
                _refda3 = value;
                OnChangedValue("refda3", value, _prvrefda3);
            }
        }
        public bool refda3Changed => GetChangedValue("refda3");
        public string? refda3OldValue => (string?)GetValue("refda3", ModelValueVersion.OldVersion);
        private string? _refda3 = default(string?);

        /// <summary>
        /// REFDA4 Field
        /// </summary>
        [DataMember(Name = "REFDA4")]
        [Field(null, PrimeryKey = false)]
        public string? refda4
        {
            get => _refda4;
            set
            {
                var _prvrefda4 = _refda4;
                _refda4 = value;
                OnChangedValue("refda4", value, _prvrefda4);
            }
        }
        public bool refda4Changed => GetChangedValue("refda4");
        public string? refda4OldValue => (string?)GetValue("refda4", ModelValueVersion.OldVersion);
        private string? _refda4 = default(string?);

        /// <summary>
        /// REFDA5 Field
        /// </summary>
        [DataMember(Name = "REFDA5")]
        [Field(null, PrimeryKey = false)]
        public string? refda5
        {
            get => _refda5;
            set
            {
                var _prvrefda5 = _refda5;
                _refda5 = value;
                OnChangedValue("refda5", value, _prvrefda5);
            }
        }
        public bool refda5Changed => GetChangedValue("refda5");
        public string? refda5OldValue => (string?)GetValue("refda5", ModelValueVersion.OldVersion);
        private string? _refda5 = default(string?);

        /// <summary>
        /// REFDT1 Field
        /// </summary>
        [DataMember(Name = "REFDT1")]
        [Field(null, PrimeryKey = false)]
        public DateTime? refdt1
        {
            get => _refdt1;
            set
            {
                var _prvrefdt1 = _refdt1;
                _refdt1 = value;
                OnChangedValue("refdt1", value, _prvrefdt1);
            }
        }
        public bool refdt1Changed => GetChangedValue("refdt1");
        public DateTime? refdt1OldValue => (DateTime?)GetValue("refdt1", ModelValueVersion.OldVersion);
        private DateTime? _refdt1 = default(DateTime?);

        /// <summary>
        /// REFDT2 Field
        /// </summary>
        [DataMember(Name = "REFDT2")]
        [Field(null, PrimeryKey = false)]
        public DateTime? refdt2
        {
            get => _refdt2;
            set
            {
                var _prvrefdt2 = _refdt2;
                _refdt2 = value;
                OnChangedValue("refdt2", value, _prvrefdt2);
            }
        }
        public bool refdt2Changed => GetChangedValue("refdt2");
        public DateTime? refdt2OldValue => (DateTime?)GetValue("refdt2", ModelValueVersion.OldVersion);
        private DateTime? _refdt2 = default(DateTime?);

        /// <summary>
        /// REFDT3 Field
        /// </summary>
        [DataMember(Name = "REFDT3")]
        [Field(null, PrimeryKey = false)]
        public DateTime? refdt3
        {
            get => _refdt3;
            set
            {
                var _prvrefdt3 = _refdt3;
                _refdt3 = value;
                OnChangedValue("refdt3", value, _prvrefdt3);
            }
        }
        public bool refdt3Changed => GetChangedValue("refdt3");
        public DateTime? refdt3OldValue => (DateTime?)GetValue("refdt3", ModelValueVersion.OldVersion);
        private DateTime? _refdt3 = default(DateTime?);

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
    public class WkplmstModelList : XNDIMModelList<WkplmstModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public WkplmstModelList()
        {
            TableName = "wkplmst";
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public WkplmstModelList(string dataSource): base("wkplmst", dataSource)
        {
            return;
        }
    }

}