using System.Text;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography;
using System.Net;
using System.IO;

public static class StringExtensions
{
	public static Stream GenerateStreamFromString(this string s)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
    public static string toFileName(this string Url)
    {
        char[] splitchar = { ' ', '\\', '/' };
        string[] tmp = Url.Split(splitchar);
        return tmp[tmp.Length - 1];
    }
    public static string toIP(this string Url)
    {
        return new Uri(Url).Host;
    }
    public static string EncodeNonAsciiCharacters(this string value)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in value)
        {
            if (c > 127)
            {
                // This character is too big for ASCII
                string encodedValue = "\\u" + ((int)c).ToString("x4");
                sb.Append(encodedValue);
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
    public static string DecodeEncodedNonAsciiCharacters(this string value)
    {
        return Regex.Replace(
            value,
            @"\\u(?<Value>[a-zA-Z0-9]{4})",
            m =>
            {
                return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
            });
    }

    public static string toMD5(this string input)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            byte[] data = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(input));
            String tmp = BitConverter.ToString(data).Replace("-", "");
            return tmp.ToLower();
        }
    }

    public static bool isEmpty(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return true;
        return false;
    }

    public static string Clone(this string data, float time)
    {
        int l = Convert.ToInt32(data.Length * time);
        string tmp = data;
        if (l > data.Length)
        {
            int j = 0;
            for (int i = data.Length; i < l; i++, j++)
            {
                if (j >= data.Length)
                {
                    j = 0;
                }
                tmp += data[j];
            }
        }
        else
        {
            tmp.Remove(l, data.Length - l);
        }
        return tmp;

    }
    public static byte[] toBytes(this string data)
    {

        return Encoding.UTF8.GetBytes(data);
    }
   

}
