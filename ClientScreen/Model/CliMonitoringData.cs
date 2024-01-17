using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace ClientScreen.Models
{
    public class CliMonitoringData : INotifyPropertyChanged
    {
        private DateTime? provisionDate;
        private string lktyp;
        private string name;
        private string boxname;
        private string location;
        private DateTime? lockprepare;
        private DateTime? lockClose;
        private DateTime? getOn;
        private DateTime? getOff;
        private DateTime? lockoff;
        private DateTime? crushing;
        private string confno;


        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public DateTime? ProvisionDate { get { return provisionDate; } set { provisionDate = value; OnPropertyChanged(nameof(ProvisionDate)); } }
        public string LKTYP { get { return lktyp; } set { lktyp = value; OnPropertyChanged(nameof(LKTYP)); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string BoxName { get { return boxname; } set { boxname = value; OnPropertyChanged(nameof(BoxName)); } }
        public string Location { get { return location; } set { location = value; OnPropertyChanged(nameof(Location)); } }
        public DateTime? LockPrepare { get { return lockprepare; } set { lockprepare = value; OnPropertyChanged(nameof(LockPrepare)); } }
        public DateTime? LockClose { get { return lockClose; } set { lockClose = value; OnPropertyChanged(nameof(LockClose)); } }
        public DateTime? GetOn { get { return getOn; } set { getOn = value; OnPropertyChanged(nameof(GetOn)); } }
        public DateTime? GetOff { get { return getOff; } set { getOff = value; OnPropertyChanged(nameof(GetOff)); } }
        public DateTime? LockOff { get { return lockoff; } set { lockoff = value; OnPropertyChanged(nameof(LockOff)); } }
        public DateTime? Crushing { get { return crushing; } set { crushing = value; OnPropertyChanged(nameof(Crushing)); } }
        public string CONFNO { get { return confno; } set { confno = value; OnPropertyChanged(nameof(CONFNO)); } }



        public CliMonitoringData(DateTime? provisionDate, string lktyp, string name ,string boxname, string location, DateTime? lockprepare, DateTime? lockClose, DateTime? getOn, DateTime? getOff, DateTime? lockoff, DateTime? crushing, string confno)
        {
            ProvisionDate = provisionDate;
            LKTYP = lktyp;
            Name = name;
            BoxName = boxname;
            Location = location;
            LockPrepare = lockprepare;
            LockClose = lockClose;
            GetOn = getOn;
            GetOff = getOff;
            LockOff = lockoff;
            Crushing = crushing;
            CONFNO = confno;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, args);
        }

    }
}