using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class AMServer : AltaControllerInterFace
{
    NetworkView networkView;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        networkView = GetComponent<NetworkView>();
        RunServer();
    }
	
	// Update is called once per frame
	void Update () {

        //Reset Server
        if (Input.GetKeyDown(KeyCode.R))
        {
            RunServer();
        }
	}

    //Socket ========================
    public void RunServer()
    {
        if (!Network.isServer)
        {
            Network.InitializeServer(1000, AMGlobal.Settings.SocketPortServer, false);
        }
    }

    void OnServerInitialized()
    {
        Debug.Log("Server running: " + AltaStatic.GetIpCurrent() + ":" + AMGlobal.Settings.SocketPortServer);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log(player.ipAddress + " Disconnect");

    }

    public void SendClient(string str, NetworkPlayer Player)
    {
        networkView.RPC("OnReceiverString", Player, str);
    }

    public void SendClientAll(string str)
    {
        networkView.RPC("OnReceiverString", RPCMode.Others, str);
    }


    [RPC]
    public void OnReceiverString(string msg, NetworkMessageInfo player)
    {
        NetworkPlayer Player = player.sender;
        if(msg.ToUpper() == "MobileClient".ToUpper())
        {
            AMGlobal.MobileClient = Player;
            SendClient("Time_0.1", AMGlobal.MobileClient);
        }

        if (msg.ToUpper() == "Start".ToUpper())
        {
            SendClient("Play", AMGlobal.MobileClient);
            SceneManager.LoadScene("MainGame");
        }

        //Reset Game
        if (msg.ToUpper() == "Reset".ToUpper())
        {
            
        }

        //SendClient("Play", AMGlobal.MobileClient);
    }

    [RPC]
    public void OnReceiverVector(Quaternion vt, NetworkMessageInfo player)
    {
        NetworkPlayer Player = player.sender;
        if(Player.ipAddress == AMGlobal.MobileClient.ipAddress && Application.loadedLevelName == "MainGame")
        {
            //Debug.Log(vt);
            //PlayerControl.Static.gameObject.transform.localRotation = Quaternion.EulerRotation(0, vt.y, 0);
            CubeTest.Static.gameObject.transform.localRotation = Quaternion.EulerRotation(0, vt.y, 0);
            //Debug.Log(CubeTest.Static.gameObject.transform.localRotation.eulerAngles.y);
            float y = CubeTest.Static.gameObject.transform.localRotation.eulerAngles.y;
            //Debug.Log(y);
            float vlu = 7;//Khoang cach checnh lech
            float kL = 355;//Goc hien tai
            float Right = vlu + kL;//Goc phai
            float Left = kL - vlu;//Goc trai
            if(y < 30)
            {
                y += 360;
            }

            //Debug.Log(y+"--"+ AMGlobal.Settings.SppedMove);

            if (y > Right)
            {
                ThirdPersonUserControl.Static.MoveLeft(AMGlobal.Settings.SppedMove);
            }
            else
            {
                if (y < Left)
                {
                    ThirdPersonUserControl.Static.MoveRight(AMGlobal.Settings.SppedMove);
                }
                else {
                    ThirdPersonUserControl.Static.MoveReset();
                }
            }
            //CubeTest.Static.thisGame.transform.localRotation = new Quaternion(0,vt.y,0,vt.z);
        }
    }

    [RPC]
    public void OnReceicerObject(NetworkMessageInfo info, object obj)
    {

    }
}
