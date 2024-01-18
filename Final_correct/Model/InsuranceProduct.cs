using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_correct.Model
{
    public class InsuranceProduct
    {
        [Key]
        public int ProductId { get; set; }
        [MinLength(1), MaxLength(50)]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }
        [ForeignKey("PriceId")]
        public int PriceId { get; set; }
        public Price ProductPrice { get; set; }
        public int AuthorizedUserId { get; set; }
        public AuthorizedUser AuthorizedUser { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }

}
