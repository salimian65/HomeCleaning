using Newtonsoft.Json;

namespace HomeCleaning.ApiAndAuth.Models
{
    public class ErrorDetails
    {
        public string ReferenceCode { get; set; }
        public string Message { get; set; }
 
 
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}