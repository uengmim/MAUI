using AdminScreen.Interface;
using AdminScreen.Model;
using AdminScreen.Models;
using Android.Runtime;
using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;
using Java.Interop;
using Java.Lang;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AdminScreen.ViewModels
{
    public class RegisterSearchModel : INotifyPropertyChanged
    {

        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; OnPropertyChanged(nameof(LockDataModel)); } }

        public ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();

        public LockInfom LockInfom
        {
            get { return lockinfo; }
            set
            {
                lockinfo = value;
                OnPropertyChanged(nameof(LockInfom));
            }
        }       

        private LockInfom lockinfo;
        public void onInitLock()
        {
        }

        
        public void startScanLock()
        {
            IScanLockCallback callback = new ScanLockCallback();
            TTLockClient.Default.StartScanLock(callback);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}