using DVDManagement.Models;

namespace DVDManagement.ViewModel
{
    public class ActorViewModel
    {
        public ActorModel? actor { get; set; }
        public DVDTitleModel? dvd { get; set; }
        public CastMemberModel? castMember { get; set; }
    }
}