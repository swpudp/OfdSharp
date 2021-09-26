using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OfdSharp.Utils
{
    /// <summary>
    /// XML 操作辅助类
    /// </summary>
    public static class XmlUtils
    {
        /// <summary>
        /// 从流反序列化对象实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static T Deserialize<T>(Stream stream)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                memory.Seek(0, SeekOrigin.Begin);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (XmlReader xmlReader = XmlReader.Create(memory))
                {
                    T instance = (T)serializer.Deserialize(xmlReader);
                    return instance;
                }
            }
        }

        /// <summary>
        /// 从文件反序列化对象实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static T Deserialize<T>(string fullName)
        {
            using (var fileStream = new FileStream(fullName, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (XmlReader xmlReader = XmlReader.Create(fileStream))
                {
                    T instance = (T)serializer.Deserialize(xmlReader);
                    return instance;
                }
            }
        }

        /// <summary>
        /// 序列化一个对象为XML字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter xmlReader = new StringWriter())
            {
                serializer.Serialize(xmlReader, obj);
                return xmlReader.ToString();
            }
        }
    }
}
