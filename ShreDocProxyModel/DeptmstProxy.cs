// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "deptmst" -output "DeptmstProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

using XNSC;
using XNSC.DD;
using XNSC.DD.EX;

namespace ShreDoc.ProxyModel
{
/// <summary>
    /// DeptmstModel(deptmst) Proxy class
    /// </summary>
    public class DeptmstModel
    {
        /// <summary>
        /// DEPTID(deptid) Field
        /// </summary>
        public string? DEPTID { get; set; }

        /// <summary>
        /// WKPL(wkpl) Field
        /// </summary>
        public string? WKPL { get; set; }

        /// <summary>
        /// DEPTNM(deptnm) Field
        /// </summary>
        public string? DEPTNM { get; set; }

        /// <summary>
        /// DEPTTYP(depttyp) Field
        /// </summary>
        public string? DEPTTYP { get; set; }

        /// <summary>
        /// RECSTA(recsta) Field
        /// </summary>
        public string? RECSTA { get; set; }

        /// <summary>
        /// PDEPTID(pdeptid) Field
        /// </summary>
        public string? PDEPTID { get; set; }

        /// <summary>
        /// SEQ(seq) Field
        /// </summary>
        public int? SEQ { get; set; }

        /// <summary>
        /// REFDA1(rEFDA1) Field
        /// </summary>
        public string? REFDA1 { get; set; }

        /// <summary>
        /// REFDA2(rEFDA2) Field
        /// </summary>
        public string? REFDA2 { get; set; }

        /// <summary>
        /// REFDA3(rEFDA3) Field
        /// </summary>
        public string? REFDA3 { get; set; }

        /// <summary>
        /// REFDA4(rEFDA4) Field
        /// </summary>
        public string? REFDA4 { get; set; }

        /// <summary>
        /// REFDA5(rEFDA5) Field
        /// </summary>
        public string? REFDA5 { get; set; }

        /// <summary>
        /// REFDT1(rEFDT1) Field
        /// </summary>
        public DateTime? REFDT1 { get; set; }

        /// <summary>
        /// REFDT2(rEFDT2) Field
        /// </summary>
        public DateTime? REFDT2 { get; set; }

        /// <summary>
        /// REFDT3(rEFDT3) Field
        /// </summary>
        public DateTime? REFDT3 { get; set; }

        /// <summary>
        /// CRTUSR(crtusr) Field
        /// </summary>
        public string? CRTUSR { get; set; }

        /// <summary>
        /// CRTDT(crtdt) Field
        /// </summary>
        public DateTime? CRTDT { get; set; }

        /// <summary>
        /// UPDUSR(updusr) Field
        /// </summary>
        public string? UPDUSR { get; set; }

        /// <summary>
        /// UPDDT(upddt) Field
        /// </summary>
        public DateTime? UPDDT { get; set; }

   
        /// <summary>
        /// 모델의 상태
        /// </summary>
        public DIMModelStatus ModelStatus { get; set; }

    }

/// <summary>
    /// DeptmstModel(deptmst) Proxy List Class
    /// </summary>    
    public class DeptmstModelList  : List<DeptmstModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public DeptmstModelList()
        {
            return;
        }
    }

}