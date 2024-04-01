
using ShreDoc.ProxyModel;
using WorkerScreenCrushing.ViewModel;

namespace WorkerScreenCrushing
{
    /// <summary>
    /// Lock Scan Callback
    /// </summary>
    public partial class RegistControlLockCallback
    {


        /// <summary>
        /// 부모 뷰모델
        /// </summary>
        public CrushingDetailViewModel ParentVewModel { get; set; }

        public SierepModelList sierepData { get; set; } = new SierepModelList();
        public SilockModelList silockData { get; set; } = new SilockModelList();
        public SiehisModelList siehisData { get; set; } = new SiehisModelList();
        public LkmstModelList lkMstData { get; set; } = new LkmstModelList();

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="viewModel"></param>
        public RegistControlLockCallback(CrushingDetailViewModel parentVewModel, SierepModelList sierepmodelList,SiehisModelList siehismodelList, LkmstModelList lkMstmodelList)
        {
            ParentVewModel = parentVewModel;
            sierepData = sierepmodelList;
            siehisData = siehismodelList;
            lkMstData = lkMstmodelList;
        }
    }
}
