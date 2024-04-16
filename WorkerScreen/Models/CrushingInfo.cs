using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace WorkerScreen.Models
{
    public class CrushingInfo : INotifyPropertyChanged
    {

        private string name;
        private string boxname;
        private string location;
        private string lockdata;
        private DateTime? pickupdate;
        private DateTime? lockdate;
        private string confno;
        private Color backgroundcolorset;

        /// <summary>
        /// 로그인 아이디
        /// </summary>
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }
        public string BoxName { get { return boxname; } set { boxname = value; OnPropertyChanged(nameof(BoxName)); } }
        public string Location { get { return location; } set { location = value; OnPropertyChanged(nameof(Location)); } }
        public string LockData { get { return lockdata; } set { lockdata = value; OnPropertyChanged(nameof(LockData)); } }

        public DateTime? PickupDate { get { return pickupdate; } set { pickupdate = value; OnPropertyChanged(nameof(PickupDate)); } }
        public DateTime? LockDate { get { return lockdate; } set { lockdate = value; OnPropertyChanged(nameof(LockDate)); } }
        public string ConfNo { get { return confno; } set { confno = value; OnPropertyChanged(nameof(ConfNo)); } }
        public Color BackgroundColorSet { get { return backgroundcolorset; } set { backgroundcolorset = value; OnPropertyChanged(nameof(BackgroundColorSet)); } }


        public CrushingInfo(string name ,string boxname, string location, string lockdata, DateTime? pickupdate, DateTime? lockdate, string confno, Color backgroundcolorset)
        {
            Name = name;
            BoxName = boxname;
            Location = location;
            LockData = lockdata;
            PickupDate = pickupdate;
            LockDate = lockdate;
            ConfNo = confno;
            BackgroundColorSet = backgroundcolorset;
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