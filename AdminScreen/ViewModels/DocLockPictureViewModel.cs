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
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Reflection;

namespace AdminScreen.ViewModels
{
    /// <summary>
    /// 작업 모니터링 문서
    /// </summary>
    public class DocLockPictureViewModel : INotifyPropertyChanged
    {

        #region Properties
        /// <summary>
        /// PDF Viewer
        /// </summary>
        private Stream m_pdfDocumentStream;
        /// <summary>
        /// The PDF document stream that is loaded into the instance of the PDF viewer. 
        /// </summary>
        public Stream PdfDocumentStream
        {
            get
            {
                return m_pdfDocumentStream;
            }
            set
            {
                m_pdfDocumentStream = value;
                OnPropertyChanged("PdfDocumentStream");
            }
        }
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
        /// 봉인 시간
        /// </summary>
        public DateTime LockCloseTime
        {
            get => _lockcloseTime;
            set
            {
                _lockcloseTime = value;
                OnPropertyChanged(nameof(LockCloseTime));
            }
        }
        private DateTime _lockcloseTime;

        /// <summary>
        /// 봉인 작업자
        /// </summary>
        public string LockWorker
        {
            get => _lockworker;
            set
            {
                _lockworker = value;
                OnPropertyChanged(nameof(LockWorker));
            }
        }
        private string _lockworker = "";
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

        #region DocLockPictureViewModel
        public DocLockPictureViewModel()
        {
            m_pdfDocumentStream = typeof(App).GetTypeInfo().Assembly.GetManifestResourceStream("AdminScreen.Assets.TTLock.pdf");

        }
        #endregion
    }
}