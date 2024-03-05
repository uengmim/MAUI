using System;
using ObjCRuntime;
using Foundation;

namespace SmartLock.TT.iOS
{

#region API 바인딩 설명

    // The first step to creating a binding is to add your native library ("libNativeLibrary.a")
    // to the project by right-clicking (or Control-clicking) the folder containing this source
    // file and clicking "Add files..." and then simply select the native library (or libraries)
    // that you want to bind.
    //
    // When you do that, you'll notice that MonoDevelop generates a code-behind file for each
    // native library which will contain a [LinkWith] attribute. VisualStudio auto-detects the
    // architectures that the native library supports and fills in that information for you,
    // however, it cannot auto-detect any Frameworks or other system libraries that the
    // native library may depend on, so you'll need to fill in that information yourself.
    //
    // Once you've done that, you're ready to move on to binding the API...
    //
    //
    // Here is where you'd define your API definition for the native Objective-C library.
    //
    // For example, to bind the following Objective-C class:
    //
    //     @interface Widget : NSObject {
    //     }
    //
    // The C# binding would look like this:
    //
    //     [BaseType (typeof (NSObject))]
    //     interface Widget {
    //     }
    //
    // To bind Objective-C properties, such as:
    //
    //     @property (nonatomic, readwrite, assign) CGPoint center;
    //
    // You would add a property definition in the C# interface like so:
    //
    //     [Export ("center")]
    //     CGPoint Center { get; set; }
    //
    // To bind an Objective-C method, such as:
    //
    //     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
    //
    // You would add a method definition to the C# interface like so:
    //
    //     [Export ("doSomething:atIndex:")]
    //     void DoSomething (NSObject object, int index);
    //
    // Objective-C "constructors" such as:
    //
    //     -(id)initWithElmo:(ElmoMuppet *)elmo;
    //
    // Can be bound as:
    //
    //     [Export ("initWithElmo:")]
    //     IntPtr Constructor (ElmoMuppet elmo);
    //
    // For more information, see https://aka.ms/ios-binding
    //

#endregion

#if !NET
    using NativeHandle = System.IntPtr;
#endif

    //
    // TTBlocks.h
    //
    //typedef void(^TTBluetoothStateBlock)(TTBluetoothState state);
    public delegate void TTBluetoothStateBlock(TTBluetoothState state);

    //typedef void(^TTScanBlock)(TTScanModel *scanModel);
    public delegate void TTScanBlock(TTScanModel scanModel);

    //typedef void (^TTInitLockSucceedBlock)(NSString* lockData);
    public delegate void TTInitLockSucceedBlock(string lockData);

    //typedef void (^TTFailedBlock)(TTError TTError, NSString* errorMsg);
    public delegate void TTFailedBlock(TTError TTError, string errorMsg);

    //typedef void (^TTSucceedBlock)(void);
    public delegate void TTSucceedBlock();

    //typedef void(^TTControlLockSucceedBlock)(long long lockTime, NSInteger electricQuantity, long long uniqueId);
    public delegate void TTControlLockSucceedBlock(long lockTime, int electricQuantity, long uniqueId);

    //
    //TTScanModel.h
    //
    //@interface TTScanModel : NSObject
    /// <summary>
    /// TTLock Device Scan Model
    /// </summary>
    [BaseType(typeof(NSObject))]
    public interface TTScanModel
    {
        //@property (nonatomic, strong) NSString *lockName;
        [Export("lockName", ArgumentSemantic.Strong)]
        public string LockName { get; set; }

        //@property (nonatomic, strong) NSString *lockMac;
        [Export("lockMac", ArgumentSemantic.Strong)]
        public string LockMac { get; set; }

        //@property (nonatomic, assign) BOOL isInited;
        [Export("isInited", ArgumentSemantic.Assign)]
        public bool IsInited { get; set; }

        //@property (nonatomic, assign) BOOL isAllowUnlock;
        [Export("isAllowUnlock", ArgumentSemantic.Assign)]
        public bool IsAllowUnlock { get; set; }

        //@property (nonatomic, assign) BOOL isDfuMode;
        [Export("isDfuMode", ArgumentSemantic.Assign)]
        public bool IsDfuMode { get; set; }

        //@property (nonatomic, assign) NSInteger electricQuantity;
        [Export("electricQuantity", ArgumentSemantic.Assign)]
        public int ElectricQuantity { get; set; }

        //@property (nonatomic, strong) NSString * lockVersion;
        [Export("lockVersion", ArgumentSemantic.Strong)]
        public string LockVersion { get; set; }

        //@property (nonatomic, assign) TTLockSwitchState lockSwitchState;
        [Export("lockSwitchState", ArgumentSemantic.Assign)]
        public TTLockSwitchState LockSwitchState { get; set; }

