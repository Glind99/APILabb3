using System.ComponentModel.DataAnnotations;

namespace APILabb3.DTO
{
    public class InterestUpdateDto
    {
        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        [Required]
        [StringLength(70)]
        public string Summary { get; set; }

        public List<LinkDto> Links { get; set; }
    }
}
