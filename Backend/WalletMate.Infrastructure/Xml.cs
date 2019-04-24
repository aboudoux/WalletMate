using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WalletMate.Infrastructure
{
    public static class Xml
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("ISO-8859-1");

        public static T Deserialize<T>(string xmlContent)
        {
            using (var stream = new MemoryStream(Encoding.GetBytes(xmlContent)))
            {
                using (var reader = new StreamReader(stream, Encoding))
                {
                    return Load<T>(reader);
                }
            }
        }
        public static string Serialize<T>(T serializableObject)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream, Encoding))
                {
                    SaveTo(writer, serializableObject);
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static T DeserializeFrom<T>(string xmlFilePath)
        {
            if (xmlFilePath == null) throw new ArgumentNullException("xmlFilePath");
            using (var streamReader = new StreamReader(xmlFilePath, Encoding)) {
                return Load<T>(streamReader);
            }
        }
        public static void SaveTo<T>(string filePath, T serializableObject)
        {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            using (var writer = new StreamWriter(filePath)) {
                SaveTo(writer, serializableObject);
            }
        }

        
        private static T Load<T>(TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            var serializer = new XmlSerializer(typeof (T));
            using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings {CheckCharacters = true})) {
                return (T) serializer.Deserialize(xmlReader);
            }
        }
        private static void SaveTo<T>(TextWriter writer, T serializableObject)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            var serialiseur = new XmlSerializer(typeof (T));
            using (var xmlWriter = XmlWriter.Create(writer, new XmlWriterSettings {CheckCharacters = true, Indent = true, Encoding = Encoding})) {
                serialiseur.Serialize(xmlWriter, serializableObject);
            }            
        }
    }
}