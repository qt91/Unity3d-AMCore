using System.Collections.Generic;
using UnityEngine;

public static class AMGlobal
{
    public static Settings Settings = new Settings();
    public static int FrameCurrent;
    public static float ScaleMovementX = 1;
    public static float ScaleMovementY = 1;

    //For proejct

    public static int KindIDCurrent;
    public static int QuestionNumbersCurrent = 1;
    //public static QuestionPackage questionPackage;
    //public static Question QuestionShow;
    public static int NumbersWin;
    public static int TimeCount;
    public static int CountTeam = 0;
    public static bool isReady = false;

    public static NetworkPlayer MobileClient = new NetworkPlayer();
    public static string ImageCaptureCurrent;

    //Report
    public static int ContryNumber;
    public static int TextDauChan;
    public static int TextBongGan;
    public static int TextNgatMui;
    public static int TextNhucCo;


    public static void Reset()
    {
        ContryNumber = 0;
        TextDauChan = 0;
        TextBongGan = 0;
        TextNgatMui = 0;
        TextNhucCo = 0;
        ImageCaptureCurrent = "";

        NumbersWin = 0;
        QuestionNumbersCurrent = 1;
        KindIDCurrent = 0;
    }

    public enum PlayerType { Ipad, LED };
    public static NetworkPlayer[] listPlayer;
}
