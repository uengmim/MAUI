using AdminScreen.ViewModel;
using Mapsui.UI.Maui;
using NetTopologySuite.GeometriesGraph;

namespace AdminScreen.Views
{
    public partial class LockChoicePage : ContentPage
    {

        public LockChoicePage()
        {
            InitializeComponent();

            LockChoiceViewModel lockChoiceViewModel = new LockChoiceViewModel();

            this.BindingContext = lockChoiceViewModel;
        }
    }
}