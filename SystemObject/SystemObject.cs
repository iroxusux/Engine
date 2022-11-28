using Engine.Interfaces;
using System.Runtime.Serialization;

namespace Engine.SystemObject
{
    [DataContract(Name = "Object", Namespace = "IndiconStudio", IsReference = true)]
    public class SystemObject : IEquatable<SystemObject>, IEqualityComparer<SystemObject>, IComparable<SystemObject>, ISystemObject
    {
        private static int AssignmentID;
        protected int GetNextID() => ++AssignmentID;
        [DataMember()]
        protected string _Name;
        [DataMember()]
        protected int _ID;
        [DataMember()]
        protected ISystemObject? _Parent;
        [DataMember()]
        protected List<ISystemObject> _Children;
        public virtual string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnUpdated(EventArgs.Empty);
            }
        }
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnUpdated(EventArgs.Empty);
            }
        }
        public ISystemObject? Parent
        {
            get { return _Parent; }
            set
            {
                _Parent = value;
                OnUpdated(EventArgs.Empty);
            }
        }
        public List<ISystemObject> Children
        {
            get { return _Children; }
            set
            {
                _Children = value;
                OnUpdated(EventArgs.Empty);
            }
        }

        public event EventHandler Updated = delegate { };
        protected virtual void OnUpdated(EventArgs oArgs)
        {
            EventHandler oHandler = Updated;
            oHandler?.Invoke(this, oArgs);
        }
        public SystemObject()
        {
            _Name = string.Empty;
            _ID = GetNextID();
            _Parent = null;
            _Children = new();
        }
        static SystemObject() => AssignmentID = 0;
        public virtual void AddDevice(ref ISystemObject oDevice)
        {
            oDevice.Parent = this;
            _Children.Add(oDevice);
            OnUpdated(EventArgs.Empty);
        }
        public virtual void RemoveDevice(ISystemObject oDevice)
        {
            if(oDevice.Parent == this)
            {
                oDevice.Parent = null;
            }
            this._Children.Remove(oDevice);
            OnUpdated(EventArgs.Empty);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as SystemObject);
        }
        public bool Equals(SystemObject other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }
        public bool Equals(SystemObject oObject1, SystemObject oObject2)
        {
            if (oObject1 == null && oObject2 == null) return true;
            else if (oObject1 == null || oObject2 == null) return false;
            else if (oObject1.ID == oObject2.ID) return true;
            else return false;
        }
        public int GetHashCode(SystemObject oObject)
        {
            return oObject.ID.GetHashCode();
        }
        public override int GetHashCode()
        {
            return _ID;
        }
        public int CompareTo(SystemObject? other)
        {
            throw new NotImplementedException();
        }
        public ISystemObject[] GetAllChildren()
        {
            List<ISystemObject> oObjects = new(_Children);
            foreach(ISystemObject oObject in oObjects.ToList())
            {
                oObjects.AddRange(GetGrandChildren(oObject, oObjects));
            }
            return oObjects.ToArray();
        }
        private List<ISystemObject> GetGrandChildren(ISystemObject oParent, List<ISystemObject> oObjects)
        {
            foreach(ISystemObject oObject in oParent.Children)
            {
                oObjects.Add(oObject);
                GetGrandChildren(oObject, oObjects);
            }
            return oObjects;
        }
    }
}
