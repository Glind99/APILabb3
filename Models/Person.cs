using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using APILabb3.NewFolder;

namespace APILabb3.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; } = 0;

        [Required]
        [StringLength(maximumLength: 30)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [NotMapped]
        [DisplayName("Person")]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [StringLength(maximumLength: 25)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<Interest> interests { get; set; }
    }
}
