using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models
{
    public class DVDTitleModel
    {
        [Key]
        public string? DVDNumber { get; set; }
        public string? CategoryNumber { get; set; }
        public string? StudioNumber { get; set; }
        public string? ProducerNumber { get; set; }
        public string? DVDTitle { get; set; }
        public string? DVDReleased { get; set; }
        public string? StandardCharge { get; set; }
        public string? PenaltyCharge { get; set; }

        [ForeignKey("CategoryNumber")]
        public DVDCategoryModel? DVDCategoryModel { get; set; }

        [ForeignKey("StudioNumber")]
        public StudioModel? StudioModel { get; set; }

        [ForeignKey("ProducerNumber")]
        public ProducerModel? ProducerModel { get; set; }



    }
}
