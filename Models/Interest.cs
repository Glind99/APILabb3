using APILabb3.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace APILabb3.NewFolder
{
    public class Interest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestId { get; set; }

        [Required]
        [StringLength(40)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(70)]
        public string Summary { get; set; }


        [ForeignKey("Persons")]
        public int? FK_PersonId { get; set; } = null;
        [JsonIgnore] // Ignorera Persons för att undvika cykler
        public Person? Persons { get; set; }
        public ICollection<Link>? Links { get; set; }
    }
}
