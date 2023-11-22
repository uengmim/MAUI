using Camera.MAUI;
using Camera.MAUI.Test;
using WorkerScreen.Models;
using WorkerScreen.ViewModel;
using XNSC.DD.EX;

namespace WorkerScreen.Views;

public partial class GetOnDocument : ContentPage
{


    public GetOnDocument()
    {

        InitializeComponent();
        GetOnViewModel getOnViewModel = new GetOnViewModel();
        this.BindingContext = getOnViewModel;
    }

}