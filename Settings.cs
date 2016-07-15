using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Settings
{
    public string productName = "AMBuocNhayHoanVuNhi";

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
    public int WindowsPositionX = -5;//Postion X Left to Right
    public int WindowsPositionY = -25;//Postion Y Top to Down

    //For panel Main UI => Scale
    public int PanelContentScaleX = 1;//Scale show window
    public int PanelContentScaleY = 1;//Scale show window
    public int PanelContentScaleZ = 1;//Scale show window
    // For panel content => Size
    public float PanelContentWith = 1280;//width
    public float PanelContentHeight = 200;//height

    

    public float TimeNextSenceF5 = 5;//Thoi gian next scene report
    public float TimeViewImageEndGame = 10;//Thoi gian xem anh va bat dau lai game

    public float TimeUp = 0.06f;//Toc do tang dan
    public float TimeDown = 0.02f;//Toc do giam dan khi ko dap
    public float SppedMove = 0.7f;//Toc do cua

    public float DistanceUnit = 1;//Don vi de goi ham lam to hinh country qua tung step
    public float ScaleUnit = 0.02f;//Don vi tang scale sau moi lan DistanceUnit ( Nho so nay lai nghia la khoang cach cac quoc gia se tang len

    public string colorDefault = "0,216,255";
    public string colorSelect = "255,132,0";
    public string colorFalse = "255,0,0";
    public string AppPath = @"D:\Project\GameShow\MotPhutMotTramTrieu\Update110716\Code\UserClientLCD\Build\LCD\UserClientLCD_Data\StreamingAssets\Touch3x4\ClientLCD.exe";




    public void Load()
    {
        AMGlobal.Settings = AltaStatic.Read<Settings>("Settings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<Settings>(this, "Settings.xml");
    }


}
