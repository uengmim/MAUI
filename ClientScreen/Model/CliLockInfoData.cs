using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace ClientScreen.Models
{
    public class CliLockInfoData : INotifyPropertyChanged
    {

        private string lsn;
        private string mac;
        private string lktyp;
        private string lknm;
        private string confno;
        private string areanm;
        private Color backgroundcolorset;
        private Color stackgroundcolor;


        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public string LSN { get { return lsn; } set { lsn = value; OnPropertyChanged(nameof(LSN)); } }
        public string MAC { get { return mac; } set { mac = value; OnPropertyChanged(nameof(MAC)); } }
        public string LKTYP { get { return lktyp; } set { lktyp = value; OnPropertyChanged(nameof(LKTYP)); } }
        public string LKNM { get { return lknm; } set { lknm = value; OnPropertyChanged(nameof(LKNM)); } }
        public string CONFNO { get { return confno; } set { confno = value; OnPropertyChanged(nameof(CONFNO)); } }
        public string AREANM { get { return areanm; } set { areanm = value; OnPropertyChanged(nameof(AREANM)); } }
        public Color BackgroundColorSet { get { return backgroundcolorset; } set { backgroundcolorset = value; OnPropertyChanged(nameof(BackgroundColorSet)); } }
        public Color StackgroundColor { get { return stackgroundcolor; } set { stackgroundcolor = value; OnPropertyChanged(nameof(StackgroundColor)); } }


        public CliLockInfoData(string lsn, string mac, string lktyp, string lknm, string confno, string areanm, Color backgroundcolorset, Color stackgroundcolor)
        {
            LSN = lsn;
            MAC = mac;
            LKTYP = lktyp;
            LKNM = lknm;
            CONFNO = confno;
            AREANM = areanm;
            BackgroundColorSet = backgroundcolorset;
            StackgroundColor = stackgroundcolor;
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