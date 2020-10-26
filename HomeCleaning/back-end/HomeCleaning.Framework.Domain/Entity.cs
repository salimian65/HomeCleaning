using System.ComponentModel.DataAnnotations;

namespace Framework.Domain
{
    
    public class Entity
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}