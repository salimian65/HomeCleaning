using System;
using System.Collections.Generic;
using HomeCleaning.Domain;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace HomeCleaning.Persistance.Externals.Idp
{
    public class RegisteringUserDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        [SerializeAs(Name = "username")]
        public string Username { get; set; }

        [JsonProperty("firstName")]
        [SerializeAs(Name = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        [SerializeAs(Name = "lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        [SerializeAs(Name = "email")]
        public string Email { get; set; }

        [JsonProperty("emailVerified")]
        [SerializeAs(Name = "emailVerified")]
        public bool EmailVerified { get; set; }

        [JsonProperty("attributes")]
        [SerializeAs(Name = "attributes")]
        public AttributesDto Attributes { get; set; }

        [JsonProperty("credentials")]
        [SerializeAs(Name = "credentials")]
        public CredentialDto[] Credentials { get; set; }
    }

    public class AttributesDto
    {
        private AttributesDto(string[] organizationName, string[] organizationId)
        {
            OrganizationName = organizationName;
            OrganizationId = organizationId;
        }

        public AttributesDto(string organizationName, string organizationId)
            : this(new[] { organizationName }, new[] { organizationId })
        {
        }

        [JsonProperty("organizationName")]
        [SerializeAs(Name = "organizationName")]
        public string[] OrganizationName { get; private set; }

        [JsonProperty("organizationId")]
        [SerializeAs(Name = "organizationId")]
        public string[] OrganizationId { get; private set; }
    }

    public class CredentialDto
    {
        public CredentialDto(string value)
        {
            Value = value;
        }

        [JsonProperty("temporary")]
        [SerializeAs(Name = "temporary")]
        public bool Temporary => true;


        [JsonProperty("type")]
        [SerializeAs(Name = "type")]
        public string Type => "password";

        [JsonProperty("value")]
        [SerializeAs(Name = "value")]
        public string Value { get;  set; }
    }

    public class TokenDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("refresh_expires_in")]
        public long RefreshExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("not-before-policy")]
        public long NotBeforePolicy { get; set; }

        [JsonProperty("session_state")]
        public Guid SessionState { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }

    public class AuthUserDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("createdTimestamp")]
        public long CreatedTimestamp { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("totp")]
        public bool Totp { get; set; }

        [JsonProperty("emailVerified")]
        public bool EmailVerified { get; set; }

        [JsonProperty("disableableCredentialTypes")]
        public List<string> DisableableCredentialTypes { get; set; }

        [JsonProperty("requiredActions")]
        public List<string> RequiredActions { get; set; }

        [JsonProperty("notBefore")]
        public long NotBefore { get; set; }

        [JsonProperty("access")]
        public AccessDto Access { get; set; }

        [JsonIgnore]
        public DateTime Time
        {
            get
            {
                try
                {
                    DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                    dtDateTime = dtDateTime.AddMilliseconds(CreatedTimestamp).ToLocalTime();
                    return dtDateTime;
                }
                catch
                {
                    return DateTime.Now;
                }
            }
        }

        //public AuthUserDto FromEntity(User user)
        //{
        //    Id = user.Id;
        //    Username = user.Username;
        //    return this;
        //}
    }

    public class AccessDto
    {
        [JsonProperty("manageGroupMembership")]
        public bool ManageGroupMembership { get;  set; }

        [JsonProperty("view")]
        public bool View { get;  set; }

        [JsonProperty("mapRoles")]
        public bool MapRoles { get;  set; }

        [JsonProperty("impersonate")]
        public bool Impersonate { get;  set; }

        [JsonProperty("manage")]
        public bool Manage { get;  set; }
    }
}