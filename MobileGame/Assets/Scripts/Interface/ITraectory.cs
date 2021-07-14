namespace MobileGame
{
    internal interface ITraectory
    {
        public Traectory[] Track { get; }
        public OnTriggerView onTriggerView { get; }
    }
}