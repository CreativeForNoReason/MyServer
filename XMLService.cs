using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer
{
    public static class XMLService
    {
        private static string _path => AppContext.BaseDirectory + "StoredCodes.xml";

        public static void WriteXML<T>(T input)
        {
            System.Xml.Serialization.XmlSerializer writer = 
                new System.Xml.Serialization.XmlSerializer(typeof(T));

            System.IO.FileStream file = System.IO.File.Create(_path);

            writer.Serialize(file, input);
            file.Close();
        }

        public static T[] ReadXML<T>()
        {  
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(T[]));

            if (System.IO.File.Exists(_path))
            {
                System.IO.StreamReader file = new System.IO.StreamReader(_path);
                T[] output = (T[])reader.Deserialize(file);
                file.Close();

                return output;
            }
            else
            {
                return Array.Empty<T>();
            }       
        }
    }
}
