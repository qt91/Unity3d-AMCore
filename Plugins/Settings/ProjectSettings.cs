using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ProjectSettings{
    //Registry valiable setting for project
    public float secondReloadReadFile = 0.1f;
    public string linkFileData1 = @"E:\Program\Xampp\htdocs\metaware\a.txt";
    public string linkFileData2 = @"E:\Program\Xampp\htdocs\metaware\b.txt";
    public float timeLoopBarPower = 1;

    public void Load()
    {
        AMGlobal.ProjectSettings = AltaStatic.Read<ProjectSettings>("ProjectSettings.xml");
    }

    public void Save()
    {
        AltaStatic.Write<ProjectSettings>(this, "ProjectSettings.xml");
    }
}
