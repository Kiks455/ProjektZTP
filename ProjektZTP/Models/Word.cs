using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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