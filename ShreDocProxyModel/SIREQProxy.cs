// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "sireq" -output "SIREQProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

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
    /// SireqModel(sireq) Proxy class
    /// </summary>
    public class SireqModel
    {
        /// <summary>
        /// EREQID(ereqid) Field
        /// </summary>
        public string? EREQID { get; set; }

        /// <summary>
        /// EREQTYP(ereqtyp) Field
        /// </summary>
        public string? EREQTYP { get; set; }

        /// <summary>
        /// WDEPTID(wdeptid) Field
        /// </summary>
        public string? WDEPTID { get; set; }

        /// <summary>
        /// WEMPID(wempid) Field
        /// </summary>
        public string? WEMPID { get; set; }

        /// <summary>
        /// ADEPTID(adeptid) Field
        /// </summary>
        public string? ADEPTID { get; set; }

        /// <summary>
        /// AEMPID(aempid) Field
        /// </summary>
        public string? AEMPID { get; set; }

        /// <summary>
        /// EREQDT(ereqdt) Field
        /// </summary>
        public DateTime? EREQDT { get; set; }

        /// <summary>
        /// STRDT(strdt) Field
        /// </summary>
        public DateTime? STRDT { get; set; }

        /// <summary>
        /// ENDDT(enddt) Field
        /// </summary>
        public DateTime? ENDDT { get; set; }

        /// <summary>
        /// ENTRMK(entrmk) Field
        /// </summary>
        public string? ENTRMK { get; set; }

        /// <summary>
        /// SAFEEDU(safeedu) Field
        /// </summary>
        public string? SAFEEDU { get; set; }

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
    /// SireqModel(sireq) Proxy List Class
    /// </summary>    
    public class SireqModelList  : List<SireqModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public SireqModelList()
        {
            return;
        }
    }

}