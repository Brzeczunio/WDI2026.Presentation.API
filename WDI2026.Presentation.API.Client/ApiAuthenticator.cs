using RestSharp.Authenticators;
using System.Security.Authentication;
using WDI2026.Presentation.API.Client.Models;

namespace WDI2026.Presentation.API.Client
{
    internal class ApiAuthenticator : AuthenticatorBase
    {
        readonly Uri _baseUrl;
        readonly string _username, _password;

        public ApiAuthenticator(Uri baseUrl, string username, string password) : base(string.Empty)
        {
            _baseUrl = baseUrl;
            _username = username;
            _password = password;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            Token = string.IsNullOrEmpty(accessToken) ? await GetToken() : accessToken;
            return new HeaderParameter(KnownHeaders.Authorization, Token);
        }

        async Task<string> GetToken()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("api/v1/account/login");
            request.AddJsonBody(new
            {
                userName = _username,
                password = _password
            });

            var response = await client.ExecutePostAsync<LoginResponse>(request);
            if (response.Data == null)
            {
                throw new AuthenticationException("User wasn't logged");
            }
            return $"bearer {response.Data.AccessToken}";
        }
    }
}
