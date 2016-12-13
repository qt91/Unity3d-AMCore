using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Alta.Plugin;
using UnityEngine.UI;

namespace Alta.INetwork
{

    [RequireComponent(typeof(NetworkView))]
    public class SimpleNetworkSever : NetworkBase
    {
        private new NetworkView networkView;
        public bool autoRun;
        public bool canRun = true;
        public List<Client> m_aryClients;
        public Text textLog;

        internal bool hasType(TypeClient type)
        {
            if (this.m_aryClients == null || this.m_aryClients.Count == 0)
                return false;
            foreach (Client c in this.m_aryClients)
            {
                if (c.Type == type)
                    return true;

            }
            return false;
        }

        void Awake()
        {
            m_aryClients = new List<Client>();
            networkView = GetComponent<NetworkView>();
        }
        // Use this for initialization
        void Start()
        {
            if (this.autoRun)
            {
                this.InitializeServer();
            }
        }
        public void InitializeServer()
        {
            this.canRun = true;
            if (Network.peerType == NetworkPeerType.Disconnected)
            {
                Network.InitializeServer(100, port, false);
                textLog.text += "Server đang chạy..." + Environment.NewLine;
            }

        }


        public void SendTo(string msg)
        {
            Debug.LogWarning("S->C: " + msg);
            if (this.m_aryClients.Count > 0)
            {
                networkView.RPC("OnReceiverString", RPCMode.Others, msg);
            }
        }

        /// <summary>
        /// gui Msg toi mot may
        /// </summary>
        /// <param name="msg"> Du lieu dang string</param>
        /// <param name="player">dia chi ip string</param>
        /// 

        public void SendTo(string msg, NetworkPlayer player)
        {
            if (this.m_aryClients.Count > 0)
            {
                foreach (Client c in this.m_aryClients)
                {
                    if (c.player == player)
                    {
                        networkView.RPC("OnReceiverString", c.player, msg);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// gui du lieu xuong mot ip bi tre mot thoi gian
        /// </summary>
        /// <param name="msg">du lieu dang string</param>
        /// <param name="ip">dia chi ip</param>
        /// <param name="timeDelay">thoi gian tre</param>
        /// <returns></returns>

        public IEnumerator SendTo(string msg, NetworkPlayer player, float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            this.SendTo(msg, player);
        }

        /// <summary>
        /// Gui du lieu xuong mot nhom may
        /// Bao gom nhieu may khac nhau
        /// </summary>
        /// <param name="msg">du lieu</param>
        /// <param name="Type">Loai may</param>
        /// 

        public void SendTo(string msg, TypeClient Type)
        {
            if (this.m_aryClients.Count > 0)
            {
                foreach (Client c in this.m_aryClients)
                {
                    if (c.Type == Type)
                    {
                        this.networkView.RPC("OnReceiverString", c.player, msg);
                    }
                }
            }
        }

        internal string getInfo(TypeClient type)
        {
            if (this.m_aryClients == null)
                return string.Empty;
            foreach (Client c in this.m_aryClients)
            {
                if (c.Type == type)
                    return c.player.ipAddress;

            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="type"></param>

        public void SendObj<T>(T data, TypeClient type)
        {
            foreach (Client c in this.m_aryClients)
            {
                if (c.Type == type)
                {
                   
                    PackageTranfer pdata = new PackageTranfer();
                    pdata.TYPE = data.GetType().ToString();
                    pdata.DataTranfer = data.SerializeObjectByte();
                    Debug.LogWarning("S->C: "+ pdata.DataTranfer.Length);
                    this.networkView.RPC("OnReceiverData", c.player, pdata.SerializeObjectByte());
                }
            }
        }
        public void SendObj<T>(T data, Client client)
        {
            PackageTranfer pdata = new PackageTranfer();
            pdata.TYPE = data.GetType().ToString();
            pdata.DataTranfer = data.SerializeObjectByte();
            Debug.LogWarning("S->C: " + pdata.DataTranfer.Length);
            this.networkView.RPC("OnReceiverData", client.player, pdata.SerializeObjectByte());
        }

        public IEnumerator SendObj<T>(T data, Client client,float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            this.SendObj(data, client);
        }

        public void SendObj<T>(T data)
        {
            PackageTranfer pdata = new PackageTranfer();
            pdata.TYPE = data.GetType().ToString();
            pdata.DataTranfer = data.SerializeObjectByte();
            Debug.LogWarning("S->C: " + pdata.DataTranfer.Length);
            this.networkView.RPC("OnReceiverData", RPCMode.Others, pdata.SerializeObjectByte());
        }


        /// <summary>
        /// Gui du lieu xuong mot nhom may thoi gian tre
        /// </summary>
        /// <param name="msg">du lieu</param>
        /// <param name="Type">laoi may</param>
        /// <param name="TimeDelay">Thoi gian tre</param>
        /// <returns></returns>
        /// 

        public IEnumerator SendTo(string msg, TypeClient Type, float TimeDelay)
        {
            yield return new WaitForSeconds(TimeDelay);
            this.SendTo(msg, Type);
        }
        [RPC]
        public void OnReceiverVector(Vector2 v)
        {
#if UNITY_EDITOR
            Debug.LogWarning(v);
#endif
        }

        [RPC]
        public void OnReceiverString(string msg, NetworkMessageInfo info)
        {
#if UNITY_EDITOR
            Debug.LogWarning("C->S: " + msg);
#endif
            string[] cmds = msg.Split('_', '|');
            if (cmds.Length > 0)
            {
                if (this.m_aryClients.Count > 0)
                {
                    Client client = new Client();
                    client.player = info.sender;
                    TypeClient type = cmds[0].ToUpper().ToEnum<TypeClient>(TypeClient.None);
                    foreach (Client c in this.m_aryClients)
                    {
                        if (c.player == info.sender)
                        {
                            if (type != TypeClient.None)
                            {
                                c.Type = type;
                                transform.parent.SendMessage("OnClientConnected", c, SendMessageOptions.DontRequireReceiver);
                            }
                            client = c;                            
                            break;
                        }
                    }
                }
                base.OnReceiverString(msg, info);
            }
        }


        void OnPlayerConnected(NetworkPlayer player)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Client Connect: " + player.ipAddress);
#endif
            m_aryClients.Add(new Client() { player = player });
        }

        void OnPlayerDisconnected(NetworkPlayer player)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Client Disconnect: " + player.ipAddress);
#endif
            if (this.m_aryClients != null && this.m_aryClients.Count > 0)
            {
                foreach (Client c in this.m_aryClients)
                {
                    if (c.player == player)
                    {
                        this.m_aryClients.Remove(c);
                        return;
                    }
                }
            }

        }
    }
}
