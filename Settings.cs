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
    public string UrlServerData = "http://develop.altamedia.vn/qt/quiz/data/";
    //http://develop.altamedia.vn/ht/venus/api/getCategories
    //http://develop.altamedia.vn/ht/APIDOC/mock/38fqB65WVGrRxlOZ/global/api-getcategories
    public string UrlKinds = "http://develop.altamedia.vn/ht/venus/api/getCategories";
    //http://develop.altamedia.vn/ht/venus/api/getQuestions?cat_id={0}
    //http://develop.altamedia.vn/ht/APIDOC/mock/38fqB65WVGrRxlOZ/global/api-getquestions
    public string UrlPackage = "http://develop.altamedia.vn/ht/venus/api/getQuestions?cat_id={0}";
    //http://develop.altamedia.vn/ht/venus/api/getScores?limit={0}
    //http://develop.altamedia.vn/ht/APIDOC/mock/38fqB65WVGrRxlOZ/global/api-getscores
    public string UrlTop = "http://develop.altamedia.vn/ht/venus/api/getScores?limit={0}";
    //Post Score
    //http://develop.altamedia.vn/ht/venus/api/postScores
    //http://develop.altamedia.vn/ht/APIDOC/mock/38fqB65WVGrRxlOZ/global/api-postscores
    public string UrlPostScore = "http://develop.altamedia.vn/ht/venus/api/postScores";

    //Client API
    public string UrlApiClient = "http://develop.altamedia.vn/ht/venus/api/getQuestionsByCode?code={0}";
    public int TimeCount = 59;
    public int TimeNext = 1;



    public void Load()
    {
        AMGlobal.Settings = AltaStatic.Read<Settings>("Settings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<Settings>(this, "Settings.xml");
    }


}
