
using AdminScreen.Model;
using AdminScreen.Models;
using AdminScreen.ViewModels;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using XNSC.DD.EX;
using static Bumptech.Glide.Load.Engine.Engine;

namespace AdminScreen.Views;

public partial class LockProvisionPage : ContentPage
{
    public ObservableCollection<LockInfoData> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

    public ObservableCollection<LockInfoData> lockDataModel = new ObservableCollection<LockInfoData>();

    public ObservableCollection<LockProvision> LockData { get { return lockData; } set { lockData = value; OnPropertyChanged(nameof(LockData)); } }

    public ObservableCollection<LockProvision> lockData = new ObservableCollection<LockProvision>();


    public LockProvisionPage()
    {
        LockDataModel.Clear();

        InitializeComponent();
        ProvisionViewModel provisionViewModel = new ProvisionViewModel();

        this.BindingContext = provisionViewModel;

    }

    private async void Renew_Clicked(object sender, EventArgs e)
    {

        base.OnAppearing();
        if (BindingContext is ProvisionViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }

    }

    protected override async void OnAppearing()
    {

        base.OnAppearing();
        if (notesCollection.SelectedItems.Count > 0)
            notesCollection.SelectedItems.Clear();
        if (BindingContext is ProvisionViewModel viewModel)
        {
            await viewModel.ExecuteMyCommand();
        }

    }
    // 클릭 이벤트 핸들러
    public async void HandleSelectionButton_Clicked(object sender, EventArgs e)
    {

        var selectedItems = notesCollection.SelectedItems;
        var dataService = ImateHelper.GetSingleTone();

        if (selectedItems.Count == 0)
        {
            await Application.Current.MainPage.DisplayAlert("통보", "지급하실 자물쇠를 선택해주세요.", "OK");

            return;
        }
        else
        {
                LockData.Clear();
            foreach (LockInfoData item in selectedItems)
            {
                //자물쇠 이름
                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.AND,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {new DIMWhereFieldCondition{ fieldName = "LSN" , value = item.LSN, condition = DIMWhereCondition.Equal}}
                };
                var lkMstData = await dataService.Adapter.SelectModelDataAsync<LkmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.LkmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                LockData.Add(new LockProvision(lkMstData[0].LSN, lkMstData[0].LKNM));
            }
        }
        await Application.Current.MainPage.Navigation.PushAsync(new LockProvisionDetailPage(LockData));

        //await Application.Current.MainPage.Navigation.PushAsync(new HomePage(LoginInfo));
    }
}