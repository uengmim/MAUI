namespace WorkerScreen.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]



public partial class NotePage : ContentPage
{
    public string ItemId
    {
        set { LoadNote(value); }
    }


    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    private void LoadNote(string fileName)
    {
        Models.Note noteModel = new Models.Note();
        noteModel.FileName = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }

    public NotePage()
	{
		InitializeComponent();
        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));

    }


    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AllNotesPage));
    }


    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
            File.WriteAllText(note.FileName, TextEditor.Text);
        if(TextEditor.Text == null || TextEditor.Text == "")
        {
            await DisplayAlert("Alert", "메모를 입력해주세요!", "OK");
            return;
        }
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

        bool answer = await DisplayAlert("Alert", "정말 삭제하시겠습니까?", "Yes", "No");
        if (answer)
        {
            if (BindingContext is Models.Note note)
            {
                // Delete the file.
                if (File.Exists(note.FileName))
                    File.Delete(note.FileName);
            }

            await Shell.Current.GoToAsync("..");
        }
    }


}