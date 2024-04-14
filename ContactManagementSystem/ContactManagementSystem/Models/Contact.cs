using System.ComponentModel.DataAnnotations;

namespace ContactManagementSystem.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string PhoneNumber { get; set; }
    }
}
