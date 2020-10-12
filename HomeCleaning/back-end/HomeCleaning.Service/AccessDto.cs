using Newtonsoft.Json;

namespace HomeCleaning.Service
{
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