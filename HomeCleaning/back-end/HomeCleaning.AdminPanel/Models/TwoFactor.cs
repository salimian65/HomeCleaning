using System.ComponentModel.DataAnnotations;

namespace HomeCleaning.AdminPanel.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
