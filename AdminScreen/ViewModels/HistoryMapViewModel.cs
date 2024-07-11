﻿using AdminScreen.Common;
using AdminScreen.Model;
using AdminScreen.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ShreDoc.ProxyModel;
using XNSC.DD.EX;
using XNSC.Net;
using XNSC.Net.NOKE;
using System.Collections.ObjectModel;
using AdminScreen.Models;
using ShreDoc.Utils;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 운송 경로 조회 화면
    /// </summary>
    public class HistoryMapViewModel
    {
        /// <summary>
        /// CustomMarker 모델
        /// </summary>
        public ObservableCollection<CustomMarker> Markers { get; set; }

        #region HistoryViewModel
        /// <summary>
        /// 위도 경도
        /// </summary>
        public HistoryMapViewModel()
        {
            this.Markers = new ObservableCollection<CustomMarker>();
            var currentTime = DateTime.UtcNow;
            this.Markers.Add(new CustomMarker()
            {
                Latitude = 47.60621,
                Longitude = -122.332071,
                Name = "Seattle",
                Time = currentTime.Subtract(new TimeSpan(7, 0, 0)).ToLongTimeString()
            });


            this.Markers.Add(new CustomMarker()
            {
                Latitude = -1.455833,
                Longitude = -48.503887,
                Name = "Belem",
                Time = currentTime.Subtract(new TimeSpan(3, 0, 0)).ToLongTimeString()
            });

            this.Markers.Add(new CustomMarker()
            {
                Latitude = 64.10,
                Longitude = -51.44,
                Name = "Nuuk",
                Time = currentTime.Subtract(new TimeSpan(2, 0, 0)).ToLongTimeString()
            });

            this.Markers.Add(new CustomMarker()
            {
                Latitude = 62.035452,
                Longitude = 129.675475,
                Name = "Yakutsk",
                Time = currentTime.Add(new TimeSpan(9, 0, 0)).ToLongTimeString()
            });

            this.Markers.Add(new CustomMarker()
            {
                Latitude = 28.704059,
                Longitude = 77.10249,
                Name = "Delhi",
                Time = currentTime.Add(new TimeSpan(5, 30, 0)).ToLongTimeString()
            });

            this.Markers.Add(new CustomMarker()
            {
                Latitude = -27.469771,
                Longitude = 153.025124,
                Name = "Brisbane",
                Time = currentTime.Add(new TimeSpan(10, 0, 0)).ToLongTimeString(),
            });

            this.Markers.Add(new CustomMarker()
            {
                Latitude = -17.825166,
                Longitude = 31.03351,
                Name = "Harare",
                Time = currentTime.Add(new TimeSpan(2, 0, 0)).ToLongTimeString(),
            });
        }

    }
    #endregion
}