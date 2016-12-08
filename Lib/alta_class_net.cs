using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class DataRecieved : EventArgs
{
    public string MSG { get; set; }
    public IPEndPoint IP { get; set; }
}

public class alta_class_net : IDisposable
{
    private ArrayList m_aryClients = new ArrayList();
    //private string keySerect;
    Socket listener;
    public string ipHostStream = "";
    public event EventHandler<DataRecieved> RecievedEvent;
    public event EventHandler<DataRecieved> NewConnectEvent;
    public event EventHandler<DataRecieved> RemoveClientEvent;

    public alta_class_net(int nPortListen = 339)
    {
        // Determine the IPAddress of this machine
        List<IPAddress> aryLocalAddr = new List<IPAddress>();
        string strHostName = "";

        try
        {
            // NOTE: DNS lookups are nice and all but quite time consuming.
            strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            foreach (IPAddress ip in ipEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    aryLocalAddr.Add(ip);
                }

            }
        }
        catch (Exception ex)
        {
            Debug.Log(string.Format("Error trying to get local address {0} ", ex.Message));
            aryLocalAddr.Clear();
            aryLocalAddr.Add(IPAddress.Loopback);
        }

        // Verify we got an IP address. Tell the user if we did
        if (aryLocalAddr == null || aryLocalAddr.Count < 1)
        {
            Debug.LogWarning("Unable to get local address");
            return;
        }
        Debug.Log(string.Format("Listening on : [{0}] {1}:{2}", strHostName, aryLocalAddr[0], nPortListen));

