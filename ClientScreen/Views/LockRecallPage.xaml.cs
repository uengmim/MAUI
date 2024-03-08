using ClientScreen.Model;
using ClientScreen.ViewModels;
using ShreDoc.Utils;
using System.Collections.ObjectModel;

namespace ClientScreen.Views
{

    public partial class LockRecallPage : ContentPage
    {
        public LockRecallPage(string DeptId, string Wkpl, string Empno, string Refda2)
        {
            InitializeComponent();

            LockRecallModel lockRecallModel = new LockRecallModel();
            lockRecallModel.DEPTID = DeptId;
            lockRecallModel.WKPL = Wkpl;
            lockRecallModel.EMPNO = Empno;
            lockRecallModel.REFDA2 = Refda2;





            this.BindingContext = lockRecallModel;

        }

        protected override async void OnAppearing()
        {
            loadingIndicator.IsRunning = true;
            loadingIndicator.IsVisible = true;
            base.OnAppearing();

            if (BindingContext is LockRecallModel viewModel)
            {
                await viewModel.ExecuteMyCommand();
            }
            loadingIndicator.IsRunning = false;
            loadingIndicator.IsVisible = false;
        }
    }

}   