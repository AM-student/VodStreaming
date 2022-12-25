using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodStreaming.Models
{
    public class Category
    {
        [Required]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}
