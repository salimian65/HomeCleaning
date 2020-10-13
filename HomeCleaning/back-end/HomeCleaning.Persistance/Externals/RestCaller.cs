using System;
using System.Net;
using System.Threading.Tasks;
using HomeCleaning.Persistance.Externals.Idp;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization;

namespace Infrastructures.DataAccess.Externals.Idp.KeyCloak
{
    public class RestCaller
    {
        protected readonly string serverUrl;
        protected AuthenticationScheme authenticationScheme;

        protected RestCaller(string serverUrl, AuthenticationScheme authenticationScheme = AuthenticationScheme.Jwt)
        {
            this.serverUrl = serverUrl;
            this.authenticationScheme = authenticationScheme;
        }

        private static async Task<TokenDto> GetToken()
        {
            var client = new RestClient("http://development.avan:8080/auth/realms/moneymaker/protocol/openid-connect/token");
            var request = new RestRequest( Method.POST);
            
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "client_id=admin-cli&grant_type=client_credentials&client_secret=24e7c645-1636-4310-80e6-999d3da40126&undefined=", ParameterType.RequestBody);
            return (await client.ExecuteAsync<TokenDto>(request)).Data;
        }

        protected async Task<T> Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(serverUrl)
                .UseSerializer(() => new JsonNetSerializer());
            if (authenticationScheme == AuthenticationScheme.Jwt)
                client.Authenticator = new JwtAuthenticator((await GetToken()).AccessToken);
            var response = await client.ExecuteAsync<T>(request);
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new Exception(message, response.ErrorException);
            }

            return response.Data;
        }

        protected async Task<IRestResponse> Execute(RestRequest request)
        {
            var client = new RestClient(serverUrl)
                .UseSerializer(() => new JsonNetSerializer());
            client.Authenticator = new JwtAuthenticator((await GetToken()).AccessToken);
            IRestResponse response = await client.ExecuteTaskAsync(request);
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new Exception(message, response.ErrorException);
            }

            return response;
        }

        private class JsonNetSerializer : IRestSerializer
        {
            public string Serialize(object obj)
            {
                var v = JsonConvert.SerializeObject(obj);
                return v;
            }

            public string Serialize(Parameter parameter) =>
                JsonConvert.SerializeObject(parameter.Value);

            public T Deserialize<T>(IRestResponse response) =>
                JsonConvert.DeserializeObject<T>(response.Content);

            public string[] SupportedContentTypes { get; } =
            {
                "application/json", "text/json", "text/x-json", "text/javascript", "*+json"
            };

            public string ContentType { get; set; } = "application/json";

            public DataFormat DataFormat { get; } = DataFormat.Json;
        }
    }

    public enum AuthenticationScheme
    {
        None = 0,
        Jwt
    }
}

namespace RestSharp
{
    public static class RestClientExtensions
    {
        public static Task<IRestResponse> ExecuteTaskAsync(this RestClient @this, RestRequest request)
        {
            if (@this == null)
                throw new NullReferenceException();

            var tcs = new TaskCompletionSource<IRestResponse>();

            @this.ExecuteAsync(request, (response) =>
            {
                if (response.ErrorException != null)
                    tcs.TrySetException(response.ErrorException);
                else
                    tcs.TrySetResult(response);
            });

            return tcs.Task;
        }
    }
}