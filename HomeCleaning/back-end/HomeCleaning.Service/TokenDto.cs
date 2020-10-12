using System;
using Newtonsoft.Json;

namespace HomeCleaning.Service
{
    public class TokenDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; private set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; private set; }

        [JsonProperty("refresh_expires_in")]
        public long RefreshExpiresIn { get; private set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; private set; }

        [JsonProperty("token_type")]
        public string TokenType { get; private set; }

        [JsonProperty("not-before-policy")]
        public long NotBeforePolicy { get; private set; }

        [JsonProperty("session_state")]
        public Guid SessionState { get; private set; }

        [JsonProperty("scope")]
        public string Scope { get; private set; }
    }
}