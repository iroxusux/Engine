using Engine.Enums;
using Engine.IO;
using Engine.Structs;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using JsonSerializer = Engine.IO.JsonSerializer;

namespace Engine.Interfaces
{
    public interface IFileControl<T> where T : new()
    {
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Path { get { return $"{Directory}\\{FileName}{FileExtension}"; } }
        public Serializers Serializer { get; set; }
        public virtual void SaveToFile()
        {
            if (!PathIsValid()) return;
            if((Serializer & Serializers.Binary) == Serializers.Binary)
            {
                try
                {
                    BinarySerialization.WriteToBinaryFile<T>(Path, (T)this, false);
                }
                catch (SerializationException oE)
                {
                    NotifySaveFailure(oE.Message);
                }
                
            }
            if((Serializer & Serializers.Json) == Serializers.Json)
            {
                try
                {
                    JsonSerializer.WriteToJsonFile<T>(Path, (T)this, false);
                }
                catch (JsonSerializationException oE)
                {
                    NotifySaveFailure(oE.Message);
                }
                
            }
            if((Serializer & Serializers.Xml) == Serializers.Xml)
            {
                try
                {
                    XmlSerializer.WriteToXmlFile<T>(Path, (T)this);
                }
                catch (SerializationException oE)
                {
                    NotifySaveFailure(oE.Message);
                }
            }
        }
        public virtual T? LoadFromFile(string sPath)
        {
            if (String.IsNullOrWhiteSpace(sPath)) return default;
            if ((Serializer & Serializers.Binary) == Serializers.Binary)
            {
                try
                {
                    return BinarySerialization.ReadFromBinaryFile<T>(sPath);
                }
                catch (SerializationException oE)
                {
                    NotifyLoadFailure(oE.Message);
                }
                
            }
            if ((Serializer & Serializers.Json) == Serializers.Json)
            {
                try
                {
                    return JsonSerializer.ReadFromJsonFile<T>(sPath);
                }
                catch (SerializationException oE)
                {
                    NotifyLoadFailure(oE.Message);
                }
            }
            if ((Serializer & Serializers.Xml) == Serializers.Xml)
            {
                try
                {
                    return XmlSerializer.ReadFromXmlFile<T>(sPath);
                }
                catch (SerializationException oE)
                {
                    NotifyLoadFailure(oE.Message);
                }
            }
            return default;
        }
        private void NotifySaveFailure(string sMessage)
        {
            MessageBox.Show($"File Save Failed:\n{sMessage}", "File Save Failed");
        }
        private void NotifyLoadFailure(string sMessage)
        {
            MessageBox.Show($"File Load Failed:\n{sMessage}", "File Load Failed");
        }
        private bool PathIsValid()
        {
            return !(Directory == string.Empty || FileName == string.Empty || FileExtension == string.Empty);
        }
        public bool SetNewLocation(FileLocation oLocation)
        {
            if (oLocation.Name == string.Empty || oLocation.Directory == string.Empty) return false;
            FileName = oLocation.Name;
            Directory = oLocation.Directory;
            return true;
        }
    }
}
