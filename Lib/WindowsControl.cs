using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;

public class WindowsControl : MonoBehaviour {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);
    public GameObject PanelMain;

    public static void SetPosition(string AppName, int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, AppName), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }
#endif

    public static void UpdateWindows(string AppName){
        SetPosition(AppName,(int)AMGlobal.Settings.WindowsPositionX, (int)AMGlobal.Settings.WindowsPositionY);
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
            PanelMain.GetComponent<RectTransform>().localScale = new Vector3(AMGlobal.Settings.PanelContentScaleX, AMGlobal.Settings.PanelContentScaleY, AMGlobal.Settings.PanelContentScaleZ);
            PanelMain.GetComponent<RectTransform>().anchoredPosition  = new Vector3(AMGlobal.Settings.PanelPositionX, AMGlobal.Settings.PanelPositionY, 0);
            if (AMGlobal.Settings.PanelContentWith > 0 && AMGlobal.Settings.PanelContentHeight > 0)
            {
                PanelMain.GetComponent<RectTransform>().sizeDelta = new Vector2(AMGlobal.Settings.PanelContentWith, AMGlobal.Settings.PanelContentHeight);
            }
        }
    }
}
