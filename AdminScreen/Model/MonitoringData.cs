using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class MonitoringData : INotifyPropertyChanged
    {

        /// <summary>
        /// Lock 유형
        /// </summary>
        private string lktyp;
        /// <summary>
        /// Lock 이름
        /// </summary>
        private string lsn;
        /// <summary>
        /// Lock 이름
        /// </summary>
        private string lknm;

        /// <summary>
        /// 작업자
        /// </summary>
        private string name;

        /// <summary>
        /// 보안 문서함
        /// </summary>
        private string boxname;

        /// <summary>
        /// 위치
        /// </summary>
        private string location;

        /// <summary>
        /// 지급시간
        /// </summary>
        private DateTime? provisionDate;

        /// <summary>
        /// 봉인 준비
        /// </summary>
        private DateTime? lockprepare;

        /// <summary>
        /// 봉인 시간
        /// </summary>
        private DateTime? lockClose;

        /// <summary>
        /// 상차 시간
        /// </summary>
        private DateTime? getOn;

        /// <summary>
        /// 하차 시간
        /// </summary>
        private DateTime? getOff;

        /// <summary>
        /// 봉인 해제 시간
        /// </summary>
        private DateTime? lockoff;

        /// <summary>
        /// 파쇄 시간
        /// </summary>
        private DateTime? crushing;

        /// <summary>
        /// Confno
        /// </summary>
        private string confno;
        /// <summary>
        /// 작업자 이름
        /// </summary>
        private string lockWorker;
        /// <summary>
        /// 파소 수량
        /// </summary>
        private string crushNum;
        /// <summary>
        /// 파쇄 방법
        /// </summary>
        private string crushWay;
        /// <summary>
        /// 봉인 사진
        /// </summary>
        private string lockPicture;
        /// <summary>
        /// 파쇄 사진
        /// </summary>
        private string crushPicture;
        private string myimage;

        /// <summary>
        /// ProvisionDate
        /// </summary>
        public DateTime? ProvisionDate { get { return provisionDate; } set { provisionDate = value; OnPropertyChanged(nameof(ProvisionDate)); } }

        /// <summary>
        /// Lock 유형
        /// </summary>
        public string LKTYP { get { return lktyp; } set { lktyp = value; OnPropertyChanged(nameof(LKTYP)); } }
        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }
        /// <summary>
        /// Lock 이름
        /// </summary>
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }

        /// <summary>
        /// 작업자
        /// </summary>
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }

        /// <summary>
        /// 보안 문서함
        /// </summary>
        public string BoxName { get { return boxname; } set { boxname = value; OnPropertyChanged(nameof(BoxName)); } }

        /// <summary>
        /// 위치
        /// </summary>
        public string Location { get { return location; } set { location = value; OnPropertyChanged(nameof(Location)); } }

        /// <summary>
        /// 봉인 준비
        /// </summary>
        public DateTime? LockPrepare { get { return lockprepare; } set { lockprepare = value; OnPropertyChanged(nameof(LockPrepare)); } }

        /// <summary>
        /// 봉인
        /// </summary>
        public DateTime? LockClose { get { return lockClose; } set { lockClose = value; OnPropertyChanged(nameof(LockClose)); } }

        /// <summary>
        /// 상차
        /// </summary>
        public DateTime? GetOn { get { return getOn; } set { getOn = value; OnPropertyChanged(nameof(GetOn)); } }

        /// <summary>
        /// 하차
        /// </summary>
        public DateTime? GetOff { get { return getOff; } set { getOff = value; OnPropertyChanged(nameof(GetOff)); } }

        /// <summary>
        /// 봉인 해제
        /// </summary>
        public DateTime? LockOff { get { return lockoff; } set { lockoff = value; OnPropertyChanged(nameof(LockOff)); } }

        /// <summary>
        /// 파쇄
        /// </summary>
        public DateTime? Crushing { get { return crushing; } set { crushing = value; OnPropertyChanged(nameof(Crushing)); } }

        /// <summary>
        /// Confno
        /// </summary>
        public string CONFNO { get { return confno; } set { confno = value; OnPropertyChanged(nameof(CONFNO)); } }
        /// <summary>
        /// 작업자 이름
        /// </summary>
        public string LockWorker { get { return lockWorker; } set { lockWorker = value; OnPropertyChanged(nameof(LockWorker)); } }
        /// <summary>
        /// 파소 수량
        /// </summary>
        public string CrushNum { get { return crushNum; } set { crushNum = value; OnPropertyChanged(nameof(CrushNum)); } }
        /// <summary>
        /// 파쇄 방법
        /// </summary>
        public string CrushWay { get { return crushWay; } set { crushWay = value; OnPropertyChanged(nameof(CrushWay)); } }
        /// <summary>
        /// 봉인 사진
        /// </summary>
        public string LockPicture { get { return lockPicture; } set { lockPicture = value; OnPropertyChanged(nameof(LockPicture)); } }
        /// <summary>
        /// 파쇄 사진
        /// </summary>
        public string CrushPicture { get { return crushPicture; } set { crushPicture = value; OnPropertyChanged(nameof(CrushPicture)); } }

        /// <summary>
        /// 이미지 변경
        /// </summary>
        public string MyImage { get { return myimage; } set { myimage = value; OnPropertyChanged(nameof(MyImage)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public MonitoringData(DateTime? provisionDate, string lktyp, string lsn, string lknm, string name ,string boxname, string location, 
                              DateTime? lockprepare, DateTime? lockClose, DateTime? getOn, DateTime? getOff, DateTime? lockoff, DateTime? crushing, 
                              string lockWorker, string crushNum, string crushWay, string lockPicture, string crushPicture, string confno, string myimage)
        { 
            ProvisionDate = provisionDate;
            LKTYP = lktyp;
            LSN = lsn;
            LKNM = lknm;
            Name = name;
            BoxName = boxname;
            Location = location;
            LockPrepare = lockprepare;
            LockClose = lockClose;
            GetOn = getOn;
            GetOff = getOff;
            LockOff = lockoff;
            Crushing = crushing;
            LockWorker = lockWorker;
            CrushNum = crushNum;
            CrushWay = crushWay;
            LockPicture = lockPicture;
            CrushPicture = crushPicture;
            CONFNO = confno;
            MyImage = myimage;
        }

        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

    }
}