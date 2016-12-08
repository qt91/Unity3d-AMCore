using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RecieveData : EventArgs
{
    public string Msg { get; set; }
}

public class alta_client
{
    public event EventHandler ConnectErrEvent;
    public event EventHandler ConnectSuccessEvent;
    public event EventHandler DisconnectEvent;
    private Socket m_sock;						// Server connection
    private byte[] m_byBuff = new byte[1024];	// Recieved data buffer
    public string sRecieved = "";
    public bool isConnected
    {
        get
        {
            if (this.m_sock == null)
                return false;
            return m_sock.Connected;
        }
    }
    public string ip;
    public event EventHandler<RecieveData> RecieveDataEvent;
    public alta_client()
    {

    }

    /// <summary>
    /// gui du lieu len server
    /// </summary>
    /// <param name="m_tbMessage">du lieu can gui</param>
    public void sendData(string m_tbMessage)
    {
        if (m_sock == null || !m_sock.Connected)
        {
            throw new Exception("Chưa kết nối với Server");
        }

        // Read the message from the text box and send it
        try
        {
            // Convert to byte array and send.
            byte[] byteDateLine = Encoding.UTF8.GetBytes(m_tbMessage);
            m_sock.Send(byteDateLine, byteDateLine.Length, SocketFlags.None);
        }
        catch (Exception)
        {
        }
    }
    public void connect(string ip, int port = 339, int TimeOut = 5000)
    {
        try
        {
            // Close the socket if it is still open
            if (m_sock != null && m_sock.Connected)
            {
                m_sock.Shutdown(SocketShutdown.Both);
                if (this.DisconnectEvent != null)
                {
                    this.DisconnectEvent(this, new EventArgs());
                }
                System.Threading.Thread.Sleep(10);
                m_sock.Close();

            }
            m_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint epServer = new IPEndPoint(IPAddress.Parse(ip), port);
            m_sock.Blocking = false;
            AsyncCallback onconnect = new AsyncCallback(OnConnect);
            IAsyncResult result = m_sock.BeginConnect(epServer, onconnect, m_sock);
            bool success = result.AsyncWaitHandle.WaitOne(TimeOut, true);
            if (!success)
            {
                m_sock.Close();
                if (this.ConnectErrEvent != null)
                {
                    this.ConnectErrEvent(this, new EventArgs());
                }
            }
        }
        catch (Exception)
        {
            if (ConnectErrEvent != null)
            {
                ConnectErrEvent(this, new EventArgs());
            }
        }
        this.ip = ip;
    }
    public void OnConnect(IAsyncResult ar)
    {
        Socket sock = (Socket)ar.AsyncState;
        try
        {
            if (sock.Connected)
            {
                if (this.ConnectSuccessEvent != null)
                {
                    this.ConnectSuccessEvent(this, new EventArgs());
                }
                SetupRecieveCallback(sock);
            }
            else
            {
                if (ConnectErrEvent != null)
                    ConnectErrEvent(this, new EventArgs());
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.GetBaseException().ToString());
            if (ConnectErrEvent != null)
                ConnectErrEvent(this, new EventArgs());
        }
    }
    public void SetupRecieveCallback(Socket sock)
    {
        try
        {
            AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
            sock.BeginReceive(m_byBuff, 0, m_byBuff.Length, SocketFlags.None, recieveData, sock);
        }
        catch (Exception)
        {
        }
    }

    public void disconnect()
    {
        if (this.isConnected)
        {
            this.m_sock.Shutdown(SocketShutdown.Both);
            this.m_sock.Close();
            if (this.DisconnectEvent != null)
            {
                this.DisconnectEvent(this, new EventArgs());
            }
        }
    }

    public void OnRecievedData(IAsyncResult ar)
    {
        Socket sock = (Socket)ar.AsyncState;
        try
        {
            int nBytesRec = sock.EndReceive(ar);
            if (nBytesRec > 0)
            {
                // Wrote the data to the List
                sRecieved = Encoding.UTF8.GetString(m_byBuff, 0, nBytesRec);
                if (RecieveDataEvent != null)
                {
                    RecieveDataEvent(this, new RecieveData() { Msg = sRecieved });
                }
                SetupRecieveCallback(sock);
            }
            else
            {
                // If no data was recieved then the connection is probably dead
                Debug.Log(string.Format("Client {0}, disconnected", sock.RemoteEndPoint));
                sock.Shutdown(SocketShutdown.Both);
                sock.Close();
                if (this.DisconnectEvent != null)
                {
                    this.DisconnectEvent(this, new EventArgs());
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.GetBaseException().ToString());
            SetupRecieveCallback(sock);
        }

    }
    ~alta_client()
    {
        if (m_sock != null && m_sock.Connected)
        {
            m_sock.Shutdown(SocketShutdown.Both);
            m_sock.Close();
        }
    }
}