        // Create the listener socket in this machines IP address
        listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(aryLocalAddr[0], nPortListen));
        //listener.Bind( new IPEndPoint( IPAddress.Loopback, 399 ) );	// For use with localhost 127.0.0.1
        listener.Listen(10);
        // Setup a callback to be notified of connection requests
        listener.BeginAccept(new AsyncCallback(this.OnConnectRequest), listener);
    }


    ~alta_class_net()
    {
        listener.Close();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    /// <summary>
    /// Callback used when a client requests a connection. 
    /// Accpet the connection, adding it to our list and setup to 
    /// accept more connections.
    /// </summary>
    /// <param name="ar"></param>
    public void OnConnectRequest(IAsyncResult ar)
    {
        try
        {
            Socket listener = (Socket)ar.AsyncState;
            NewConnection(listener.EndAccept(ar));
            listener.BeginAccept(new AsyncCallback(OnConnectRequest), listener);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.GetBaseException().ToString());
        }
    }
    /// <summary>
    /// Add the given connection to our list of clients
    /// Note we have a new friend
    /// Send a welcome to the new client
    /// Setup a callback to recieve data
    /// </summary>
    /// <param name="sockClient">Connection to keep</param>
    //public void NewConnection( TcpListener listener )
    public void NewConnection(Socket sockClient)
    {
        // Program blocks on Accept() until a client connects.
        //SocketChatClient client = new SocketChatClient( listener.AcceptSocket() );
        SocketControlClient client = new SocketControlClient(sockClient);
        m_aryClients.Add(client);
        Debug.Log(string.Format("Client {0}, joined", client.Sock.RemoteEndPoint));
        if (this.NewConnectEvent != null)
        {
            this.NewConnectEvent(this, new DataRecieved() { IP = client.Sock.RemoteEndPoint as IPEndPoint });
        }
        // Get current date and time.           

        client.SetupRecieveCallback(this);
    }

    private byte[] getByteText(string input)
    {
        byte[] array = System.Text.Encoding.UTF8.GetBytes(input);
        return array;
    }

    /// <summary>
    /// Get the new data and send it out to all other connections. 
    /// Note: If not data was recieved the connection has probably 
    /// died.
    /// </summary>
    /// <param name="ar"></param>
    public void OnRecievedData(IAsyncResult ar)
    {
        SocketControlClient client = (SocketControlClient)ar.AsyncState;
        byte[] aryRet = client.GetRecievedData(ar);
        if (aryRet.Length < 1)
        {
            Debug.Log(string.Format("Client {0}, disconnected", client.Sock.RemoteEndPoint));
            if (this.RemoveClientEvent != null)
            {
                this.RemoveClientEvent(this, new DataRecieved() { IP = client.Sock.RemoteEndPoint as IPEndPoint });
            }
            client.Sock.Close();
            m_aryClients.Remove(client);
            return;
        }
        string str = System.Text.Encoding.UTF8.GetString(aryRet);

        if (RecievedEvent != null)
        {
            RecievedEvent(this, new DataRecieved() { MSG = str, IP = client.Sock.RemoteEndPoint as IPEndPoint });
        }

        client.Sock.Send(getByteText("OK|200|" + str.ToUpper()), SocketFlags.None);
        client.SetupRecieveCallback(this);

    }

    /// <summary>
    /// Gui tin nhan den tat ca cac may
    /// </summary>
    /// <param name="Msg"></param>
    public void SendMsg(string Msg)
    {
        int count = this.m_aryClients.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                SocketControlClient client = this.m_aryClients[i] as SocketControlClient;
                try
                {
                    client.Sock.Send(getByteText(Msg), SocketFlags.None);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.GetBaseException().ToString());
                }
            }
        }
    }

    public bool SendMsg(string Msg, string ip)
    {
        int count = this.m_aryClients.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                SocketControlClient client = this.m_aryClients[i] as SocketControlClient;
                if ((client.Sock.RemoteEndPoint as IPEndPoint).Address.ToString() == ip)
                {
                    client.Sock.Send(getByteText(Msg), SocketFlags.None);
                    return true;
                }

            }
        }
        return false;
    }

    public bool SendMsg(string Msg, IPEndPoint ip)
    {
        int count = this.m_aryClients.Count;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                SocketControlClient client = this.m_aryClients[i] as SocketControlClient;
                try
                {

                    if ((client.Sock.RemoteEndPoint as IPEndPoint).Address.ToString() == ip.Address.ToString())
                    {
                        client.Sock.Send(getByteText(Msg), SocketFlags.None);
                        Debug.Log((client.Sock.RemoteEndPoint as IPEndPoint).Address.ToString() + "_" + Msg);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.GetBaseException().ToString());
                }

            }
        }
        return false;
    }

    public void Dispose()
    {
        listener.Close();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}

public enum TYPE
{
    ADMIN, USER, NONE
}

internal class SocketControlClient
{
    public TYPE type = TYPE.NONE;
    private Socket m_sock;						// Connection to the client
    private byte[] m_byBuff = new byte[1024];		// Receive data buffer
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sock">client socket conneciton this object represents</param>
    public SocketControlClient(Socket sock)
    {
        m_sock = sock;
    }



    // Readonly access
    public Socket Sock
    {
        get { return m_sock; }
    }

    /// <summary>
    /// Setup the callback for recieved data and loss of conneciton
    /// </summary>
    /// <param name="app"></param>
    public void SetupRecieveCallback(alta_class_net app)
    {
        try
        {
            AsyncCallback recieveData = new AsyncCallback(app.OnRecievedData);
            m_sock.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, this);
        }
        catch (Exception ex)
        {
            Debug.LogError(string.Format("Recieve callback setup failed! {0}", ex.Message));
        }
    }

    /// <summary>
    /// Data has been recieved so we shall put it in an array and
    /// return it.
    /// </summary>
    /// <param name="ar"></param>
    /// <returns>Array of bytes containing the received data</returns>
    public byte[] GetRecievedData(IAsyncResult ar)
    {
        int nBytesRec = 0;
        try
        {
            nBytesRec = m_sock.EndReceive(ar);
        }
        catch { }
        byte[] byReturn = new byte[nBytesRec];
        Array.Copy(m_byBuff, byReturn, nBytesRec);

        return byReturn;
    }


}
