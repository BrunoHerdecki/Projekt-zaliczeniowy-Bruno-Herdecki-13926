using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Models
{
    public class ReservedBookDto
    {
        public int BookId { get; set; }

        public int HistoryId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsBorrowing { get; set; }

        public bool HasReturned { get; set; }

        [Display(Name = "Reserved date")]
        public DateTime ReservedDate { get; set; }


        [Display(Name = "Expected return date")]
        public DateTime ExpectedReturnDate { get; set; }
    }
}
