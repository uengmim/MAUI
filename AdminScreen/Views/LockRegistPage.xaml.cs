using AdminScreen.Models;
using AdminScreen.ViewModels;
using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;
using Javax.Crypto;
using Kotlin.Jvm.Internal;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using System.Reflection.Emit;
using System.Xml.Linq;
using XNSC.DD.EX;

namespace AdminScreen.Views
{
    public partial class LockRegisterPage : ContentPage
    {
        public LockRegisterPage(LockInfo lockInfo)
        {
            InitializeComponent();
            LockRegigterModel lockRegigterModel = new LockRegigterModel();

            lockRegigterModel.LockInfo = lockInfo;
            lockRegigterModel.Mac = lockInfo.LockMac;

            this.BindingContext = lockRegigterModel;

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