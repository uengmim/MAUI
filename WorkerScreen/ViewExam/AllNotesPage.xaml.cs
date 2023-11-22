using System.Windows.Input;

namespace WorkerScreen.Views;

public partial class AllNotesPage : ContentPage
{
    public ICommand BackButtonCommand { get; private set; }

    public AllNotesPage()
    {
        InitializeComponent();

        BindingContext = new Models.AllNotes();

        BackButtonCommand = new Command
        (
            () =>
            {
                Shell.Current.Navigation.PopAsync();
            }
        );
    }

    protected override void OnAppearing()
    {
        ((Models.AllNotes)BindingContext).LoadNotes();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NotePage));
    }
    private async void About_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AboutPage));
    }
    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var note = (Models.Note)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.FileName}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}