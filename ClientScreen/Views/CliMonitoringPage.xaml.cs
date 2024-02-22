using ClientScreen.Model;
using ClientScreen.ViewModels;
using NetTopologySuite.GeometriesGraph;
using System.Collections.ObjectModel;

namespace ClientScreen.Views
{

    public partial class CliMonitoringPage : ContentPage
    {
        public CliMonitoringPage(string DEPTID, string WKPL,string EMPNO, string REFDA2)
        {
            InitializeComponent();
            CliMonitoringViewModel cliMonitoringViewModel = new CliMonitoringViewModel();

            cliMonitoringViewModel.DEPTID = DEPTID;
            cliMonitoringViewModel.WKPL = WKPL;
            cliMonitoringViewModel.EMPNO = EMPNO;
            cliMonitoringViewModel.REFDA2 = REFDA2;
        }
    }

}   