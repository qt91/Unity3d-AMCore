using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System;
using System.Text;

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
        UnityEngine.Object.Destroy(tx);
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

    public static IEnumerator downloadImg(this Sprite ps,string url)
    {
        Texture2D texture = new Texture2D(1, 1);
        WWW www = new WWW(url);
        yield return www;
        www.LoadImageIntoTexture(texture);

        Sprite image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        ps = image;
        Debug.Log("Xong ko sao");
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

    //Get list info player type form 
    public static string getListPlayerType()
    {
        string Str = "";
        foreach (AMGlobal.PlayerType value in Enum.GetValues(typeof(AMGlobal.PlayerType)))
        {
            Str += value+"|";
        }
        return Str;
    }

    //Support Get data
    //Get question form id

    //Get data from list data every
    //public static T DataFromID<T>(T[] data,int id)
    //{
    //    foreach (T item in data)
    //    {
    //        if((item as Question).id == id){
    //            return item;
    //        }
    //    }
    //    return default(T);
    //}
    //// Get dât from so thu tu
    //public static T DataFromNumbers<T>(T[] data, int numbers)
    //{
    //    foreach (T item in data)
    //    {
    //        if ((item as Question).numbers == numbers)
    //        {
    //            return item;
    //        }
    //    }
    //    return default(T);
    //}

    //public static T DataFromNumbersAnswer<T>(T[] data, int numbers)
    //{
    //    foreach (T item in data)
    //    {
    //        if ((item as Answer).numbers == numbers)
    //        {
    //            return item;
    //        }
    //    }
    //    return default(T);
    //}

    //AM Color function
    public static Color Color(float r, float g, float b, float a)
    {
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
    }

    // Capture 
    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
                             Application.streamingAssetsPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }


}
