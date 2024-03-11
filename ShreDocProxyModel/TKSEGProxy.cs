// iMATE Auto generator Code
// (C)Copyright 2023 ISTN
// RUN : imatecc gen_md -title shredoc -object "tkseg" -output "TKSEGProxy.cs" -nspace "ShreDoc.ProxyModel" -mtype "proxy"

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
    /// TksegModel(tkseg) Proxy class
    /// </summary>
    public class TksegModel
    {
        /// <summary>
        /// TKID(tkid) Field
        /// </summary>
        public string? TKID { get; set; }

        /// <summary>
        /// LSN(lsn) Field
        /// </summary>
        public string? LSN { get; set; }

        /// <summary>
        /// ILSID(ilsid) Field
        /// </summary>
        public string? ILSID { get; set; }

        /// <summary>
        /// EVTDT(evtdt) Field
        /// </summary>
        public DateTime? EVTDT { get; set; }

        /// <summary>
        /// CONDT(condt) Field
        /// </summary>
        public DateTime? CONDT { get; set; }

        /// <summary>
        /// BATTV(battv) Field
        /// </summary>
        public int? BATTV { get; set; }

        /// <summary>
        /// TKTYPE(tktype) Field
        /// </summary>
        public string? TKTYPE { get; set; }

        /// <summary>
        /// LKOPM(lkopm) Field
        /// </summary>
        public string? LKOPM { get; set; }

        /// <summary>
        /// LKACT(lkact) Field
        /// </summary>
        public string? LKACT { get; set; }

        /// <summary>
        /// WKPL(wkpl) Field
        /// </summary>
        public string? WKPL { get; set; }

        /// <summary>
        /// AREA(area) Field
        /// </summary>
        public string? AREA { get; set; }

        /// <summary>
        /// EMPID(empid) Field
        /// </summary>
        public string? EMPID { get; set; }

        /// <summary>
        /// DEPTID(deptid) Field
        /// </summary>
        public string? DEPTID { get; set; }

        /// <summary>
        /// RANK(rank) Field
        /// </summary>
        public string? RANK { get; set; }

        /// <summary>
        /// POSITION(position) Field
        /// </summary>
        public string? POSITION { get; set; }

        /// <summary>
        /// ENTTYP(enttyp) Field
        /// </summary>
        public string? ENTTYP { get; set; }

        /// <summary>
        /// CONFNO(confno) Field
        /// </summary>
        public string? CONFNO { get; set; }

        /// <summary>
        /// REPTYP(reptyp) Field
        /// </summary>
        public string? REPTYP { get; set; }

        /// <summary>
        /// LAT(lat) Field
        /// </summary>
        public object? LAT { get; set; }

        /// <summary>
        /// LON(lon) Field
        /// </summary>
        public object? LON { get; set; }

        /// <summary>
        /// WOTYP(wotyp) Field
        /// </summary>
        public string? WOTYP { get; set; }

        /// <summary>
        /// WONO(wono) Field
        /// </summary>
        public string? WONO { get; set; }

        /// <summary>
        /// EQNO(eqno) Field
        /// </summary>
        public string? EQNO { get; set; }

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
        /// CRTDT(crtdt) Field
        /// </summary>
        public DateTime? CRTDT { get; set; }

   
        /// <summary>
        /// 모델의 상태
        /// </summary>
        public DIMModelStatus ModelStatus { get; set; }

    }

/// <summary>
    /// TksegModel(tkseg) Proxy List Class
    /// </summary>    
    public class TksegModelList  : List<TksegModel>
    {
        /// <summary>
        /// 생성자
        /// </summary>
        public TksegModelList()
        {
            return;
        }
    }

}