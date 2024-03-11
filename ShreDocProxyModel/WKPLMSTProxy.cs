// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "wkplmst" -output "WKPLMSTProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

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
    /// WKPLMSTModel(wkplmst) Proxy class
    /// </summary>
    public class WkplmstModel
    {
        /// <summary>
        /// WKPL(wkpl) Field
        /// </summary>
        public string? WKPL { get; set; }

        /// <summary>
        /// WKPLNM(wkplnm) Field
        /// </summary>
        public string? WKPLNM { get; set; }

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
    /// AreamstModel(areamst) Proxy List Class
    /// </summary>    
    public class WkplmstModelList  : List<WkplmstModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public WkplmstModelList()
        {
            return;
        }
    }

}