using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace AdminScreen.Models
{
    public class CustomMarker : INotifyPropertyChanged
    {
        /// <summary>
        /// 위도
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 경도
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 이름
        /// </summary>
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 상태
        /// </summary>
        private string state;
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                if (state != value)
                {
                    state = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 나라
        /// </summary>
        private string country;
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                if (country != value)
                {
                    country = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 시간
        /// </summary>
        private string time;
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                if (time != value)
                {
                    time = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}