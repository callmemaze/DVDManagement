using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models
{
    public class CastMemberModel
    {
        [Key]
        public string? CastMemberModelNo { get; set; }
        public string? DVDNumber { get; set; }
        public string? ActorNumber { get; set; }

        [ForeignKey("ActorNumber")]
        public ActorModel? ActorNumberModel { get; set; }

        [ForeignKey("DVDNumber")]
        public DVDTitleModel? DVDNumberModel { get; set; }
    }
}
