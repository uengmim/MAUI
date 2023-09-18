using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    internal class QRRecog
    {
        public string Name => "작업자 : " + AppInfo.Name;
        public string BoxNum => "보안 문서함 : " + AppInfo.VersionString;

        public string Location => "위치 : " + "주식회사 ISTN 901호";
    }
}
