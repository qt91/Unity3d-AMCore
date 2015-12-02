using System.Collections.Generic;
using UnityEngine;

public static class AMGlobal
{
    public static Settings Settings = new Settings();
    public static int FrameCurrent;
    public static float ScaleMovementX = 1;
    public static float ScaleMovementY = 1;

    public static bool isGrab;

    //List Image
    //public static List<Texture> ListTextureTakePhoto = new List<Texture>();
    //public static List<Sprite> ListSpriteTakePhoto = new List<Sprite>();
    public static List<string> ListStringTakePhoto;
    public static string UrlSave;
    public static string UrlSaveGif;
    public static void Reset()
    {
        AMGlobal.ListStringTakePhoto = new List<string>();
        AMGlobal.isGrab = false;
    }
}
