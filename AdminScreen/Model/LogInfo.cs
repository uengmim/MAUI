using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AdminScreen.Model
{
    public class LoginInfo
    {
        /// <summary>
        /// 로그인 정보
        /// </summary>
        public string AdminID { get; set; }
        public string AdminPW { get; set; }
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }
        public string PIN { get; set; }
        public string EMPNM { get; set; }
    }
}