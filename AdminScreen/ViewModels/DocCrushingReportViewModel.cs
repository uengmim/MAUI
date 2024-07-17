using AdminScreen.Common;
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
using XNSC;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 문서 폐기 보고서
    /// </summary>
    public class DocCrushingReportViewModel : INotifyPropertyChanged
    {

        #region Properties
        /// <summary>
        /// 로딩패널
        /// </summary>
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }
        private bool isLoading;
        /// <summary>
        /// 수거 시간
        /// </summary>
        public DateTime GetOnTime
        {
            get => _getonTime;
            set
            {
                _getonTime = value;
                OnPropertyChanged(nameof(GetOnTime));
            }
        }
        private DateTime _getonTime;

        /// <summary>
        /// 파쇄 시간
        /// </summary>
        public DateTime CrushingTime
        {
            get => _crushingTime;
            set
            {
                _crushingTime = value;
                OnPropertyChanged(nameof(CrushingTime));
            }
        }
        private DateTime _crushingTime;
        /// <summary>
        /// 파쇄 수량
        /// </summary>
        public string CrushNum { get; set; }
        /// <summary>
        /// 파쇄 방법
        /// </summary>
        public string CrushWay { get; set; }
        /// <summary>
        /// Confno
        /// </summary>
        public string CONFNO
        {
            get => _confno;
            set
            {
                _confno = value;
                OnPropertyChanged(nameof(CONFNO));
            }
        }
        private string _confno = "";
        /// <summary>
        /// 특성 변경 이벤트
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 특성 변경
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region DocCrushingReportViewModel
        public DocCrushingReportViewModel()
        {
        }
        #endregion
    }
}