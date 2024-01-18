using System.ComponentModel.DataAnnotations;

namespace Final_correct.Model
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }
        public decimal? ProductPrice { get; set; }
        public InsuranceProduct InsuranceProduct { get; set; }

    }
}
