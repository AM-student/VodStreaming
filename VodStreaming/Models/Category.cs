using System.ComponentModel.DataAnnotations;

namespace VodStreaming.Models
{
    public class Category
    {
        [Required]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}
