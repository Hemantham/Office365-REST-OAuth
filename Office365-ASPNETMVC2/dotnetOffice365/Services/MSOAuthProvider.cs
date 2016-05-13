using dotnetOffice365.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace dotnetOffice365.Services
{
    public class MSOAuthProvider
    {
        public static async Task<OAuthResponse> GetToken(string tenant = "15c5f4f1-2392-49d4-a360-70bb8f22d0cd")
        {

            HttpClient aClient = new HttpClient();

            // Uri is where we are posting to: 
            Uri theUri = new Uri(string.Format("https://login.microsoftonline.com/{0}/oauth2/token", tenant));

           
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("resource", "https://outlook.office365.com/"),
                new KeyValuePair<string, string>("client_id", "41222498-622f-4c2a-9f87-22634b651a4f"),
                new KeyValuePair<string, string>("client_secret", "HSt76WSNyMPOzhzL1iApMmLH44w7Nw0orAcsr2WPpg8=")
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(theUri.ToString(), formContent);

            // use the Http client to POST some content ( ‘theContent’ not yet defined). 
            var content = await response.Content.ReadAsStringAsync();

            var model =  JsonConvert.DeserializeObject<OAuthResponse>(content);

            return model;
        }

        public static async Task<string> GetTokenString(string tenant = "15c5f4f1-2392-49d4-a360-70bb8f22d0cd")
        {

            // use the Http client to POST some content ( ‘theContent’ not yet defined). 
            var content = await GetToken(tenant);           

            return content.access_token;
        }
    }
}