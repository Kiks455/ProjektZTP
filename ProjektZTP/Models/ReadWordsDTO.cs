using System.Collections.Generic;

namespace ProjektZTP.Models
{
    public class ReadWordsDTO
    {
        public IEnumerable<Word> Words { get; set; }
        public int LastPageNumber { get; set; }
    }
}