using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodStreaming.Models
{
    public class Video
    {
        [Required]
        public int VideoID { get; set; }
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID ")]
        public Category? Category { get; set; }

        [Required(ErrorMessage ="Please Upload the required Video")]
        [Display(Name ="VideoFile")]
        [NotMapped]
        public IFormFile VideoFile { get; set; }    
    }
}
