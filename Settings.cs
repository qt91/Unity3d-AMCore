using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Settings
{
    public string productName = "ClimbingWall";

    //Socket Client
    public string SocketIP = "192.168.10.65";//
    public int SocketPort = 6789;
    public string ClientGroup = "";//Group name ( Multip client to a group )
    public int ClientID = 1;// Client ID
    public string ClientName = "LCD";//Client name
    public bool autoConnect = true;

    //Socket Server
    public int SocketPortServer = 6789;

    public bool WindowsReadTime = true;//State ReadTime if equal "true" auto loop reload follow ReadTimeLoop
    public float WindowsReadTimeLoop = 1f;//Time loop reload setting file
    //Move position windown
    public float WindowsPositionX = -5;//Postion X Left to Right
    public float WindowsPositionY = -25;//Postion Y Top to Down
    //Move position main panel
    public float PanelPositionX = 0;//Postion X Left to Right
    public float PanelPositionY = 0;//Postion Y Top to Down

    //For panel Main UI => Scale
    public float PanelContentScaleX = 1;//Scale show window
    public float PanelContentScaleY = 1;//Scale show window
    public float PanelContentScaleZ = 1;//Scale show window
    // For panel content => Size
    public float PanelContentWith = 1536;//width
    public float PanelContentHeight = 768;//height


    public float ScalePointBody = 30;





    public void Load()
    {
        AMGlobal.Settings = AltaStatic.Read<Settings>("Settings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<Settings>(this, "Settings.xml");
    }


}
