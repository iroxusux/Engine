namespace Engine.Interfaces
{
    public interface ISystemObject
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public List<ISystemObject>? Children { get; set; }
        public ISystemObject? Parent { get; set; }
        public abstract void AddDevice(ref ISystemObject oDevice);
        public abstract void RemoveDevice(ISystemObject oDevice);
    }
}
