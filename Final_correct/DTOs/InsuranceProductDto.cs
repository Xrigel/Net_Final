using Final_correct.Model;
using System.ComponentModel.DataAnnotations;

namespace Final_correct.DTOs
{
    public class InsuranceProductDto
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public string PackageName { get; set; }
        public string AuthorizedUserName { get; set; }
        public string Description { get; set; }
    }
}
