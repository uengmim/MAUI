using System;
using System.Collections.Generic;
using ObjCRuntime;

namespace SmartLock.TT.iOS
{
    //
    // TTAacros.h
    //
    //typedef NS_ENUM(NSInteger, TTBluetoothState)
    //
    /// <summary>
    /// Represents the current state of a Manager.
    /// </summary>
    public enum TTBluetoothState 
    {
        /// <summary>
        /// State unknown, update imminent.
        /// </summary>
        TTBluetoothStateUnknown = 0,
        /// <summary>
        /// The connection with the system service was momentarily lost, update imminent.
        /// </summary>
        TTBluetoothStateResetting,
        /// <summary>
        /// The platform doesn't support the Bluetooth Low Energy Central/Client role.
        /// </summary>
        TTBluetoothStateUnsupported,
        /// <summary>
        /// The application is not authorized to use the Bluetooth Low Energy role.
        /// </summary>
        TTBluetoothStateUnauthorized,
        /// <summary>
        /// Bluetooth is currently powered off.
        /// </summary>
        TTBluetoothStatePoweredOff,
        /// <summary>
        /// Bluetooth is currently powered on and available to use.
        /// </summary>
        TTBluetoothStatePoweredOn,
    }

    //typedef NS_ENUM(NSInteger,TTLockSwitchState)
    //
    /// <summary>
    /// Lock Switch State
    /// </summary>
    public enum TTLockSwitchState
    {
        /// <summary>
        /// Lock
        /// </summary>
        TTLockSwitchStateLock = 0,
        /// <summary>
        /// Unlock
        /// </summary>
        TTLockSwitchStateUnlock = 1,
        /// <summary>
        /// Unknown
        /// </summary>
        TTLockSwitchStateUnknown = 2,
        /// <summary>
        /// Unlock，Has Car
        /// </summary>
        TTLockSwitchStateUnlockHasCar = 3,
    }

    //
    //typedef NS_ENUM(NSInteger, TTError)
    //
    /// <summary>
    /// TTLOCK ERROR
    /// </summary>
    public enum TTError
    {
        TTErrorHadReseted = 0x00,
        TTErrorCRCError = 0x01,
        TTErrorNoPermisstion = 0x02,
        TTErrorWrongAdminCode = 0x03,
        TTErrorLackOfStorageSpace = 0x04,
        TTErrorInSettingMode = 0x05,
        TTErrorNoAdmin = 0x06,
        TTErrorNotInSettingMode = 0x07,
        TTErrorWrongDynamicCode = 0x08,
        TTErrorIsNoPower = 0x0a,
        TTErrorResetPasscode = 0x0b,
        TTErrorUpdatePasscodeIndex = 0x0c,
        TTErrorInvalidLockFlagPos = 0x0d,
        TTErrorEkeyExpired = 0x0e,
        TTErrorPasscodeLengthInvalid = 0x0f,
        TTErrorSamePasscodes = 0x10,
        TTErrorEkeyInactive = 0x11,
        TTErrorAesKey = 0x12,
        TTErrorFail = 0x13,
        TTErrorPasscodeExist = 0x14,
        TTErrorPasscodeNotExist = 0x15,
        TTErrorLackOfStorageSpaceWhenAddingPasscodes = 0x16,
        TTErrorInvalidParaLength = 0x17,
        TTErrorCardNotExist = 0x18,
        TTErrorFingerprintDuplication = 0x19,
        TTErrorFingerprintNotExist = 0x1A,
        TTErrorInvalidCommand = 0x1B,
        TTErrorInFreezeMode = 0x1C,
        TTErrorInvalidClientPara = 0x1D,
        TTErrorLockIsLocked = 0x1E,
        TTErrorRecordNotExist = 0x1F,
        TTErrorWrongSSID = 0x25,
        TTErrorWrongWifiPassword = 0x26,

        TTErrorBluetoothPoweredOff = 0x61,
        TTErrorConnectionTimeout = 0x62,
        TTErrorDisconnection = 0x63,
        TTErrorLockIsBusy = 0x64,
        TTErrorWrongLockData = 0x65,
        TTErrorInvalidParameter = 0x66,
    }

    //typedef NS_ENUM(NSInteger, TTControlAction)
    //
    /// <summary>
    /// Lock Control Action
    /// </summary>
    public enum TTControlAction
    {
        TTControlActionRemoteStop = 0x00,
        TTControlActionUnlock = 0x01,
        TTControlActionLock = 0x02,
        TTControlActionlPause = 0x04,
        TTControlActionHold = 0x08,
    }
}


