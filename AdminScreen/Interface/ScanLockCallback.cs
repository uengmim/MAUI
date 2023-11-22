using AdminScreen.Models;
using Com.Ttlock.BL.Sdk.Api;
using Com.Ttlock.BL.Sdk.Callback;
using Com.Ttlock.BL.Sdk.Entity;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminScreen.Interface
{
    internal class ScanLockCallback : Java.Lang.Object, IScanLockCallback 
    {
        public ObservableCollection<LockInfom> LockDataModel { get { return lockDataModel; } set { lockDataModel = value; } }

        public ObservableCollection<LockInfom> lockDataModel = new ObservableCollection<LockInfom>();
        IInitLockCallback mInitLockCallback;
        public nint Handle => (nint)0;

        public int JniIdentityHashCode => 100;

        public JniObjectReference PeerReference => new JniObjectReference();

        public JniPeerMembers JniPeerMembers => new JniPeerMembers(nameof(ScanLockCallback),typeof(ScanLockCallback),false);

        public JniManagedPeerStates JniManagedPeerState => new JniManagedPeerStates();

        public void Dispose()
        {

        }

        public void Disposed()
        {

        }

        public void DisposeUnlessReferenced()
        {

        }

        public void Finalized()
        {

        }

        public void OnFail(LockError p0)
        {
            
        }

        public void OnScanLockSuccess(ExtendedBluetoothDevice p0)
        {
            try
            {
                var MacAddress = p0.Address.ToString();

                LockDataModel.Add(new LockInfom(MacAddress));
                mInitLockCallback = new InitLockCallback();
                TTLockClient.Default.InitLock(p0, mInitLockCallback);
            }
            catch (System.Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("통보", ex.Message, "OK");
                return;
            }
        }

        public void SetJniIdentityHashCode(int value)
        {

        }

        public void SetJniManagedPeerState(JniManagedPeerStates value)
        {

        }

        public void SetPeerReference(JniObjectReference reference)
        {
            
        }

        public void UnregisterFromRuntime()
        {

        }
    }
}
