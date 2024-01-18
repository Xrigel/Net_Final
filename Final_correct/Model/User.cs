using System.ComponentModel.DataAnnotations;

namespace Final_correct.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UniqueNumber { get; set; }
        public ICollection<InsuranceProduct> InsuranceProducts { get; set; } = new List<InsuranceProduct>();

    }
}
