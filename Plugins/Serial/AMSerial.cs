using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class AMSerial : MonoBehaviour {

    private SerialPort Se1;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start () {

        Se1 = new SerialPort(Uniduino.Arduino.guessPortName(), 9600);
        ConnectBoard1();
    }

    void ConnectBoard1()
    {


        try
        {
            Se1.Open();
            Se1.ReadTimeout = 1;
        }
        catch (Exception)
        {
            string MsgStatus = Uniduino.Arduino.guessPortName() + " không thể kết nối với mạch\n";
            Debug.Log(MsgStatus);
        }
    }

    // Update is called once per frame
    void Update () {
        //Tin hieu Input
        if (Se1.IsOpen)
        {
            try
            {
                int i = Se1.ReadByte();
                if (i != 0)
                {

                    string[] ab = Se1.ReadLine().Split('$');
                    int num = int.Parse(ab[0]);
                    //Debug.Log(num);
                    for (int k = 0; k < num; k++)
                    {
                        ThirdPersonUserControl.Static.MoveUp(AMGlobal.Settings.SpeedMax,AMGlobal.Settings.TimeUp, AMGlobal.Settings.TimeDown);
                    }
                }
                //qtClient.sendData("A_1");
                //Debug.Log("A_1");
            }
            catch (System.Exception)
            {
                //throw;
            }
        }
    }
}
