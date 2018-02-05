using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using Windows.Security.Authentication.Web.Core;
using Windows.Security.Credentials;

namespace MyUWPGraphApp01
{
    public class MicrosoftGraphContext
    {
        private static string _clientId = null;
        private static string _scopes = null;
        public static GraphServiceClient GetAuthenticatedClient(string clientId, string scopes = "")
        {
            _clientId = clientId;
            _scopes = scopes;
            var graphClient = new GraphServiceClient(
                                                new DelegateAuthenticationProvider(
                                                    async (requestMessage) =>
                                                    {
                                                        var token = await GetTokenForUserAsync();
                                                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                                                    }));
            return graphClient;
        }
        public static async Task<string> GetTokenForUserAsync()
        {
            //for most Enteprise apps, we only care about AAD version of MSGraph
            string authority = "organizations";
            // string resource = "https://graph.windows.net";   // This is the Azure AD Graph not MS Graph...
            string resource = "https://graph.microsoft.com";    //Microsoft Graph
            string TokenForUser = null;

            var wap = await WebAuthenticationCoreManager.FindAccountProviderAsync("https://login.microsoft.com", authority);

            // craft the token request for the Graph api
            //What is the correct scope?
            WebTokenRequest wtr = new WebTokenRequest(wap, _scopes, _clientId);
            wtr.Properties.Add("resource", resource);

            WebTokenRequestResult wtrr = await WebAuthenticationCoreManager.RequestTokenAsync(wtr);
            if (wtrr.ResponseStatus == WebTokenRequestStatus.Success)
            {
                TokenForUser = wtrr.ResponseData[0].Token;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(wtrr.ResponseError);
            }
            return TokenForUser;
        }
    }
}
