using PostmanClone.Library;

namespace PostmanClone.UI
{
    public partial class Dashboard : Form
    {
        private readonly IApiAccess _apiAccess = new ApiAccess();
        public Dashboard()
        {
            InitializeComponent();

            httpVerbSelection.SelectedItem = "GET";
        }

        private async void callApi_Click(object sender, EventArgs e)
        {
            if (_apiAccess.IsValidUrl(apiText.Text))
            {
                try
                {
                    systemStatus.Text = "Calling API ...";
                    
                    resultsText.Text = "";

                    resultsText.Text = await _apiAccess.CallApi(apiText.Text);

                    callData.SelectedTab = resultTab;

                    systemStatus.Text = "Ready";
                }
                catch (Exception ex)
                {
                    resultsText.Text = $"Error: ${ex.Message}";
                    systemStatus.Text = "Error";
                }
            }
            else
            {
                systemStatus.Text = "Invalid URL";
                return;
            }
        }
    }
}
