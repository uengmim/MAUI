
using AdminScreen.ViewModels;

namespace AdminScreen
{
    /// <summary>
    /// Lock Scan Callback
    /// </summary>
    public partial class RegistSearchScanCallback
    {
        /// <summary>
        /// View 모델
        /// </summary>
        public RegisterSearchModel ViewModel { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModel"></param>
        public RegistSearchScanCallback(RegisterSearchModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
