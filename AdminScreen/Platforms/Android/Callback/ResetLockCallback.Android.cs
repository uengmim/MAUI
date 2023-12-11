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
    public class ResetLockCallback : Java.Lang.Object,  IResetLockCallback
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
        /// Lock 초기화 성공
        /// </summary>
        public void OnResetLockSuccess()
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Application.Current.MainPage.DisplayAlert("확인", "Lock 초기화를 하였습니다.", "OK");
            });
        }
    }
}
