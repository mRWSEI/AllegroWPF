using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace AllegroClass
{
    //http://luisquintanilla.me/2017/12/25/client-credentials-authentication-csharp/
    //https://gist.github.com/lqdev/5e82a5c856fcf0818e0b5e002deb0c28
    /// <summary>
    /// Class for requesting OAuth Token and performing Search Request
    /// </summary>
    public partial class AllegroRest
    {
        public AllegroRest(string ClientId, string ClientSecret)
        {
            clientId = ClientId;
            clientSecret = ClientSecret;
        }

        private string clientSecret; // button
        private string clientId; // button
        public string accessToken { get; set; }

        private decimal priceFrom;
        public decimal PriceFrom => priceFrom;

        private decimal priceTo;
        public decimal PriceTo => priceFrom;

        private string city;
        public string City => city;


        public void SetValues(decimal PriFrom, decimal PriTo, string Cty)
        {
            priceFrom = PriFrom;
            priceTo = PriTo;
            city = Cty;
        }

        class AccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public long expires_in { get; set; }
        }

        public async Task<string> GetTokenJ()
        {
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);

            // ze względu na zmiany w API powodujące błąd "The request was aborted: Could not create SSL/TLS secure channel."
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;

            using(var client = new HttpClient())
            {
                //Define Headers
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                var request = await client.PostAsync("https://allegro.pl/auth/oauth/token", requestBody).ConfigureAwait(false);
                var response = await request.Content.ReadAsStringAsync().ConfigureAwait(false); 
                var x = JsonConvert.DeserializeObject<AccessToken>(response);
                x = x as AccessToken;
                accessToken = x.access_token;
                return JsonConvert.DeserializeObject<AccessToken>(response).ToString();
            }
        }
   
        /*
        public string GetTokenK()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;

            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            var client = new RestClient("https://allegro.pl/auth/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.RequestFormat = DataFormat.Json;
            //request.AddHeader("Authorization", $"basic client_id:{clientId}, client_secret:{clientSecret}");
            //request.AddHeader("Authorization", $"basic {clientId}:{clientSecret}");
            //request.AddParameter("application/json", "{\n\"grant_type\":\"client_credentials\"\n}", ParameterType.RequestBody);
            request.AddHeader("data", $"grant_type=client_credentials&client_id={clientId}&client_secret={clientSecret}");
            IRestResponse response = client.Execute(request);

            AccessToken returnData = deserial.Deserialize<AccessToken>(response);
            if(returnData.access_token != null)
            {
                var j = returnData.access_token; //This correctly gets the Access Token. You should return this to a class variable so that all the  other functions can access it easily and you're not constantly passing along the variable through them.
                return j;
            }

            return "";
        }
        */
        /*
        public async Task<string> GetAccessToken()
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://allegro.pl/auth/oauth/token");

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build up the data to POST.
                Dictionary<string, string> postData = new Dictionary<string, string>()
                {
                    { "grant_type", "client_credentials"},
                    { "client_id", "***"},
                    { "client_secret", "***"}
                };

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                // Post to the Server and parse the response.
                HttpResponseMessage response = await client.PostAsync("Token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);

                // return the Access Token.
                return ((dynamic)responseData).access_token;
            }
        }
        */

        //private const string url = "https://api.allegro.pl/offers/listing?phrase=galaxy+s7+gwarancja+clear";
        /// <summary>
        /// Method used for Searching item returns deserialized object
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns></returns>
        public AllegroClass.Rootobject requestSearchItem(string PItemName, string PPriceFrom, string PPriceTo)
        {
            string priceFrom = "";
            if (!String.IsNullOrEmpty(PPriceFrom))
                priceFrom = $"&price.from={PPriceFrom}";

            string priceTo = "";
            if (!String.IsNullOrEmpty(PPriceTo))
                priceTo = @"&price.to="+PPriceTo;


            //find offers listed by a particular seller whose price range is from PLN 10 to PLN 1,000 and sort them according to price(low to high): 
            //https://api.allegro.pl/offers/listing?seller.id={Seller_ID}&price.from=10&price.to=1000&sort=+price
            //find offers with phrase “czerwona sukienka”: 
            //https://api.allegro.pl/offers/listing?phrase=czerwona+sukienka


            //string url = $"https://api.allegro.pl/offers/listing?phrase={PItemName}+clear";
            string url = $"https://api.allegro.pl/offers/listing?phrase={PItemName}{priceFrom}{priceTo}+clear";

            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/vnd.allegro.public.v1+json");
            request.AddHeader("content-type", "application/vnd.allegro.public.v1+json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            
            var response = client.Execute(request).Content; //the Content (body) of the response
            return JsonConvert.DeserializeObject<Rootobject>(response); 
        }
    }
}
