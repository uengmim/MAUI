using WorkerScreenCrushing.Views;
using WorkerScreenCrushing.ViewModel;
using XNSC.Net;
using XNSC.Net.NOKE;
using DeviceId;

namespace WorkerScreenCrushing;

public partial class App : Application
{
    //-----------------------------------------------------------------

    /// <summary>
    /// APP ID
    /// </summary>
    public const string APPID = "WorkerScreenCrushing";
    public const string ServerID = "shredoc";
    /// <summary>
    /// Device 식별자
    /// </summary>
    public static string DeviceId { get; set; } = InitApp();

    /// <summary>
    /// MANDT
    /// </summary>
    public static string Mandt { get; private set; }

    /// <summary>
    /// Plant
    /// </summary>
    public static string Werks { get; private set; }

    /// <summary>
    /// 언어 키
    /// </summary>
    public static string Spras { get; private set; }

    /// <summary>
    /// 서버 설정
    /// </summary>
    public static string DbTitle { get; private set; }

    /// <summary>
    /// 인터페이스 사용자 ID
    /// </summary>
    public static string IFUserId { get; private set; }

    public static string InitApp()
    {

        string ImateDeviceId;

        try
        {
            ImateDeviceId = new DeviceIdBuilder().ToString();
        }
        catch (Exception)
        {
            ImateDeviceId = null;
        }

        Mandt = Preferences.Get("serverMandt", "100");
        Werks = Preferences.Get("serverWerks", "1000");
        Spras = Preferences.Get("serverSpras", "3");
        DbTitle = $"{Preferences.Get("serverKey", "shredoc")}?client={Mandt};pool_name=imate_pool_{Mandt}";
        IFUserId = Preferences.Get("serverIfUserId", "imate_system");

        DeviceId = ImateDeviceId;

        return ImateDeviceId;
    }

    private static bool _isNetwork;

    public static bool isNetwork
    {
        get => _isNetwork;
        set
        {
        }
    }
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    static ImateAdapter adapter;
    public static ImateAdapter Adapter
    {
        get
        {
            if (adapter == null)
                adapter = new ImateAdapter(Preferences.Get("API_URL", "https://183.111.166.141/"), "", "imate_system", "a#12!08@", true);
            return adapter;
        }
        set { adapter = value; }

    }
}
