using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

// Count time play game
public class AMTimeCount : MonoBehaviour {

    public static AMTimeCount Static;
    [Header("Start For Type Text")]
    public Text OutputText;
    public string CountFormat = "00";

    [Header("Cấu hình thời gian")]
    public float CountStart = 3;
    public int CountEnd = 0;

    [Header("Thời gian đếm, mặc định 1 giây")]
    public float TimeStep = 1;// Thơi gian đmế

    [Header("Chuyển đổi sang dạng phút")]
    public bool convertToSecound = true;

    private bool CountDirection = true;//Count Down
    private float TimeCount = 0;

    public bool StartOnPlay = false;

    private bool isComplete;

    public delegate void CompleteAction();
    public static event CompleteAction OnComplete;


    void Awake()
    {
        Static = this;
    }

    void Start () {
        //CountStart = AMGlobal.Settings.TimeCountStart;
        OutputText.text = CountStart.ToString(CountFormat);

        if (convertToSecound)
        {
            OutputText.text = AMTimeHelper.ConvertTimeSecoundToMinute(CountStart);
        }
        else
        {
            OutputText.text = CountStart.ToString(CountFormat);
        }

        if (CountStart < CountEnd)//Count up
        {
            CountDirection = false;
        }
    }

    public void StartCountTime()
    {
        StartOnPlay = true;
    }

    private void NextTimeStep()
    {
        
    }

    // Update is called once per frame
    void Update () {

        if (StartOnPlay)
        {
            TimeCount += Time.deltaTime;


            if (TimeCount >= TimeStep)
            {
                TimeCount = 0;
                if (CountDirection)
                {
                    OutputText.text = CountStart.ToString(CountFormat);
                    CountStart--;

                    if (CountStart < CountEnd)
                    {
                        if(OnComplete != null)
                        {
                            OnComplete();
                        }
                        
                        isComplete = true;
                    }
                }
                else
                {
                    OutputText.text = CountStart.ToString(CountFormat);
                    CountStart++;
                    if (CountStart > CountEnd)
                    {
                        if (OnComplete != null)
                        {
                            OnComplete();
                        }
                        isComplete = true;
                    }
                }

                if (convertToSecound)
                {
                    OutputText.text = AMTimeHelper.ConvertTimeSecoundToMinute(CountStart);
                }
                else
                {
                    OutputText.text = CountStart.ToString(CountFormat);
                }
            }

            
        }
    }
}
