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

    //Registry for this Project
    public int percentSymptom = 50;//Phần trăm triệu chứng xuất hiện
    public int percentSymptomWhite = 50;//Phần trăm triệu chứng xuất hiện ( Trieu chứng dùng Balm Trắng )
    public int percentSymptomWhite1 = 50;//Trieu chung trang thu nhat
    public int percentSymptomWhite2 = 50;//Trieu chung trang thu hai
    public int percentSymptomBlack = 50;//Phần trăm triệu chứng xuất hiện ( Trieu chứng dùng Balm Đen )
    public int percentSymptomBlack1 = 50;//Trieu chung den thu nhat
    public int percentSymptomBlack2 = 50;//Trieu chung den thu hai

    public float SpeedMax = 2;//Tốc độ tối đa
    public float SpeedMin = 0;//Tốc độ tối thiểu
    public int TimeCountDownStart = 3;//Countdown Start Game
    public int TimeCountStart = 60;//Thời gian thời gian bắt đâu
    public int TimeCountEnd = 0;//Thơi gian kết thúc

    

    public float TimeNextSenceF5 = 5;//Thoi gian next scene report
    public float TimeViewImageEndGame = 10;//Thoi gian xem anh va bat dau lai game

    public float TimeUp = 0.06f;//Toc do tang dan
    public float TimeDown = 0.02f;//Toc do giam dan khi ko dap
    public float SppedMove = 0.7f;//Toc do cua

    public float DistanceUnit = 1;//Don vi de goi ham lam to hinh country qua tung step
    public float ScaleUnit = 0.02f;//Don vi tang scale sau moi lan DistanceUnit ( Nho so nay lai nghia la khoang cach cac quoc gia se tang len




    public void Load()
    {
        AMGlobal.Settings = AltaStatic.Read<Settings>("Settings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<Settings>(this, "Settings.xml");
    }


}
