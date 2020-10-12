using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp.Serializers;

namespace HomeCleaning.Service
{
    public class RegisteringUserDto
    {
        private RegisteringUserDto(Guid id, string username, string firstName, string lastName, string email, bool emailVerified, AttributesDto attributes, CredentialDto[] credentials)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            EmailVerified = emailVerified;
            Attributes = attributes;
            Credentials = credentials;
            Id = id;
        }

        public RegisteringUserDto(Guid id, string username, string firstName, string lastName, string email, string organizationName, string organizationId, string password)
            : this(id, username, firstName, lastName, email, false, new AttributesDto(organizationName, organizationId), new[] {new CredentialDto(password)})
        {
        }

        [JsonProperty("id")]
        public Guid Id { get; private set; }
            
        [JsonProperty("username")]
        [SerializeAs(Name = "username")]
        public string Username { get; private set; }

        [JsonProperty("firstName")]
        [SerializeAs(Name = "firstName")]
        public string FirstName { get; private set; }

        [JsonProperty("lastName")]
        [SerializeAs(Name = "lastName")]
        public string LastName { get; private set; }

        [JsonProperty("email")]
        [SerializeAs(Name = "email")]
        public string Email { get; private set; }

        [JsonProperty("emailVerified")]
        [SerializeAs(Name = "emailVerified")]
        public bool EmailVerified { get; private set; }

        [JsonProperty("attributes")]
        [SerializeAs(Name = "attributes")]
        public AttributesDto Attributes { get; private set; }

        [JsonProperty("credentials")]
        [SerializeAs(Name = "credentials")]
        public CredentialDto[] Credentials { get; private set; }
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

    public class UserDto
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
    }
}