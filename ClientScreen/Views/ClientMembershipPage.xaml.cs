

using ClientScreen.ViewModels;
using ShreDoc.ProxyModel;
using ShreDoc.Utils;
using XNSC.DD.EX;

namespace ClientScreen.Views
{
    public partial class ClientMembershipPage : ContentPage
    {
        public string ClientInfo { get; set; }

        public ClientMembershipPage()
        {
            InitializeComponent();

            MembershipModel membershipModel = new MembershipModel();
            cliNumber.TextChanged += async(sender, args) =>
            {
                var dataService = ImateHelper.GetSingleTone();

                var whereLKCondition = new DIMGroupFieldCondtion()
                {
                    condition = DIMGroupCondtion.OR,
                    joinCondtion = DIMGroupCondtion.AND,
                    whereFieldConditions = new DIMWhereFieldCondition[]
                    {
                new DIMWhereFieldCondition{ fieldName = "WKPL" , value = args.NewTextValue, condition = DIMWhereCondition.Equal}
                    }
                };

                var wkplmstData = await dataService.Adapter.SelectModelDataAsync<WkplmstModelList>(App.ServerID, "ShreDocDataModel", "ShreDoc.DataModel.WkplmstModelList",
                            whereLKCondition, new Dictionary<string, XNSC.DIMSortOrder>(), QueryCacheType.None);

                if (wkplmstData.Count > 0)
                {
                    cliInfo.Text = wkplmstData[0].REFDA1;
                }
            };
        }
    }
}