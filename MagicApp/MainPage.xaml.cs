using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void txb_API_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }



        private async void Test_ClickAsync()
        {
            string data = await callAPIAsync();
            JObject token = JObject.Parse(data);
            txb_Name.Text = (string)token.SelectToken("name");
            txb_cmc.Text = (string)token.SelectToken("cmc");
            txb_type_line.Text = (string)token.SelectToken("type_line");
            txb_oracle_text.Text = (string)token.SelectToken("oracle_text");
            txb_mana_cost.Text = (string)token.SelectToken("mana_cost");
        }

        private async System.Threading.Tasks.Task<string> callAPIAsync()
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            Uri requestUri = new Uri("https://api.scryfall.com/cards/vis/127");

            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            httpResponse = await httpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string httpResponseBody = "";
            return httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Test_ClickAsync();
        }
    }

}
