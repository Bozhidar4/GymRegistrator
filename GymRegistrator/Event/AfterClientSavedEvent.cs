using Prism.Events;

namespace GymRegistrator.UI.Event
{
    public class AfterClientSavedEvent : PubSubEvent<AfterClientSavedEventArgs>
    {
    }

    public class AfterClientSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}
