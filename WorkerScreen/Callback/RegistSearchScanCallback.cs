
using WorkerScreen.ViewModel;

namespace WorkerScreen
{
    /// <summary>
    /// Lock Scan Callback
    /// </summary>
    public partial class RegistSearchScanCallback
    {
        /// <summary>
        /// View 모델
        /// </summary>
        public QRRecogViewModel ViewModel { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModel"></param>
        public RegistSearchScanCallback(QRRecogViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
