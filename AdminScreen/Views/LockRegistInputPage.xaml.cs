using AdminScreen.Models;
using AdminScreen.ViewModels;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Reflection.Emit;
using System.Xml.Linq;
using XNSC.DD.EX;

namespace AdminScreen.Views
{
    public partial class LockRegistInputPage : ContentPage
    {
        public LockRegistInputPage(LockInfomation lockInfo)
        {
            InitializeComponent();
            LockRegistInputViewModel lockRegistDetailViewModel = new LockRegistInputViewModel();

            lockRegistDetailViewModel.LockInfo = lockInfo;
            lockRegistDetailViewModel.Mac = lockInfo.LockMac;

            this.BindingContext = lockRegistDetailViewModel;

        }
        private string _mac = "";

        private string _lockData = "";

        public string LockData
        {
            get => _lockData;
            set
            {
                _lockData = value;
                OnPropertyChanged(nameof(LockData));
            }
        }

        public string Mac
        {
            get => _mac;
            set
            {
                _mac = value;
                OnPropertyChanged(nameof(Mac));
            }
        }
    }
}