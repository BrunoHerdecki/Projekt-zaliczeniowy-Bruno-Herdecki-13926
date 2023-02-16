using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Projekt_zaliczeniowy_Bruno_Herdecki_13926.Entity
{

    [Table("Users")]
    public class Users
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surrname")]
        public string Surrname { get; set; }

        [Required]
        [StringLength(11)]
        [RegularExpression(@"^[0-9]{11}$")]
        public string Pesel { get; set; }

        public bool IsAdmin { get; set; } = false;

        public bool IsRemoved { get; set; } = false;
    }

    [Table("Books")]
    public class Books
    {
        [Key]
        [Required]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Is borrowed?")]
        public bool IsBorrowed { get; set; }
    }

    [Table("HistoryOfReservation")]
    public class HistoryOfReservation
    {
        [Key]
        [Required]
        public int HistoryId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime ReservedDate { get; set; }

        [Required]
        public DateTime ExpectedReturnDate { get; set; }

        public bool HasReturned { get; set; } = false;

        [DataMember]
        [ForeignKey("BookId")]
        public virtual Books Books { get; set; }

        [DataMember]
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        [DataMember]
        [NotMapped]
        public string Title { get; set; }

        [DataMember]
        [NotMapped]
        public string Author { get; set; }
    }
}
