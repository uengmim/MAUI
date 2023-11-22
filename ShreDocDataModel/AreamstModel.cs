// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "areamst" -output "AreamstModel.cs" -nspace "ShreDoc.DataModel"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;


namespace ShreDoc.DataModel
{
    /// <summary>
    /// AreamstModel(areamst) Model class
    /// </summary>
    [DataContract]
    public class AreamstModel : XNDIMModel
    {
        /// <summary>
        /// AREA Field
        /// </summary>
        [DataMember(Name = "AREA")]
        [Field(null, PrimeryKey = true )]
        public string? area
        {
            get => _area;
            set
            {
                var _prvarea = _area;
                _area = value; 
                OnChangedValue("area", value, _prvarea);
            }
        }
        public bool areaChanged => GetChangedValue("area");
        public string? areaOldValue => (string?)GetValue("area", ModelValueVersion.OldVersion);
        private string? _area = default(string?);

        /// <summary>
        /// DEPTID Field
        /// </summary>
        [DataMember(Name = "DEPTID")]
        [Field(null, PrimeryKey = false )]
        public string? deptid
        {
            get => _deptid;
            set
            {
                var _prvdeptid = _deptid;
                _deptid = value; 
                OnChangedValue("deptid", value, _prvdeptid);
            }
        }
        public bool deptidChanged => GetChangedValue("deptid");
        public string? deptidOldValue => (string?)GetValue("deptid", ModelValueVersion.OldVersion);
        private string? _deptid = default(string?);

        /// <summary>
        /// AREANM Field
        /// </summary>
        [DataMember(Name = "AREANM")]
        [Field(null, PrimeryKey = false )]
        public string? areanm
        {
            get => _areanm;
            set
            {
                var _prvareanm = _areanm;
                _areanm = value; 
                OnChangedValue("areanm", value, _prvareanm);
            }
        }
        public bool areanmChanged => GetChangedValue("areanm");
        public string? areanmOldValue => (string?)GetValue("areanm", ModelValueVersion.OldVersion);
        private string? _areanm = default(string?);

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
    /// AreamstModel(areamst) Model List Class
    /// </summary>    
    public class AreamstModelList : XNDIMModelList<AreamstModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public AreamstModelList()
        {
            TableName = "areamst";
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public AreamstModelList (string dataSource): base("areamst", dataSource)
        {
            return;
        }
    }

}