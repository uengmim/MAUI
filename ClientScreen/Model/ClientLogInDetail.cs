using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace ClientScreen.Model
{
    public class ClientLogInDetail
    {
        /// <summary>
        /// 전화번호
        /// </summary>
        public string DetailNumber { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        public string DetailName { get; set; }

        /// <summary>
        /// 사용할 E-MAIL
        /// </summary>
        public string DetailEmail { get; set; }

        /// <summary>
        /// 사용자 ID
        /// </summary>
        public string DetailID { get; set; }

    }
}