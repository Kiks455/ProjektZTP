using System.ComponentModel.DataAnnotations;

namespace ProjektZTP.Models
{
    public class Word
    {
        [Key]
        public int Id { get; set; }

        public string WordEn { get; set; }
        public string WordPl { get; set; }
    }
}