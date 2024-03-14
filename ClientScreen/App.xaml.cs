using XNSC.Net;

namespace ClientScreen
{
    public partial class App : Application
    {
        /// <summary>
        /// APP ID
        /// </summary>
        public const string APPID = "ClientScreen";
        public const string ServerID = "shredoc";

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

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}