using UnityEngine;
using System.Collections;
using System;

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
       
    }

    [RPC]
    public void OnReceicerObject(NetworkMessageInfo info, object obj)
    {

    }
}
