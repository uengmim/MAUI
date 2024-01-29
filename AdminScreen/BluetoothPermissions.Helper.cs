namespace AdminScreen
{
    public static class BluetoothPermissionsHelper
    {
        // How to use MAUI Bluetooth LE permissions

        private static async Task<bool> CheckBluetoothStatus()
        {
            try
            {
                var requestStatus = await new BluetoothPermissions().CheckStatusAsync();
                return requestStatus == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                // logger.LogError(ex);
                return false;
            }
        }

        public static async Task<bool> RequestBluetoothAccess()
        {
            try
            {
                var requestStatus = await new BluetoothPermissions().RequestAsync();
                return requestStatus == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                // logger.LogError(ex);
                return false;
            }
        }
    }
}
