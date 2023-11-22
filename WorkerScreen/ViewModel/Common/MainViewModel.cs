using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WorkerScreen.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel() {
            Items = new ObservableCollection<string>();
        }

        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [ICommand]
        void Add()
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            Items.Add(text);
            Text = string.Empty;
        }
    
        [ICommand]
        void Delete(string s)
        {
            if(Items.Contains(s))
            {
                Items.Remove(s);
            }
        }
    }
}
