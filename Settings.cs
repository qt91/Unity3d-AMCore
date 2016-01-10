using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Settings
{
    public string productName = "AMBuocNhayHoanVuNhi";

    //Socket Client
    public string SocketIP = "127.0.0.1";
    public int SocketPort = 339;

    //Socket Server
    public int SocketPortServer = 1812;

    public bool WindowsReadTime = true;//State ReadTime if equal "true" auto loop reload follow ReadTimeLoop
    public float WindowsReadTimeLoop = 1f;//Time loop reload setting file
    public int WindowsPositionX = -5;//Postion X Left to Right
    public int WindowsPositionY = -25;//Postion Y Top to Down

    //For panel Main UI => Scale
    public int PanelContentScaleX = 1;//Scale show window
    public int PanelContentScaleY = 1;//Scale show window
    public int PanelContentScaleZ = 1;//Scale show window
    // For panel content => Size
    public float PanelContentWith = 1280;//width
    public float PanelContentHeight = 200;//height

    //Registry for this Project
    public string UrlVideoYes = "Videos/Demo1.mov";//Link video dong y
    public string UrlVideoNo = "Videos/Demo2.mov";//Link video khong dong y
    public string UrlVideoLogo = "Videos/Demo4.mov";//Link video default



    public void Load()
    {
        AMGlobal.Settings = AltaStatic.Read<Settings>("Settings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<Settings>(this, "Settings.xml");
    }


}
