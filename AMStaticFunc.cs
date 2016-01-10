using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public static class AMStaticFunc {

    //Conver Sprite to Texture2D
    public static Texture2D textureFromSprite(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }

    //Get RenderTexture from RawImage
    public static Texture2D CopyRenderTexture(RenderTexture rt)
    {
        RenderTexture prevRT = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        texture.Apply(false, false);

        RenderTexture.active = prevRT;

        return texture;
    }

    public static string SaveFilePng(Texture2D tx, string name)
    {
        byte[] bytes = tx.EncodeToPNG();
        Object.Destroy(tx);
        File.WriteAllBytes(name, bytes);
        return name;
    }

    public static Sprite Texture2DToSprite(Texture2D tx, Vector2 size)
    {
        Sprite sp = Sprite.Create(tx, new Rect(Vector2.zero, size), Vector2.zero);
        return sp;
    }
    public static IEnumerator LoadTexture2D(this RawImage rar, string url, Vector2 size, TextureFormat format = TextureFormat.RGB24, bool mipmap = false)
    {
        Texture2D tex = new Texture2D((int)size.x, (int)size.y, format, mipmap);
        //tex.name = "Demo";
        WWW www = new WWW("file:///" + url);
        yield return www;
        //Debug.Log(url);
        if (string.IsNullOrEmpty(www.error))
        {
            www.LoadImageIntoTexture(tex);
            rar.texture = tex;
        }
    }

    public static IEnumerator LoadTexture2D(this Texture2D te, string url, Vector2 size, TextureFormat format = TextureFormat.RGB24, bool mipmap = false)
    {
        Texture2D tex = new Texture2D((int)size.x, (int)size.y, format, mipmap);
        //tex.name = "Demo";
        WWW www = new WWW("file:///" + url);
        yield return www;
        Debug.Log(url);
        if (string.IsNullOrEmpty(www.error))
        {
            www.LoadImageIntoTexture(tex);
            te = tex;
        }
    }

    public static IEnumerator LoadSprite2D(this Sprite ps, string url)
    {
        Texture2D texture = new Texture2D(1, 1);
        WWW www = new WWW("file:///" + url);
        yield return www;
        if (string.IsNullOrEmpty(www.error))
        {
            www.LoadImageIntoTexture(texture);
            ps = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
       
    }


    //SerialPort
    public static string guessPortNameWindows()
    {
        var devices = System.IO.Ports.SerialPort.GetPortNames();

        if (devices.Length == 0) // 
        {
            return "COM3"; // probably right 50% of the time		
        }
        else
            return devices[0];
    }
}
