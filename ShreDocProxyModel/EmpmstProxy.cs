// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "empmst" -output "EmpmstProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

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
    /// EmpmstModel(empmst) Proxy class
    /// </summary>
    public class EmpmstModel
    {
        /// <summary>
        /// EMPNO(empno) Field
        /// </summary>
        public string? EMPNO { get; set; }

        /// <summary>
        /// DEPTID(deptid) Field
        /// </summary>
        public string? DEPTID { get; set; }

        /// <summary>
        /// EMPNM(empnm) Field
        /// </summary>
        public string? EMPNM { get; set; }

        /// <summary>
        /// RANK(rank) Field
        /// </summary>
        public string? RANK { get; set; }

        /// <summary>
        /// POSITION(position) Field
        /// </summary>
        public string? POSITION { get; set; }

        /// <summary>
        /// PIN(pin) Field
        /// </summary>
        public string? PIN { get; set; }

        /// <summary>
        /// RECSTA(recsta) Field
        /// </summary>
        public string? RECSTA { get; set; }

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
    /// EmpmstModel(empmst) Proxy List Class
    /// </summary>    
    public class EmpmstModelList  : List<EmpmstModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public EmpmstModelList()
        {
            return;
        }
    }

}