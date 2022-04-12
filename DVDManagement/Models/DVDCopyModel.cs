using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models
{
    public class DVDCopyModel
    {
        [Key]
        public string? CopyNumber { get; set; }
        public string? DVDNumber { get; set; }
        public DateTime DatePurchased { get; set; }

        [ForeignKey("DVDNumber")]
        public DVDTitleModel? DVDTitleModel { get; set; }
    }
}
