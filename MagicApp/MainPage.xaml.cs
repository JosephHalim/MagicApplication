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
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        JObject cardData;
        JObject setData;

        public object Server { get; private set; }

        public MainPage()
        {
           this.InitializeComponent();
           cardData = LoadJson();
           setData = LoadAllSetsJson();    
        }

        private async System.Threading.Tasks.Task<string> callAPIAsync()
        {
            //API Call, is not being used to API downtime, ended up pulling the field directly for a JSON file.
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            //Pull data from Json File
            Uri requestUri = new Uri("https://api.scryfall.com/cards/vis/127");

            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            httpResponse = await httpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string httpResponseBody = "";
            return httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

        }
        private void txb_API_SelectionChanged()
        {

        }
        private void PopulateData(JObject data)
        {
            txb_Name.Text = (string)data.SelectToken("name");
            txb_cmc.Text = (string)data.SelectToken("cmc");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            clearTextboxes();
            if(txb_card_search.Text == null || txb_card_search.Text == "")
            {
                return;
            }
            JToken value;

            //Loads all card names of the same token... Has a lot of repeated information, might be more useful to use this function when they select the version
            var setinfotest = setData.SelectTokens("$..cards[?(@.name == 'Bayou')]") ;
            if (cardData.TryGetValue(txb_card_search.Text,out value))
            {
                Test_Var.Text = "found";
                var cardinfotest = cardData.SelectToken("['"+txb_card_search.Text+"']");
                string cardinfo = cardData.SelectToken("['" + txb_card_search.Text + "']").ToString();
                //CardInfo singleCardData = JsonConvert.DeserializeObject<CardInfo>(cardinfo);
                JObject singleCard = cardData.SelectToken("['" + txb_card_search.Text + "']") as JObject;
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
                pullFirstImage(test.name);
                cleanUpStrings();
            }
            else
            {
                Test_Var.Text = "Not Found";
            }
           
        }
        private void pullFirstImage(string imagename)
        {
            JObject firstImage = setData.SelectTokens("$..cards[?(@.name == '"+imagename+"')]").First() as JObject;
            Card singleCardData = firstImage.ToObject<Card>();
         //   Card_Image.Source =  new Uri("http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=" + singleCardData.multiverseid + "&type=card");
            BitmapImage Image = new BitmapImage(new Uri("http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=" + singleCardData.multiverseid + "&type=card", UriKind.Absolute));
            Card_Image.Source = Image;
            //  Card singleCardData = JsonConvert.DeserializeObject<Card>(firstImage);
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
            JObject cardData = JObject.Parse(File.ReadAllText(text));
            return cardData;
        }
        public  JObject LoadAllSetsJson()
        {
            string path = "AllSets.json";
            byte[] byteArray = Encoding.UTF8.GetBytes(path);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader sr = new StreamReader(stream);
            string text = sr.ReadToEnd();
            //txb_Name.Text = text;
            JObject cardData = JObject.Parse(File.ReadAllText(text));
            return cardData;
        }

        private void txb_API_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
    }
