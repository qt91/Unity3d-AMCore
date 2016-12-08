using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class AMTimeCount : MonoBehaviour {

    public static AMTimeCount Static;
    [Header("Start For Type Text")]
    public Text OutputText;
    public string CountFormat = "00";

    [Header("For General")]
    public float CountStart = 3;
    public int CountEnd = 0;

    [Header("Time count in step, seconds")]
    public float TimeStep = 1;

    public float SetupOne = 4;

    private bool CountDirection = true;//Count Down
    private float TimeCount = 0;

    public bool StartOnPlay = false;

    private bool isComplete;


    void Awake()
    {
        Static = this;
    }

    void Start () {
        //CountStart = AMGlobal.Settings.TimeCountStart;
        OutputText.text = CountStart.ToString(CountFormat);
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
                        isComplete = true;
                    }
                }
                else
                {
                    OutputText.text = CountStart.ToString(CountFormat);
                    CountStart++;
                    if (CountStart > CountEnd)
                    {
                        isComplete = true;
                    }
                }
                OutputText.text = CountStart.ToString(CountFormat);
            }

            if (isComplete)
            {
                SceneManager.LoadScene("Report");
            }

            
        }
    }
}
