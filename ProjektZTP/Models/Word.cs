using System.ComponentModel.DataAnnotations;

namespace ProjektZTP.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string WordEn { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string WordPl { get; set; }
    }
}