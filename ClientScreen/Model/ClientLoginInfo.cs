using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClientScreen.Model
{
    public class ClientLoginInfo
    {
        //로그인 아이디
        public string ClientID { get; set; }
        public string ClientPW { get; set; }
        public string ClientName{ get; set; }
        public string EMPNO { get; set; }
        public string DEPTID { get; set; }
        public string ClientDep { get; set; }

    }
}