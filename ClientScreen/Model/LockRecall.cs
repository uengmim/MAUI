using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace ClientScreen.Model
{
    public class LockRecall
    {

        private string boxname;
        private string location;
        private string ereqid;
        private string status;
        private string lsn;
        private string mac;
        private string lktyp;
        private string lknm;
        private string confno;

        public string BoxName { get { return boxname; } set { boxname = value; OnPropertyChanged(nameof(BoxName)); } }
        public string Location { get { return location; } set { location = value; OnPropertyChanged(nameof(Location)); } }
        public string EreqId { get { return ereqid; } set { ereqid = value; OnPropertyChanged(nameof(EreqId)); } }
        public string Status { get { return status; } set { status = value; OnPropertyChanged(nameof(Status)); } }
        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }
        public string MAC { get { return mac; } set { mac = value; OnPropertyChanged(nameof(MAC)); } }
        public string LKTYP { get { return lktyp; } set { lktyp = value; OnPropertyChanged(nameof(LKTYP)); } }
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }
        public string CONFNO { get { return confno; } set { confno = value; OnPropertyChanged(nameof(CONFNO)); } }


        public LockRecall(string boxname, string location, string ereqid, string status, string lsn, string mac, string lktyp, string lknm, string confno)
        {
            BoxName = boxname;
            Location = location;
            EreqId = ereqid;
            Status = status;
            LSN = lsn;
            MAC = mac;
            LKTYP = lktyp;
            LKNM = lknm;
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
