
using AdminScreen.ViewModels;
using AndroidX.Lifecycle;

namespace AdminScreen
{
    /// <summary>
    /// Lock Scan Callback
    /// </summary>
    public partial class InitLockCallback
    {
        /// <summary>
        /// View 모델
        /// </summary>
        public LockRegigterModel ViewModel { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModel">뷰모델</param>
        public InitLockCallback(LockRegigterModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