        //@property (nonatomic, assign) NSInteger RSSI;
        [Export("RSSI", ArgumentSemantic.Assign)]
        public int RSSI { get; set; }

        //@property (nonatomic, assign) NSInteger oneMeterRSSI;
        [Export("oneMeterRSSI", ArgumentSemantic.Assign)]
        public int OneMeterRSSI { get; set; }

        //@property (nonatomic, strong) NSDate *date;
        [Export("date", ArgumentSemantic.Strong)]
        public NSDate Date { get; set; }

        //- (instancetype)initWithInfoDic:(NSDictionary *)infoDic;
        [Export("initWithInfoDic:")]
        public NativeHandle Constructor(NSDictionary infoDic);

    }

    // @interface TTLock : NSObjects
    //
    /// <summary>
    /// TTLock API
    /// </summary>
    [BaseType(typeof(NSObject))]
    public interface TTLock
    {
        //@property(class, nonatomic, assign, getter=isPrintLog) BOOL printLog;
        //
        /// <summary>
        /// Print sdk log
        /// </summary>
        [Static, Export("printLog")]
        bool PrintLog { [Bind("isPrintLog")] get; set; }

        //@property (class, nonatomic, assign, readonly) TTBluetoothState bluetoothState;
        //
        /// <summary>
        /// Current Bluetooth state
        /// </summary>
        [Static, Export("bluetoothState")]
        bool BluetoothState { get; set; }

        //+ (void) setupBluetooth:(TTBluetoothStateBlock) bluetoothStateObserver;
        //
        /// <summary>
        /// Setup Bluetooth
        /// </summary>
        /// <param name="bluetoothStateObserver">A block invoked when the bluetooth setup finished</param>
        [Static, Export("setupBluetooth:")]
        public void SetupBluetooth([BlockCallback] TTBluetoothStateBlock bluetoothStateObserver);

        //+ (void) startScan:(TTScanBlock) scanBlock;
        //
        /// <summary>
        /// Start Bluetooth  scanning
        /// </summary>
        /// <param name="scanBlock">A block invoked when the bluetooth is scanning</param>
        [Static, Export("startScan:")]
        public void StartScan([BlockCallback] TTScanBlock scanBlock);

        //+ (void)stopScan;
        //
        /// <summary>
        /// Stop Bluetooth scanning
        /// </summary>
        [Static, Export("stopScan")]
        public void StopScan();

        //+ (void) initLockWithDict:(NSDictionary*) dict success:(TTInitLockSucceedBlock) success failure:(TTFailedBlock) failure;
        //
        /// <summary>
        /// Initialize the lock
        /// </summary>
        /// <param name="dict">@{@"lockMac": xxx, @"lockName": xxx, @"lockVersion": xxx}</param>
        /// <param name="success">A block invoked when the lock is initialize</param>
        /// <param name="failure">A block invoked when the operation fails</param>
        [Static, Export("initLockWithDict:success:failure:")]
        public void InitLockWithDict(NSDictionary dict, [BlockCallback] TTInitLockSucceedBlock success, [BlockCallback] TTFailedBlock failure);

        //+ (void)controlLockWithControlAction:(TTControlAction)controlAction lockData:(NSString*) lockData success:(TTControlLockSucceedBlock) success failure:(TTFailedBlock) failure;
        //
        /// <summary>
        /// Lock 제어 명령을 전송 한다.
        /// </summary>
        /// <param name="controlAction">controlAction The controlAction</param>
        /// <param name="lockData">lockData The lock data string used to operate lock</param>
        /// <param name="success">success A block invoked when the lock is unlock or lock</param>
        /// <param name="failure">failure A block invoked when the operation fails</param>
        [Static, Export("controlLockWithControlAction:lockData:success:failure:")]
        public void ControlLockWithControlAction(TTControlAction controlAction, string lockData, [BlockCallback] TTControlLockSucceedBlock success, [BlockCallback] TTFailedBlock failure);

        //+ (void)resetLockWithLockData:(NSString*)lockData success:(TTSucceedBlock) success failure:(TTFailedBlock) failure;
        //
        /// <summary>
        /// Lock을 Reset 한다.
        /// </summary>
        /// <param name="lockData">lockData The lock data string used to operate lock</param>
        /// <param name="success">success A block invoked when the lock is reseted</param>
        /// <param name="failure">failure A block invoked when the operation fails</param>
        [Static, Export("resetLockWithLockData:success:failure:")]
        public void ResetLockWithLockData(string lockData, [BlockCallback] TTSucceedBlock success, [BlockCallback] TTFailedBlock failure);
    }

}


