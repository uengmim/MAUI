using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using Mapsui.UI.Maui;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using XNSC.DD.EX;

namespace AdminScreen.Views
{
    public partial class LockRecoveryDetailPage : ContentPage
    {

        public LockRecoveryDetailPage(string Pin)
        {
            InitializeComponent();
            LoginInfo = new LoginInfo();

            LockRecoveryDetailViewModel lockRecoveryDetailViewModel = new LockRecoveryDetailViewModel();
            PIN = Pin;

            this.BindingContext = lockRecoveryDetailViewModel;
            //notesCollection.ItemsSource = (System.Collections.IEnumerable)LockData;

        }
        /// <summary>
        /// 로그인한 사용자 정보입니다.
        /// </summary>
        public LoginInfo LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                OnPropertyChanged(nameof(LoginInfo));
            }
        }
        private LoginInfo loginInfo;
        /// <summary>
        /// isLoading
        /// </summary>
        private bool isLoading;

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

        /// <summary>
        /// DptId
        /// </summary>
        private string _deptId = "";

        /// <summary>
        /// EmpNo
        /// </summary>
        private string _empNo = "";

        /// <summary>
        /// EmpNo
        /// </summary>
        private string _pin = "";

        /// <summary>
        /// Deptid
        /// </summary>
        public string DEPTID
        {
            get => _deptId;
            set
            {
                _deptId = value;
                OnPropertyChanged(nameof(DEPTID));
            }
        }

        /// <summary>
        /// Empno
        /// </summary>
        public string EMPNO
        {
            get => _empNo;
            set
            {
                _empNo = value;
                OnPropertyChanged(nameof(EMPNO));
            }
        }

        /// <summary>
        /// Pin
        /// </summary>
        public string PIN
        {
            get => _pin;
            set
            {
                _pin = value;
                OnPropertyChanged(nameof(PIN));
            }
        }

        protected override async void OnAppearing()
        {
            IsLoading = true;
            base.OnAppearing();

            if (BindingContext is LockRecoveryDetailViewModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
            IsLoading = false;
        }

        //클릭 이벤트 처리
        public async void HandleSelectionButton_Clicked(object sender, EventArgs e)
        {
            IsLoading = true;

            try
            {
                var selectedItems = notesCollection.SelectedItems;
                var dataService = ImateHelper.GetSingleTone();
                var lkmstModelList = new LkmstModelList();
                var siehisModelList = new SiehisModelList();
                DateTime currentDate = DateTime.Now;
                string selectData = "";

                if (selectedItems.Count == 0)
                {
                    await ShowCustomAlert("알림", "회수하실 자물쇠를 선택해주세요.", "확인", "");
                    IsLoading = false;
                    return;
                }

                // 모델 업데이트
                foreach (LockSearchData item in selectedItems)
                {
                    try
                    {
                        selectData = item.LSN;
                        lkmstModelList.Add(new LkmstModel()
                        {
                            LKNM = item.LKNM,
                            LSN = item.LSN,
                            MAC = item.MAC,
                            LKSTA = "L",
                            QCTOT = 0,
                            QCCNT = 0,
                            REFDA1 = null,
                            ILSID = null,
                            REFDT1 = null,
                            ModelStatus = DIMModelStatus.Modify,
                        });

                        // SIEHIS 조회
                        var whereSiehisCondition = new DIMGroupFieldCondtion()
                        {
                            condition = DIMGroupCondtion.AND,
                            joinCondtion = DIMGroupCondtion.AND,
                            whereFieldConditions = new DIMWhereFieldCondition[]
                            {
                                new DIMWhereFieldCondition{ fieldName = "CONFNO" , value = item.CONFNO, condition = DIMWhereCondition.Equal },
                            }
                        };

                        var siehisData = await ImateHelper.SelectModelData<SiehisModelList>(App.ServerID, whereSiehisCondition, new Dictionary<string, XNSC.DIMSortOrder>());

                        var hisSearchData = siehisData.FirstOrDefault(h => h.CONFNO == siehisData[0].CONFNO);

                        hisSearchData.CSTATUS = "C";
                        hisSearchData.REFDT1 = currentDate;
                        hisSearchData.ModelStatus = DIMModelStatus.Modify;
                        siehisModelList.Add(hisSearchData);

                        // TTLOCK
                        string ttGuidStr = Guid.NewGuid().ToString();
                        var location = await Geolocation.GetLastKnownLocationAsync();
                        if (location == null)
                        {
                            location = await Geolocation.GetLocationAsync(new GeolocationRequest()
                            {
                                DesiredAccuracy = GeolocationAccuracy.High,
                                Timeout = TimeSpan.FromSeconds(30)
                            });
                        }
                        dataService.Ttlock.ActiveLog(new XNSC.Net.NOKE.LockActivity()
                        {
                            trackingKey = ttGuidStr,
                            lockSN = item.LSN,
                            eventTime = currentDate,
                            ReportType = "Recovery",
                            userId = PIN,
                            lockStatus = "L",
                            opeationMode = "ON",
                            connectTime = currentDate,
                        });

                    }
                    catch (Exception ex)
                    {
                        await ShowCustomAlert("알림", ex.Message, "확인", "");
                        IsLoading = false;
                        return;
                    }
                }

                // 메시지 표시
                bool answer = await ShowCustomAlert("알림", "자물쇠를 회수 하시겠습니까?", "확인", "취소");

                if (!answer)
                {
                    IsLoading = false;
                    return; // 취소 선택 시 함수 종료
                }

                var lkmstResult = await ImateHelper.ModifyModelData(App.ServerID, lkmstModelList);
                var siehisResult = await ImateHelper.ModifyModelData(App.ServerID, siehisModelList);

                // 회수 완료 메시지 표시
                if (selectedItems.Count > 1)
                {
                    await ShowCustomAlert("알림", $"자물쇠 관리번호 [{selectData}] 외 {selectedItems.Count - 1}개를 회수하였습니다.", "확인", "");
                }
                else if (selectedItems.Count == 1)
                {
                    await ShowCustomAlert("알림", $"자물쇠 관리번호 [{selectData}]를 회수하였습니다.", "확인", "");
                }

                // 메인 화면으로 이동
                await Application.Current.MainPage.Navigation.PopModalAsync(animated: false);
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage(LoginInfo));
                IsLoading = false;

            }
            catch (Exception ex)
            {
                await ShowCustomAlert("알림", ex.Message, "확인", "");
                IsLoading = false;
                return;
            }
        }

        private void BackBtn_Clicked(object sender, EventArgs e)
        {
            // 뒤로 가기
            Application.Current.MainPage.Navigation.PopAsync();
        }

        // 팝업 표시 상태를 나타내는 플래그
        private bool isAlertShowing = false;

        // 팝업
        private async Task<bool> ShowCustomAlert(string title, string message, string accept, string cancel)
        {
            // 이미 경고 팝업이 표시 중인 경우 추가적인 처리를 하지 않음
            if (isAlertShowing)
            {
                return false;
            }

            isAlertShowing = true; // 경고 팝업 표시 중임을 표시

            try
            {
                var tcs = new TaskCompletionSource<bool>();

                var alertPage = new CustomAlertPage(title, message, accept, cancel);

                // 확인 버튼 클릭 시 처리
                alertPage.AcceptButtonClicked += (sender, e) =>
                {
                    isAlertShowing = false;
                    tcs.SetResult(true); // true 반환
                };

                // 취소 버튼 클릭 시 처리
                alertPage.CancelButtonClicked += (sender, e) =>
                {
                    isAlertShowing = false;
                    tcs.SetResult(false); // false 반환
                };

                alertPage.Disappearing += (sender, e) => isAlertShowing = false;

                await App.Current.MainPage.Navigation.PushModalAsync(alertPage, animated: false);

                return await tcs.Task;
            }
            finally
            {
                isAlertShowing = false; // 팝업 닫힘을 표시
            }
        }
    }
}