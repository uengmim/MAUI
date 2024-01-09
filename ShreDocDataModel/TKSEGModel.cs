// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "tkseg" -output "TKSEGModel.cs" -nspace "ShreDoc.DataModel"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;


namespace ShreDoc.DataModel
{
    /// <summary>
    /// TksegModel(tkseg) Model class
    /// </summary>
    [DataContract]
    public class TksegModel : XNDIMModel
    {
        /// <summary>
        /// TKID Field
        /// </summary>
        [DataMember(Name = "TKID")]
        [Field(null, PrimeryKey = true )]
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
        /// LSN Field
        /// </summary>
        [DataMember(Name = "LSN")]
        [Field(null, PrimeryKey = false )]
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
        /// EVTDT Field
        /// </summary>
        [DataMember(Name = "EVTDT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? evtdt
        {
            get => _evtdt;
            set
            {
                var _prvevtdt = _evtdt;
                _evtdt = value; 
                OnChangedValue("evtdt", value, _prvevtdt);
            }
        }
        public bool evtdtChanged => GetChangedValue("evtdt");
        public DateTime? evtdtOldValue => (DateTime?)GetValue("evtdt", ModelValueVersion.OldVersion);
        private DateTime? _evtdt = default(DateTime?);

        /// <summary>
        /// CONDT Field
        /// </summary>
        [DataMember(Name = "CONDT")]
        [Field(null, PrimeryKey = false )]
        public DateTime? condt
        {
            get => _condt;
            set
            {
                var _prvcondt = _condt;
                _condt = value; 
                OnChangedValue("condt", value, _prvcondt);
            }
        }
        public bool condtChanged => GetChangedValue("condt");
        public DateTime? condtOldValue => (DateTime?)GetValue("condt", ModelValueVersion.OldVersion);
        private DateTime? _condt = default(DateTime?);

        /// <summary>
        /// BATTV Field
        /// </summary>
        [DataMember(Name = "BATTV")]
        [Field(null, PrimeryKey = false )]
        public int? battv
        {
            get => _battv;
            set
            {
                var _prvbattv = _battv;
                _battv = value; 
                OnChangedValue("battv", value, _prvbattv);
            }
        }
        public bool battvChanged => GetChangedValue("battv");
        public int? battvOldValue => (int?)GetValue("battv", ModelValueVersion.OldVersion);
        private int? _battv = default(int?);

        /// <summary>
        /// TKTYPE Field
        /// </summary>
        [DataMember(Name = "TKTYPE")]
        [Field(null, PrimeryKey = false )]
        public string? tktype
        {
            get => _tktype;
            set
            {
                var _prvtktype = _tktype;
                _tktype = value; 
                OnChangedValue("tktype", value, _prvtktype);
            }
        }
        public bool tktypeChanged => GetChangedValue("tktype");
        public string? tktypeOldValue => (string?)GetValue("tktype", ModelValueVersion.OldVersion);
        private string? _tktype = default(string?);

        /// <summary>
        /// LKOPM Field
        /// </summary>
        [DataMember(Name = "LKOPM")]
        [Field(null, PrimeryKey = false )]
        public string? lkopm
        {
            get => _lkopm;
            set
            {
                var _prvlkopm = _lkopm;
                _lkopm = value; 
                OnChangedValue("lkopm", value, _prvlkopm);
            }
        }
        public bool lkopmChanged => GetChangedValue("lkopm");
        public string? lkopmOldValue => (string?)GetValue("lkopm", ModelValueVersion.OldVersion);
        private string? _lkopm = default(string?);

        /// <summary>
        /// LKACT Field
        /// </summary>
        [DataMember(Name = "LKACT")]
        [Field(null, PrimeryKey = false )]
        public string? lkact
        {
            get => _lkact;
            set
            {
                var _prvlkact = _lkact;
                _lkact = value; 
                OnChangedValue("lkact", value, _prvlkact);
            }
        }
        public bool lkactChanged => GetChangedValue("lkact");
        public string? lkactOldValue => (string?)GetValue("lkact", ModelValueVersion.OldVersion);
        private string? _lkact = default(string?);

        /// <summary>
        /// WKPL Field
        /// </summary>
        [DataMember(Name = "WKPL")]
        [Field(null, PrimeryKey = false )]
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
        /// AREA Field
        /// </summary>
        [DataMember(Name = "AREA")]
        [Field(null, PrimeryKey = false )]
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
        /// EMPID Field
        /// </summary>
        [DataMember(Name = "EMPID")]
        [Field(null, PrimeryKey = false )]
        public string? empid
        {
            get => _empid;
            set
            {
                var _prvempid = _empid;
                _empid = value; 
                OnChangedValue("empid", value, _prvempid);
            }
        }
        public bool empidChanged => GetChangedValue("empid");
        public string? empidOldValue => (string?)GetValue("empid", ModelValueVersion.OldVersion);
        private string? _empid = default(string?);

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
        /// RANK Field
        /// </summary>
        [DataMember(Name = "RANK")]
        [Field(null, PrimeryKey = false )]
        public string? rank
        {
            get => _rank;
            set
            {
                var _prvrank = _rank;
                _rank = value; 
                OnChangedValue("rank", value, _prvrank);
            }
        }
        public bool rankChanged => GetChangedValue("rank");
        public string? rankOldValue => (string?)GetValue("rank", ModelValueVersion.OldVersion);
        private string? _rank = default(string?);

        /// <summary>
        /// POSITION Field
        /// </summary>
        [DataMember(Name = "POSITION")]
        [Field(null, PrimeryKey = false )]
        public string? position
        {
            get => _position;
            set
            {
                var _prvposition = _position;
                _position = value; 
                OnChangedValue("position", value, _prvposition);
            }
        }
        public bool positionChanged => GetChangedValue("position");
        public string? positionOldValue => (string?)GetValue("position", ModelValueVersion.OldVersion);
        private string? _position = default(string?);

        /// <summary>
        /// ENTTYP Field
        /// </summary>
        [DataMember(Name = "ENTTYP")]
        [Field(null, PrimeryKey = false )]
        public string? enttyp
        {
            get => _enttyp;
            set
            {
                var _prventtyp = _enttyp;
                _enttyp = value; 
                OnChangedValue("enttyp", value, _prventtyp);
            }
        }
        public bool enttypChanged => GetChangedValue("enttyp");
        public string? enttypOldValue => (string?)GetValue("enttyp", ModelValueVersion.OldVersion);
        private string? _enttyp = default(string?);

        /// <summary>
        /// CONFNO Field
        /// </summary>
        [DataMember(Name = "CONFNO")]
        [Field(null, PrimeryKey = false )]
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
        [Field(null, PrimeryKey = false )]
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
        /// LAT Field
        /// </summary>
        [DataMember(Name = "LAT")]
        [Field(null, PrimeryKey = false )]
        public object? lat
        {
            get => _lat;
            set
            {
                var _prvlat = _lat;
                _lat = value; 
                OnChangedValue("lat", value, _prvlat);
            }
        }
        public bool latChanged => GetChangedValue("lat");
        public object? latOldValue => (object?)GetValue("lat", ModelValueVersion.OldVersion);
        private object? _lat = default(object?);

        /// <summary>
        /// LON Field
        /// </summary>
        [DataMember(Name = "LON")]
        [Field(null, PrimeryKey = false )]
        public object? lon
        {
            get => _lon;
            set
            {
                var _prvlon = _lon;
                _lon = value; 
                OnChangedValue("lon", value, _prvlon);
            }
        }
        public bool lonChanged => GetChangedValue("lon");
        public object? lonOldValue => (object?)GetValue("lon", ModelValueVersion.OldVersion);
        private object? _lon = default(object?);

        /// <summary>
        /// WOTYP Field
        /// </summary>
        [DataMember(Name = "WOTYP")]
        [Field(null, PrimeryKey = false )]
        public string? wotyp
        {
            get => _wotyp;
            set
            {
                var _prvwotyp = _wotyp;
                _wotyp = value; 
                OnChangedValue("wotyp", value, _prvwotyp);
            }
        }
        public bool wotypChanged => GetChangedValue("wotyp");
        public string? wotypOldValue => (string?)GetValue("wotyp", ModelValueVersion.OldVersion);
        private string? _wotyp = default(string?);

        /// <summary>
        /// WONO Field
        /// </summary>
        [DataMember(Name = "WONO")]
        [Field(null, PrimeryKey = false )]
        public string? wono
        {
            get => _wono;
            set
            {
                var _prvwono = _wono;
                _wono = value; 
                OnChangedValue("wono", value, _prvwono);
            }
        }
        public bool wonoChanged => GetChangedValue("wono");
        public string? wonoOldValue => (string?)GetValue("wono", ModelValueVersion.OldVersion);
        private string? _wono = default(string?);

        /// <summary>
        /// EQNO Field
        /// </summary>
        [DataMember(Name = "EQNO")]
        [Field(null, PrimeryKey = false )]
        public string? eqno
        {
            get => _eqno;
            set
            {
                var _prveqno = _eqno;
                _eqno = value; 
                OnChangedValue("eqno", value, _prveqno);
            }
        }
        public bool eqnoChanged => GetChangedValue("eqno");
        public string? eqnoOldValue => (string?)GetValue("eqno", ModelValueVersion.OldVersion);
        private string? _eqno = default(string?);

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
    /// TksegModel(tkseg) Model List Class
    /// </summary>    
    public class TksegModelList : XNDIMModelList<TksegModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public TksegModelList()
        {
            TableName = "tkseg";
        }

        /// <summary>
        /// 생성자
        /// </summary>
        public TksegModelList(string dataSource): base("tkseg", dataSource)
        {
            return;
        }
    }

}