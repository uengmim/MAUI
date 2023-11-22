using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerScreen.data
{
    public class User
    {
        [PrimaryKey]
        public string EmpNo { get; set; }

        public string DeptNo { get; set; }

        public string EmpNm { get; set; }

        public string Rank { get; set; }

        public string Position { get; set; }

        public string Pin { get; set; }

        public string Recsta { get; set; }

        public string REFDA1 { get; set; }

        public string REFDA2 { get; set; }

        public string REFDA3 { get; set; }

        public string REFDA4 { get; set; }

        public string REFDA5 { get; set; }

        public DateTime REFDT1 { get; set; }

        public DateTime REFDT2 { get; set; }

        public DateTime REFDT3 { get; set; }

        public string CRTUSR { get; set; }

        public DateTime CRTDT { get; set; }

        public string UPDUSR { get; set; }

        public DateTime UPDDT { get; set; }
    }
}
