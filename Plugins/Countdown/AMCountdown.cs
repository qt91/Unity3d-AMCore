using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AMCountdown : MonoBehaviour
{
    public enum CountDownType
    {
        Text = 0,
        Imgae = 1,
    }

    public CountDownType Type;
    [Header("Start For Type Text")]
    public Text OutputText;
    public string CountFormat = "00";

    [Header("Start For Type Image")]
    public Image OutputImage;
    public Sprite[] ListSprite;

    [Header("For General")]
    public int CountStart = 3;
    public int CountEnd = 0;

    [Header("Time count in step, seconds")]
    public float TimeStep = 1;

    [Header("Destroy gameobject when complete")]
    public bool destroy = false;


    private float TimeCount = 0;
    private bool CountDirection = true;//Count Down
    private bool isComplete = false;
    private int CountIndexSprite = -1;

    void Start()
    {
        CountStart = AMGlobal.Settings.TimeCountDownStart;
        if (CountStart < CountEnd)//Count up
        {
            CountDirection = false;
        }
    }

    void Update()
    {
        TimeCount += Time.deltaTime;
        if(isComplete == false)
        {
            if (Type == 0)//Text
            {
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
                }
            }
            else//Image
            {
                if (TimeCount >= TimeStep)
                {
                    TimeCount = 0;
                    CountStart++;
                    CountIndexSprite++;
                    OutputImage.sprite = ListSprite[CountIndexSprite];
                    if (CountStart > CountEnd)
                    {
                        isComplete = true;
                    }
                }
            }
        }

        

        //Destroy gameobject if request
        if (destroy && isComplete)
        {
            //For project
            


            Destroy(gameObject);
        }


    }

}
