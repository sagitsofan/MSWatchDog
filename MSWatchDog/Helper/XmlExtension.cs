using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace MSWatchDog.Helper
{
    public static class XmlExtension
    {
        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new SerializationException("An error occurred", ex);
            }
        }

        public static T Deserialize<T>(this string value)
        {
            try
            {
                T returnValue = default(T);

                XmlSerializer serial = new XmlSerializer(typeof(T));
                StringReader reader = new StringReader(value);
                object result = serial.Deserialize(reader);

                if (result != null && result is T)
                {
                    returnValue = ((T)result);
                }

                reader.Close();

                return returnValue;
            }
            catch (Exception ex)
            {
                throw new SerializationException("An error occurred", ex);
            }
        }
    }
}