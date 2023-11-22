// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "sierep" -output "SIEREPProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

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
    /// SierepModel(sierep) Proxy class
    /// </summary>
    public class SierepModel
    {
        /// <summary>
        /// CONFNO(confno) Field
        /// </summary>
        public string? CONFNO { get; set; }

        /// <summary>
        /// REPTYP(reptyp) Field
        /// </summary>
        public string? REPTYP { get; set; }

        /// <summary>
        /// REPDAT(repdat) Field
        /// </summary>
        public DateTime? REPDAT { get; set; }

        /// <summary>
        /// ETYPE(etype) Field
        /// </summary>
        public string? ETYPE { get; set; }

        /// <summary>
        /// FTYPE(ftype) Field
        /// </summary>
        public string? FTYPE { get; set; }

        /// <summary>
        /// FRMK(frmk) Field
        /// </summary>
        public string? FRMK { get; set; }

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
        /// 모델의 상태
        /// </summary>
        public DIMModelStatus ModelStatus { get; set; }

    }

/// <summary>
    /// SierepModel(sierep) Proxy List Class
    /// </summary>    
    public class SierepModelList  : List<SierepModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public SierepModelList()
        {
            return;
        }
    }

}