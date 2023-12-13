using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class EmpMstItem : INotifyPropertyChanged
    {

        private string id;
        private string name;
        /// <summary>
        /// 작업자
        /// </summary>
        public string ID { get { return id; } set { id = value; OnPropertyChanged(nameof(ID)); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }


        public EmpMstItem(string id, string name)
        {
            ID = id;
            Name = name;
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