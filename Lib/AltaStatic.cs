using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public static class AltaStatic
{

    /**
     * Get ip current
     * return string
     * format return: 192.168.0.10
     */
    public static string GetIpCurrent()
    {
        string hostName = System.Net.Dns.GetHostName();
        string myIP = System.Net.Dns.GetHostEntry(hostName).AddressList[0].ToString();
        return myIP;
    }
    /* Write from path file and Generic Object 
     */
    public static void Write<T>(T overview, string fileName, string filePath = null)
    {
        if (filePath == null)
        {
            filePath = Application.streamingAssetsPath;
        }
        string file = System.IO.Path.Combine(filePath, fileName);



        if (string.IsNullOrEmpty(file))
            throw new Exception("File Not Empty");
        System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        System.Xml.XmlWriterSettings setting = new System.Xml.XmlWriterSettings();
        setting.Encoding = Encoding.UTF8;
        setting.CloseOutput = true;
        setting.NewLineChars = "\r\n";
        setting.Indent = true;
        if (!File.Exists(file))
        {
            using (Stream s = File.Open(file, FileMode.OpenOrCreate))
            {
                System.Xml.XmlWriter tmp = System.Xml.XmlWriter.Create(s, setting);
                writer.Serialize(tmp, overview);
            }
        }
        else
        {
            using (Stream s = File.Open(file, FileMode.Truncate))
            {
                System.Xml.XmlWriter tmp = System.Xml.XmlWriter.Create(s, setting);
                writer.Serialize(tmp, overview);
            }
        }
    }

    public static T Read<T>(string fileName, string filePath = null)
    {
        if (filePath == null)
        {
            filePath = Application.streamingAssetsPath;
        }

        string file = System.IO.Path.Combine(filePath, fileName);
        System.Xml.XmlReaderSettings setting = new System.Xml.XmlReaderSettings();
        
        if (!File.Exists(file))
        {
            T NewObject = (T)Activator.CreateInstance(typeof(T));
            Write(NewObject, file);
            return NewObject;
        }
        else
        {
            try
            {
                using (Stream s = File.Open(file, FileMode.Open))
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    System.Xml.XmlReader tmp = System.Xml.XmlReader.Create(s, setting);
                    return (T)reader.Deserialize(tmp);
                }
            }
            catch (Exception ex)
            {
                 Debug.Log(ex.GetBaseException().ToString());
            }
            return default(T);

        }
    }
    public static  T ReadContent<T>(string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            try
            {
                using (Stream s = content.GenerateStreamFromString())
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(T));

                    return (T)reader.Deserialize(s);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.GetBaseException().ToString());
            }
        }
        return default(T);        
    }


    

}
