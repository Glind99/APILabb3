using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APILabb3.NewFolder;
using System.Text.Json.Serialization;

namespace APILabb3.Models
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; } = 0;

        [Required]
        [StringLength(100)]
        [Url]
        public string Url { get; set; }

        [ForeignKey("Interest")]
        public int? FK_InterestsId { get; set; } = null;
        [JsonIgnore] // Ignorera Interests för att undvika cykler
        public virtual Interest? Interests { get; set; }
    }
}
