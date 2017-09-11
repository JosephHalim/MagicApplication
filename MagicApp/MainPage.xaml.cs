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
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        JObject token;

        public object Server { get; private set; }

        public MainPage()
        {
            this.InitializeComponent();
             token = LoadJson();
           
        /*    string path = "AllCards-x.json";
        byte[] byteArray = Encoding.UTF8.GetBytes(path);
        //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
        MemoryStream stream = new MemoryStream(byteArray);
        StreamReader sr = new StreamReader(stream);
        string text = sr.ReadToEnd();
        //txb_Name.Text = text;
        JObject token = JObject.Parse(File.ReadAllText(text));
        //List<CardInfo> items = JsonConvert.DeserializeObject<List<CardInfo>>(token);
        // PopulateData(token);
        */
        //txb_cmc.Text = token.Count.ToString();//(string)token.SelectToken("name");
                                              // txb_oracle_text.Text = token.SelectToken("Bayou").ToString();

            // JsonSerializer serializer = new JsonSerializer();
            string cardinfo = token.SelectToken("Bayou").ToString();
            CardInfo singleCardData = JsonConvert.DeserializeObject<CardInfo>(cardinfo);
           // txb_oracle_text.Text = singleCardData.name.ToString();

            JObject singleCard = token.SelectToken("Bayou") as JObject;
            CardInfo test = singleCard.ToObject<CardInfo>();
       //     txb_oracle_text.Text = test.ToString(); 
      //  txb_oracle_text.Text = temp.Select(c => (string)c).ToString();
        //Test_ClickAsync();
        }

        private void txb_API_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }



        private static void Test_ClickAsync()
        {
            /*  // string data = await callAPIAsync();
              // JObject token = JObject.Parse(data);
              JObject token;
              token = JObject.Parse(File.ReadAllText(@"C:\Users\Joseph\Documents\Visual Studio 2017\Projects\MagicApplication\MagicApp\AllSets.json"));
              txb_Name.Text = (string)token.SelectToken("name");
              txb_cmc.Text = (string)token.SelectToken("cmc");
              txb_type_line.Text = (string)token.SelectToken("type_line");
              txb_oracle_text.Text = (string)token.SelectToken("oracle_text");
              txb_mana_cost.Text = (string)token.SelectToken("mana_cost");*/
        }

        private async System.Threading.Tasks.Task<string> callAPIAsync()
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            //Pull data from Json File
            Uri requestUri = new Uri("https://api.scryfall.com/cards/vis/127");

            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            httpResponse = await httpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string httpResponseBody = "";
            return httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

        }
        private void PopulateData(JObject data)
        {
            txb_Name.Text = (string)data.SelectToken("name");
            txb_cmc.Text = (string)data.SelectToken("cmc");
          //  txb_type_line.Text = (string)data.SelectToken("type_line");
          //  txb_oracle_text.Text = (string)data.SelectToken("oracle_text");
           // txb_mana_cost.Text = (string)data.SelectToken("mana_cost");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            clearTextboxes();
            if(txb_card_search.Text == null || txb_card_search.Text == "")
            {
                return;
            }
            // JsonSerializer serializer = new JsonSerializer();
            JToken value;
            if (token.TryGetValue(txb_card_search.Text,out value))
            {
                Test_Var.Text = "found";
                var cardinfotest = token.SelectTokens("['"+txb_card_search.Text+"']");
               
                //  if (token.SelectToken(txb_card_search.Text) == null ) { txb_card_search.Text = "No Data Found";  }
                string cardinfo = token.SelectToken("['" + txb_card_search.Text + "']").ToString();
                CardInfo singleCardData = JsonConvert.DeserializeObject<CardInfo>(cardinfo);
                // txb_oracle_text.Text = singleCardData.name.ToString();

                JObject singleCard = token.SelectToken("['" + txb_card_search.Text + "']") as JObject;
                CardInfo test = singleCard.ToObject<CardInfo>();
                txb_Name.Text = test.name;
                //Handle Land Cards, since there is no manaCost, colors, power, and toughness is null
                if (test.type.Contains("Land"))
                {
                    txb_manaCost.Text = "Land";
                    txb_colors.Text = "Land";
                    txb_power.Text = "Land";
                    txb_toughness.Text = "Land";
                }
                else
                {
                    txb_manaCost.Text = test.manaCost;
                    foreach (string singleType in test.colors)
                    {
                        txb_colors.Text = txb_colors.Text + test.colors.ToString() + ", ";
                    }
                    txb_power.Text = test.power;
                    txb_toughness.Text = test.toughness;
                }
                //Handle single value text values
                txb_cmc.Text = test.cmc;
                txb_type.Text = test.type;
                txb_text.Text = test.text;
                //Handle arrays for values
                foreach (string singleSubType in test.subtypes)
                {
                    txb_subtype.Text = txb_subtype.Text + singleSubType.ToString() + ", ";
                }
                foreach (string singlePrintings in test.printings)
                {
                    txb_printings.Text = txb_printings.Text + singlePrintings.ToString() + ", ";
                }
                foreach (Legalities singleLegality in test.legalities)
                {
                    txb_legality.Text = txb_legality.Text + singleLegality.format + ": " + singleLegality.legality + ", ";
                }
                cleanUpStrings();
            }
            else
            {
                Test_Var.Text = "Not Found";
            }
           
        }
        private void clearTextboxes()
        {
            txb_Name.Text = string.Empty;
            txb_manaCost.Text = string.Empty;
            txb_colors.Text = string.Empty;
            txb_power.Text = string.Empty;
            txb_toughness.Text = string.Empty;
            txb_cmc.Text = string.Empty;
            txb_type.Text = string.Empty;
            txb_text.Text = string.Empty;
            txb_subtype.Text = string.Empty;
            txb_printings.Text = string.Empty;
            txb_legality.Text = string.Empty;
        }
        private void cleanUpStrings()
        {
            if(txb_colors.Text != "Land") { txb_colors.Text.Substring(0, txb_colors.Text.Length - 3); }
            txb_subtype.Text.Substring(0, txb_subtype.Text.Length - 3);
            txb_printings.Text.Substring(0, txb_printings.Text.Length - 3);
            txb_legality.Text.Substring(0, txb_legality.Text.Length - 3);
        }
        public JObject LoadJson()
        {

            string path = "AllCards-x.json";
            byte[] byteArray = Encoding.UTF8.GetBytes(path);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader sr = new StreamReader(stream);
            string text = sr.ReadToEnd();
            //txb_Name.Text = text;
            JObject token = JObject.Parse(File.ReadAllText(text));

            return token;
        }

      
    }
    }
