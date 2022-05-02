using DVDManagement.Models;

namespace DVDManagement.ViewModel
{
    internal class ActorViewModel
    {
        public ActorModel? actor { get; set; }
        public DVDTitleModel? dvd { get; set; }
        public CastMemberModel? castMember { get; set; }
    }
}