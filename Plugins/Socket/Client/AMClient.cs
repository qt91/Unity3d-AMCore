using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AMClient : MonoBehaviour
{
    public static AMClient Static;

    public InputField inputFieldIP;
    public InputField inputFieldPort;
    public Toggle toggleAutoConnect;

    public InputField inputSendMessages;
    public Text TextDebug;
    public int timeReconnect = 1;

    //get data form file or from input of UI
    public bool dataFromFile = true;
    public bool autoReconnect = true;
    //
    public string clientName = "Client";
    public bool debugConsole = false;

    private NetworkView networkView;
    private bool flagConnect = false;
    private float CountReconnect = 3;
    private float Count = 0;
    private string ipValue;
    private int portValue;
    private string nameValue;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        networkView = gameObject.GetComponent<NetworkView>();
        Static = this;
    }
    void Start()
    {
        if (dataFromFile)
        {
            ipValue = AMGlobal.Settings.SocketIP;
            portValue = AMGlobal.Settings.SocketPort;
            nameValue = AMGlobal.Settings.productName;
            toggleAutoConnect.isOn = AMGlobal.Settings.autoConnect;
            autoReconnect = AMGlobal.Settings.autoConnect;
        }
        else
        {
            ipValue = inputFieldIP.text;
            portValue = int.Parse(inputFieldPort.text);
        }

        //Set value from setting file
        inputFieldIP.text = ipValue;
        inputFieldPort.text = portValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Registry Shift + C => Reconnect
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
        {
            autoReconnect = true;
            ClickConnectServer();
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.D))
        {
            ClickDisconnect();
            autoReconnect = false;
        }

        //Count time and reconnect if is disconnect
        Count += Time.deltaTime;
        if (Count > timeReconnect && flagConnect == false && autoReconnect)
        {
            Count = 0;
            ClickConnectServer();
        }

    }

    public void ClickConnectServer()
    {
        if (flagConnect == false)
        {
            if (debugConsole)
            {
                Debug.Log("Connecting server with info " + ipValue + ":" + portValue);
            }
            Network.Connect(ipValue, portValue);
        }
    }

    public void ClickDisconnect()
    {
        Network.Disconnect();
    }

    void OnConnectedToServer()
    {
        //TextDebug.text = "Connected to server";
        //Send to server reginstry client
        //SendServer(nameValue);
        if (debugConsole)
        {
            Debug.Log("Connected server with info " + ipValue + ":" + portValue);
        }
        flagConnect = true;
        SendServer("ClientName|" + AMGlobal.Settings.ClientName + AMGlobal.Settings.ClientID);
        //For current project
        SendServer("LCD");

    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if (debugConsole)
        {
            Debug.Log("Disconted with server with info " + ipValue + ":" + portValue);
        }
        flagConnect = false;
    }

    public void SendToServer()
    {
        SendServer(inputSendMessages.text);
        
    }

    //Send to server
    public void SendServer(string str)
    {
        //Debug.Log(str);
        if (flagConnect)
        {
            Debug.Log("toServer:"+str);
            networkView.RPC("OnReceiverString", RPCMode.Server, str);// + InputFieldRandom.text
        }
    }

    //For server mobile
    [RPC]
    public void OnReceiverString(string msg, NetworkMessageInfo player)
    {
        Debug.Log(msg);
        //IO|#A[R][G][B][Trang Thai][Speed]
        string[] ex = msg.Split('|');
        if(ex.Length == 2)
        {
            if(ex[0].ToUpper() == "IO")
            {
                //AMSerial.Static.SendToBoard(ex[1]);
            }
        }

        //Server to client
        Control.Static.OnRecievedDataEvent(msg);

    }

    [RPC]
    public void OnReceicerObject(NetworkMessageInfo info, object obj)
    {

    }
}
