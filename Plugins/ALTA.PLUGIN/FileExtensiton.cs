using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Security.Cryptography;
using System;

public static class FileExtensiton
{
    public static string getMD5(this FileInfo file)
    {
        if (file.Exists)
        {
            using (var md5Hash = MD5.Create())
            {
                using (var stream = File.OpenRead(file.FullName))
                {
                    byte[] data = md5Hash.ComputeHash(stream);
                    string tmp = BitConverter.ToString(data).Replace("-", "");
                    return tmp.ToLower();
                }
            }
        }
        return null;
    }

    //save file từ byte
    public static string SaveFile(this byte[] data, string fileName, bool overRide = false)
    {
        FileInfo file = new FileInfo(fileName);
        if (overRide)
        {
            System.IO.File.WriteAllBytes(file.FullName, data);
        }
        else if (!File.Exists(fileName))
        {
            System.IO.File.WriteAllBytes(file.FullName, data);
        }
        return fileName;
    }

    public static Texture2D LoadTexture2DNative(FileInfo file)
    {
        Texture2D tex = new Texture2D(2, 2, TextureFormat.RGB24, false);
        byte[] data = File.ReadAllBytes(file.FullName);
        tex.LoadImage(data);
        return tex;
    }
}
