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
            var client = new RestClient("http://localhost:8080/auth/realms/homecleaning/protocol/openid-connect/token");
            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "client_id=homecleaning&grant_type=client_credentials&client_secret=bf97b1a4-5dda-441c-9d85-3de3b40d1da5&undefined=", ParameterType.RequestBody);
            var ddd = (await client.ExecuteAsync<TokenDto>(request));

            return ddd.Data;
        }

        protected async Task<T> Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient(serverUrl).UseSerializer(() => new JsonNetSerializer());
            if (authenticationScheme == AuthenticationScheme.Jwt)
            {
                client.Authenticator = new JwtAuthenticator((await GetToken()).AccessToken);
            }

            var response = await client.ExecuteTaskAsync<T>(request);
            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new Exception(message, response.ErrorException);
            }

            return response.Data;
        }

        protected async Task<IRestResponse> Execute(RestRequest request)
        {
            var client = new RestClient(serverUrl).UseSerializer(() => new JsonNetSerializer());
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

            public T Deserialize<T>(IRestResponse response)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
              

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