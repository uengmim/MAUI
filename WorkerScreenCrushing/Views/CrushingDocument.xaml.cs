using WorkerScreenCrushing.Models;
using WorkerScreenCrushing.ViewModel;
using XNSC.DD.EX;

namespace WorkerScreenCrushing.Views;

public partial class CrushingDocument : ContentPage
{
    public CrushingDocument()
    {
        InitializeComponent();
        CrushingViewModel crushingViewModel = new CrushingViewModel();
        this.BindingContext = crushingViewModel;
    }

}