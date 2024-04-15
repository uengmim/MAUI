using Camera.MAUI;
using WorkerScreenCrushing.Models;
using WorkerScreenCrushing.ViewModel;

namespace WorkerScreenCrushing.Views;

public partial class CrushingInputData : ContentPage
{


    public CrushingInputData(string Name, string BoxName, string Location, string LockData, DateTime PickupDate, DateTime LockDate, string PhotoResult, string ConfNo)
    {

        InitializeComponent();
        InputDataViewModel inputDataViewModel = new InputDataViewModel();
        inputDataViewModel.WorkerName = Name;
        inputDataViewModel.BoxName = BoxName;
        inputDataViewModel.Location = Location;
        inputDataViewModel.LockData = LockData;
        inputDataViewModel.PickupDate = PickupDate;
        inputDataViewModel.LockDate = LockDate;
        inputDataViewModel.PhotoResult = PhotoResult;
        inputDataViewModel.ConfNo = ConfNo;
        this.BindingContext = inputDataViewModel;


    }


}


