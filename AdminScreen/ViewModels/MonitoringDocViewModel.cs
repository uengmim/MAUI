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
    /// 작업 모니터링 문서
    /// </summary>
    public class MonitoringDocViewModel : INotifyPropertyChanged
    {
        private bool isNavigating; // 중복 탐색 방지 플래그

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

        #region Commands
        /// <summary>
        /// 증빙 문서 화면 이동입니다.
        /// </summary>
        public ICommand LockPictureCommand => new Command(LockPictureClicked);
        /// <summary>
        /// 문서 폐기 보고서 화면 이동입니다.
        /// </summary>
        public ICommand CrushingReportCommand => new Command(CrushingReportClicked);
        /// <summary>
        /// 파쇄 증명서 화면 이동입니다.
        /// </summary>
        public ICommand CrushingProofCommand => new Command(CrushingProofClicked);
        #endregion
        #region 화면 이동
        /// <summary>
        /// 증빙 문서 화면 이동입니다.
        /// </summary>
        private async void LockPictureClicked()
        {
            try
            {
                if (isNavigating) return; // 중복 클릭 방지
                isNavigating = true;

                if (LockCloseTime == DateTime.MinValue)
                {
                    await ShowCustomAlert("알림", "아직 문서가 봉인되지 않았습니다.", "확인", "");
                    return;
                }
                await Application.Current.MainPage.Navigation.PushAsync(new DocLockPicturePage(CONFNO));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                isNavigating = false;
            }
        }

        /// <summary>
        /// 문서 폐기 보고서 화면 이동입니다.
        /// </summary>
        private async void CrushingReportClicked()
        {
            try
            {
                if (isNavigating) return; // 중복 클릭 방지
                isNavigating = true;

                if (CrushingTime == DateTime.MinValue)
                {
                    await ShowCustomAlert("알림", "아직 문서가 파쇄되지 않았습니다.", "확인", "");
                    return;
                }
                await Application.Current.MainPage.Navigation.PushAsync(new DocCrushingReportPage(CONFNO));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                isNavigating = false;
            }
        }

        /// <summary>
        /// 파쇄 증명서 화면 이동입니다.
        /// </summary>
        private async void CrushingProofClicked()
        {
            try
            {
                if (isNavigating) return; // 중복 클릭 방지
                isNavigating = true;

                if (CrushingTime == DateTime.MinValue)
                {
                    await ShowCustomAlert("알림", "아직 문서가 파쇄되지 않았습니다.", "확인", "");
                    return;
                }
                await Application.Current.MainPage.Navigation.PushAsync(new DocCrushingProofPage(CONFNO));
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                isNavigating = false;
            }
        }

        #endregion

        #region MonitoringDocViewModel
        public MonitoringDocViewModel()
        {
        }
        #endregion

        // 팝업 표시 상태를 나타내는 플래그
        private bool isAlertShowing = false;

        //팝업
        private async Task ShowCustomAlert(string title, string message, string accept, string cancle)
        {
            // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
            if (isAlertShowing)
            {
                return;
            }

            isAlertShowing = true; // 경고 팝업 표시 중임을 표시

            // 팝업 애니메이션 비활성화
            try
            {
                var alertPage = new CustomAlertPage(title, message, accept, cancle);
                alertPage.Disappearing += (sender, e) => isAlertShowing = false;
                await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);
            }
            finally
            {
                isAlertShowing = true;
            }
        }
    }
}