using UnityEngine;
using System.Collections;
using System.IO;
namespace Alta.Plugin
{
    public enum FileType
    {
        PNG,JPG
    }
    public static class TextureExtension
    {

        public static IEnumerator LoadImage(this Texture2D texture2D, string url, Vector2 size, TextureFormat format = TextureFormat.RGB24, bool mipmap = false)
        {
            Texture2D tex = new Texture2D((int)size.x, (int)size.y, format, mipmap);
            WWW www = new WWW(url);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                www.LoadImageIntoTexture(tex);
                texture2D = tex;
            }
        }

        public static Texture2D CopyRenderTexture(this RenderTexture rt)
        {
            RenderTexture prevRT = RenderTexture.active;
            RenderTexture.active = rt;

            Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
            texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            texture.Apply(false, false);

            RenderTexture.active = prevRT;

            return texture;
        }

        public static string SaveTextureToFile(this Texture2D texture, string fileName, FileType type= FileType.PNG)
        {
            using (Stream s = File.Open(fileName, FileMode.Create))
            {
                byte[] bytes;
                if (type == FileType.PNG)
                {
                    bytes = texture.EncodeToPNG();
                }
                else
                {
                    bytes = texture.EncodeToJPG();
                }
                BinaryWriter binary = new BinaryWriter(s);
                binary.Write(bytes);
                s.Close();
            }
            return fileName;
        }
    }
}