using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class WindowsControl : MonoBehaviour {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);

    public static void SetPosition(string AppName, int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, AppName), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }
#endif

    public static void UpdateWindows(string AppName){
        //SetPosition(AppName,AltaGlobal.Settings.WindowsPositionX,AltaGlobal.Settings.WindowsPositionY);
    }

    private float TimeCount;
    void Update()
    {
        TimeCount += Time.deltaTime;
        if (AMGlobal.Settings.WindowsReadTime && TimeCount > AMGlobal.Settings.WindowsReadTimeLoop)
        {
            TimeCount = 0;
            AMGlobal.Settings.Load();
            WindowsControl.UpdateWindows(AMGlobal.Settings.productName);
        }
    }
}
