
using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;

namespace AdminScreen.Views;

public partial class LockProvisionDetailPage : ContentPage
{
    public LockProvisionDetailPage(object LockData)
    {

        InitializeComponent();
        ProvisionDetailViewModel provisionDetailViewModel = new ProvisionDetailViewModel();

        this.BindingContext = provisionDetailViewModel;
        provisionDetailViewModel.LockDatas = (System.Collections.ObjectModel.ObservableCollection<LockProvision>)LockData;
        notesCollection.ItemsSource = null;
        notesCollection.ItemsSource = (System.Collections.IEnumerable)LockData;

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProvisionDetailViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }
    }
    //protected override async void OnAppearing()
    //{
    //    loadingIndicator.IsRunning = true;
    //    loadingIndicator.IsVisible = true;
    //    base.OnAppearing();
    //    notesCollection.ItemsSource = null;
    //    loadingIndicator.IsRunning = false;
    //    loadingIndicator.IsVisible = false;
    //}


}