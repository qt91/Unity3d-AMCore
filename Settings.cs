using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Settings
{
    public string productName = "AltamediaUnityLib";

    //Socket Client
    public string SocketIP = "127.0.0.1";
    public int SocketPort = 339;

    //Socket Server
    public int SocketPortServer = 1812;

    public bool WindowsReadTime = true;//State ReadTime if equal "true" auto loop reload follow ReadTimeLoop
    public float WindowsReadTimeLoop = 1f;//Time loop reload setting file
    public int WindowsPositionX = -5;//Postion X Left to Right
    public int WindowsPositionY = -25;//Postion Y Top to Down
    public int WindowsScaleX = 1;//Scale show window
    public int WindowsScaleY = 1;//Scale show window

    //Registry for this Project
    public int NumberPlay = 0;// Number of times play
    public float TimeEachFrame = 0.3f;//Time each frame 
    public int TimeCountDownMain = 3;
    public int TimeCountDownNext = 2;
    //F1
    public float DistanceStartGame = 4f;
    public float DistanceTimeCount = 1;

    public float MouseTimeHoverClick = 3;

    public void Load()
    {
        AMGlobal.Settings = AltaStatic.Read<Settings>("Settings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<Settings>(this, "Settings.xml");
    }


}
