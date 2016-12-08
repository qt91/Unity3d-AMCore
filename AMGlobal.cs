using System.Collections.Generic;
using UnityEngine;

public static class AMGlobal
{
    public static Settings Settings = new Settings();
    public static ProjectSettings ProjectSettings = new ProjectSettings();
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

    //My Project


    public static void Reset()
    {
        ImageCaptureCurrent = "";

        NumbersWin = 0;
        QuestionNumbersCurrent = 1;
        KindIDCurrent = 0;
    }

    public enum PlayerType { Ipad, LED };
    public static NetworkPlayer[] listPlayer;
}
