using DVDManagement.Models;

namespace DVDManagement.ViewModel
{
    internal class ProducerViewModel
    {
        public ProducerModel? producer { get; set; }
        public DVDTitleModel? dvd { get; set; }
        public CastMemberModel? castMember { get; set; }
        public StudioModel? studio { get; set; }
        public ActorModel? actor { get; set; }
    }
}