// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "silock" -output "SILOCKProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

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
    /// SilockModel(silock) Proxy class
    /// </summary>
    public class SilockModel
    {
        /// <summary>
        /// EREQID(ereqid) Field
        /// </summary>
        public string? EREQID { get; set; }

        /// <summary>
        /// LSN(lsn) Field
        /// </summary>
        public string? LSN { get; set; }

        /// <summary>
        /// AREA(area) Field
        /// </summary>
        public string? AREA { get; set; }

        /// <summary>
        /// ILSID(ilsid) Field
        /// </summary>
        public string? ILSID { get; set; }

        /// <summary>
        /// ASTATUS(astatus) Field
        /// </summary>
        public string? ASTATUS { get; set; }

        /// <summary>
        /// ASTADT(astadt) Field
        /// </summary>
        public DateTime? ASTADT { get; set; }

        /// <summary>
        /// TKEY(tkey) Field
        /// </summary>
        public string? TKEY { get; set; }

        /// <summary>
        /// CONFNO(confno) Field
        /// </summary>
        public string? CONFNO { get; set; }

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
    /// SilockModel(silock) Proxy List Class
    /// </summary>    
    public class SilockModelList  : List<SilockModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public SilockModelList()
        {
            return;
        }
    }

}