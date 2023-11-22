using Camera.MAUI;
using Camera.MAUI.Test;
using WorkerScreen.Models;
using WorkerScreen.ViewModel;
using XNSC.DD.EX;

namespace WorkerScreen.Views;

public partial class GetOffDocument : ContentPage
{


    public GetOffDocument()
    {

        InitializeComponent();
        GetOffViewModel getOffViewModel = new GetOffViewModel();
        this.BindingContext = getOffViewModel;
    }

}