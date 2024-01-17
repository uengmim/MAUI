using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace ClientScreen.Model
{
    public class MembershipLogInfo
    {
        /// <summary>
        /// 전화번호
        /// </summary>
        public string CliNumber { get; set; }

        ///// <summary>
        ///// 고객정보
        ///// </summary>

        public string REFDA1 { get; set; }

        /// <summary>
        /// 회원가입 아이디
         /// </summary>
         public string CliID { get; set; }

        /// <summary>
        /// 회원가입 비밀번호
        /// </summary>
        public string CliPW { get; set; }

        /// <summary>
        /// 회원가입 비밀번호 확인
        /// </summary>
        public string CliPWVer { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string CliName { get; set; }

        /// <summary>
        /// 부서
        /// </summary>
        public string CliDep { get; set; }

        /// <summary>
        /// 직책
        /// </summary>
        public string CliPos { get; set; }

        /// <summary>
        /// 사용할 E-MAIL
        /// </summary>
        public string CliEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EMPNO { get; set; }

        //public MembershipLogInfo(string refda1)
        //{
        //    REFDA1 = refda1;
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        //}

        //protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, args);
        //}
    }
}