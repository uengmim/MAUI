using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Xml.Linq;
using WorkerScreenCrushing.Common;
using WorkerScreenCrushing.Models;
using WorkerScreenCrushing.ViewModel;
using XNSC.DD.EX;

namespace WorkerScreenCrushing.Views;

public partial class CrushingDocDetail : ContentPage
{

    public CrushingDocDetail()
    {
        InitializeComponent();
        CrushingDetailViewModel crushingDetailViewModel = new CrushingDetailViewModel();
        crushingDetailViewModel.BackgroundColorSet = Colors.Blue; // 원하는 배경색으로 설정
        crushingDetailViewModel.SelectedItem = null;
        this.BindingContext = crushingDetailViewModel;
    }
    private async void Renew_Clicked(object sender, EventArgs e)
    {

        //loadingIndicator.IsRunning = true;
        //loadingIndicator.IsVisible = true;
        if (BindingContext is CrushingDetailViewModel crushingDetailViewModel)
        {
            await crushingDetailViewModel.ExecuteMyCommand();
        }
        //loadingIndicator.IsRunning = false;
        //loadingIndicator.IsVisible = false;
    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //   if(notesCollection.SelectedItems.Count > 0)
    //    {
    //        notesCollection.SelectedItems = null;
    //    }
    //}


    //protected override bool OnBackButtonPressed()
    //{
    //    App.Current.MainPage = new CrushingDocument();
    //    return false;
    //}
}