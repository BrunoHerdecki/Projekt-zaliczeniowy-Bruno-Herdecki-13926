using Projekt_zaliczeniowy_Bruno_Herdecki_13926.Entity;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Models
{
    public class BorrowBookModel
    {
        public BookDto Book { get; set; }
        public HistoryOfReservation HistoryOfReservation { get; set; }
    }
}
