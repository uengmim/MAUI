using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;

namespace AdminScreen
{
    /// <summary>
    /// Lock Scan Callback
    /// </summary>
    public partial class InitLockCallback : Java.Lang.Object, IInitLockCallback
    {


        /// <summary>
        /// 자물쇠 초기화 실패
        /// </summary>
        /// <param name="p0"></param>
        public void OnFail(LockError p0)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current.MainPage.DisplayAlert("오류", p0.ErrorMsg, "OK");
            });
            //Platform.CurrentActivity;
        }

        /// <summary>
        /// 자물쇠 초기화 성공
        /// </summary>
        /// <param name="p0"></param>
        public void OnInitLockSuccess(string p0)
        {
            try
            {
                var dataService = ImateHelper.GetSingleTone();

                var result = dataService.Ttlock.LockInitialize(new XNSC.Net.Ttlock.TtlockInfo()
                {
                    lockName = ViewModel.LockInfo.LockName,
                    lockMacAddr = ViewModel.LockInfo.LockMac,
                    lockData = p0
                });


                //lockname 업데이트
                var lkmstModelList = new LkmstModelList();
                lkmstModelList.Add(new LkmstModel()//add  말고 모디파이로
                {
                    LKNM = ViewModel.LockInfo.LockName,
                    LSN = result,
                    MAC = ViewModel.LockInfo.LockMac,
                    LKAKA = ViewModel.Locknm,
                    ASSETS = ViewModel.LockNumber,
                    QCTOT = 0,
                    QCCNT = 0,
                    ModelStatus = DIMModelStatus.Modify
                });

                Task.Factory.StartNew(async () =>
                {
                    var lkmstResult = await dataService.Adapter.ModifyModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList", lkmstModelList);
                    await Application.Current.MainPage.DisplayAlert("알림", "자물쇠 등록이 완료되었습니다.", "확인");
                    await Application.Current.MainPage.Navigation.PushAsync(Application.Current.MainPage);
                });
            }
            catch (Exception e)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage.DisplayAlert("오류", e.Message, "OK");
                });

                //
                //초기화 오류시 공장 초기화 상태로 되돌린다.
                //
                ResetLock(p0, ViewModel.LockInfo.LockMac);
            }
            finally
            {
                try
                {
                    TTLockClient.Default.StopScanLock();
                    TTLockClient.Default.StopBTService();
                    TTLockClient.Default.ClearAllCallback();
                }
                catch(Exception ie)
                {
                    Console.WriteLine(ie.ToString());
                }

            }
        }

        /// <summary>
        /// Lock을 Reset한다,
        /// </summary>
        /// <param name="lockData"></param>
        /// <param name="macAddress"></param>
        private void ResetLock(string lockData, string macAddress)
        {
            try
            {
                TTlockHelper.ResetLock(lockData, macAddress, new ResetLockCallback());
            }
            catch (Exception e)
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage.DisplayAlert("오류", e.Message, "OK");
                });
            }
        }
    }
}
