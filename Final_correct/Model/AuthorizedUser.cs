using System.ComponentModel.DataAnnotations;

namespace Final_correct.Model
{
    public class AuthorizedUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public InsuranceProduct InsuranceProduct { get; set; }
    }
}
