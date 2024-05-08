using System.ComponentModel.DataAnnotations;

namespace ContactManagementSystem.Entities.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public int Zip { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
        
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string PhoneNumber { get; set; }

        public string Notes { get; set; }

    }
}
