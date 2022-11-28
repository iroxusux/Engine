using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Engine.IO
{
    public static class XmlSerializer
    {
        public static void WriteToXmlFile<T>(string sFile, T oData)
        {
            FileStream oWriter = new(sFile, FileMode.Create);
            DataContractSerializer oSerializer = new(typeof(T));
            oSerializer.WriteObject(oWriter, oData);
            oWriter.Close();
        }
        public static T? ReadFromXmlFile<T>(string sFile) where T : new()
        {
            FileStream oReader = new(sFile, FileMode.Open);
            XmlDictionaryReader oDictReader = XmlDictionaryReader.CreateTextReader(oReader, new XmlDictionaryReaderQuotas() { MaxDepth = 16384});
            DataContractSerializer oSerializer = new(typeof(T));
            T? oObject = (T)oSerializer.ReadObject(oDictReader, true); 
            oDictReader.Close();
            oReader.Close();
            return oObject;
        }
        private static void SerializerUnkownNode(object? oSender, XmlNodeEventArgs oArgs)
        {
            Debug.WriteLine(oArgs);
        }
        private static void SerializerUnkownAttribute(object? oSender, XmlAttributeEventArgs oArgs)
        {
            Debug.WriteLine(oArgs);
        }
    }
}
