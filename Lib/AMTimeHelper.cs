using UnityEngine;
using System.Collections;

public static class AMTimeHelper {
    //TCac ham ho tro ve thoi giang

    //Chuyen doi thoi gian tu giay sang phut
    public static string ConvertTimeSecoundToMinute(float secound)
    {
        int minutes = Mathf.FloorToInt(secound / 60F);
        int seconds = Mathf.FloorToInt(secound - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        return niceTime;
    }
}
