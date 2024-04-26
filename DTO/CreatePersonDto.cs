using System.ComponentModel.DataAnnotations;

namespace APILabb3.DTO
{
    public class CreatePersonDto
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(25)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

}
