using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Settings
{
    public string productName = "LEDMain";

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
    public int WindowsPositionX = -5;//Postion X Left to Right
    public int WindowsPositionY = -25;//Postion Y Top to Down
    //Move position main panel
    public int PanelPositionX = 0;//Postion X Left to Right
    public int PanelPositionY = 0;//Postion Y Top to Down

    //For panel Main UI => Scale
    public int PanelContentScaleX = 1;//Scale show window
    public int PanelContentScaleY = 1;//Scale show window
    public int PanelContentScaleZ = 1;//Scale show window
    // For panel content => Size
    public float PanelContentWith = 1536;//width
    public float PanelContentHeight = 768;//height


    public float TimeCountDownStart = 3;//Time count start game
    public float TimeNextSenceF5 = 5;//Thoi gian next scene report
    public float TimeViewImageEndGame = 10;//Thoi gian xem anh va bat dau lai game





    public void Load()
    {
        AMGlobal.Settings = AltaStatic.Read<Settings>("Settings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<Settings>(this, "Settings.xml");
    }


}
