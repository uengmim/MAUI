using AdminScreen.ViewModels;
using ShreDoc.Utils;
using System.Net;
using XNSC.DD;
#if __IOS__
using Foundation;
#endif

namespace AdminScreen.Views
{
    public partial class DocCrushingReportPage : ContentPage
    {
        /// <summary>
        /// Confno
        /// </summary>
        public string ConfNO
        {
            get => _confno;
            set
            {
                _confno = value;
                OnPropertyChanged(nameof(ConfNO));
            }
        }
        private string _confno = "";

        /// <summary>
        /// 문서 폐기 보고서 화면
        /// </summary>
        /// <param name="CONFNO">ConfNO</param>
        public DocCrushingReportPage(string CONFNO)
        {
            InitializeComponent();
            ConfNO = CONFNO;

            Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("pdfview", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Settings.JavaScriptEnabled = true;
                handler.PlatformView.Settings.AllowFileAccess = true;
                handler.PlatformView.Settings.AllowFileAccessFromFileURLs = true;
                handler.PlatformView.Settings.AllowUniversalAccessFromFileURLs = true;
#elif IOS
#endif
            });
        }
        /// <summary>
        /// pdf 로딩
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                //로딩 start
                loadingIndicator.IsRunning = true;
                loadingIndicator.IsVisible = true;

                var docService = ImateHelper.GetSingleTone().DocumentService;

                //파라미터 사전
                var paramDic = new Dictionary<string, string>
        {
            { "dbTitle", "shredoc" },
            { "CONFNO", ConfNO}
        };

                var data = await docService.Print("CrushingReport", paramDic, false, "default", true);
                // 대기 시간 추가
                await Task.Delay(1000);
#if __ANDROID__

                MainThread.BeginInvokeOnMainThread(async() =>
                {
                    // 파일 경로 설정
                    var path = Path.Combine(FileSystem.AppDataDirectory, "CrushingReport.pdf");

                    // 파일 저장
                    File.WriteAllBytes(path, data);

                    await Task.Delay(100); // 대기 시간 추가

                    // 파일 경로 인코딩 수정
                    var encodedPath = Uri.EscapeDataString(path);

                    string pdfFilePath = $"file:///android_asset/pdfjs/web/viewer.html?file={encodedPath}";
                    pdfview.Source = new UrlWebViewSource { Url = pdfFilePath };
                });
#elif __IOS__
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    string pdfFileName = "CrushingReport.pdf";
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string pdfFilePath = Path.Combine(documentsPath, pdfFileName);

                    // 파일 저장
                    File.WriteAllBytes(pdfFilePath, data);

                    var pdfUrl = NSUrl.FromFilename(pdfFilePath);

                    var webViewSource = new UrlWebViewSource
                    {
                        Url = pdfUrl.AbsoluteString
                    };
                    pdfview.Source = webViewSource;
                });
#endif
            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
            }
            finally
            {
                //로딩 end
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
            }

        }

        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            //뒤로가기
            Application.Current.MainPage.Navigation.PopAsync();
        }

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