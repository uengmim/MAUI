using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace AdminScreen.Models
{
    public class EmpMstItem : INotifyPropertyChanged
    {
        /// <summary>
        /// 로그인 아이디
        /// </summary>
        private string id;
        private string name;

        public string ID { get { return id; } set { id = value; OnPropertyChanged(nameof(ID)); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged(nameof(Name)); } }

        /// <summary>
        /// 생성자
        /// </summary>
        public EmpMstItem(string id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변겅
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 특성변경
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