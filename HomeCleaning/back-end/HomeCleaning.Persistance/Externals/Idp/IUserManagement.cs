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
        public Guid Id { get;  set; }
            
        [JsonProperty("username")]
        [SerializeAs(Name = "username")]
        public string Username { get;  set; }

        [JsonProperty("firstName")]
        [SerializeAs(Name = "firstName")]
        public string FirstName { get;  set; }

        [JsonProperty("lastName")]
        [SerializeAs(Name = "lastName")]
        public string LastName { get;  set; }

        [JsonProperty("email")]
        [SerializeAs(Name = "email")]
        public string Email { get;  set; }

        [JsonProperty("emailVerified")]
        [SerializeAs(Name = "emailVerified")]
        public bool EmailVerified { get;  set; }

        [JsonProperty("attributes")]
        [SerializeAs(Name = "attributes")]
        public AttributesDto Attributes { get;  set; }

        [JsonProperty("credentials")]
        [SerializeAs(Name = "credentials")]
        public CredentialDto[] Credentials { get;  set; }
    }

    public class AttributesDto
    {
        private AttributesDto(string[] organizationName, string[] organizationId)
        {
            OrganizationName = organizationName;
            OrganizationId = organizationId;
        }

        public AttributesDto(string organizationName, string organizationId)
            : this(new[] {organizationName}, new[] {organizationId})
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
        public string Value { get; private set; }
    }

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

    public class AuthUserDto
    {
        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonProperty("createdTimestamp")]
        public long CreatedTimestamp { get; private set; }

        [JsonProperty("username")]
        public string Username { get; private set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; private set; }

        [JsonProperty("totp")]
        public bool Totp { get; private set; }

        [JsonProperty("emailVerified")]
        public bool EmailVerified { get; private set; }

        [JsonProperty("disableableCredentialTypes")]
        public List<string> DisableableCredentialTypes { get; private set; }

        [JsonProperty("requiredActions")]
        public List<string> RequiredActions { get; private set; }

        [JsonProperty("notBefore")]
        public long NotBefore { get; private set; }

        [JsonProperty("access")]
        public AccessDto Access { get; private set; }

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
                    return  DateTime.Now;
                }
            }
        }

        public AuthUserDto FromEntity(User user)
        {
            Id = user.Id;
            Username = user.Username;
            return this;
        }
    }

    public class AccessDto
    {
        [JsonProperty("manageGroupMembership")]
        public bool ManageGroupMembership { get; private set; }

        [JsonProperty("view")]
        public bool View { get; private set; }

        [JsonProperty("mapRoles")]
        public bool MapRoles { get; private set; }

        [JsonProperty("impersonate")]
        public bool Impersonate { get; private set; }

        [JsonProperty("manage")]
        public bool Manage { get; private set; }
    }
}