using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alta.INetwork
{
    [RequireComponent(typeof(NetworkView))]
    public abstract class NetworkBase : MonoBehaviour, ISimpleNetwork
    {
        public int port = 6789;
        protected NetworkView networkViewBase;
        public bool status;

        public event EventHandler<StringNetwork> OnReceiverStringEvent;
        public event EventHandler<DataNetwork> OnRecevicerDataEvent;

        public bool Connected
        {
            get
            {
                return Network.peerType != NetworkPeerType.Disconnected;
            }
        }

        public void Awake()
        {
            this.networkViewBase = GetComponent<NetworkView>();
        }



        public void Update()
        {
            this.status = Network.peerType != NetworkPeerType.Disconnected;
        }


        public virtual void DisConnect()
        {
            Network.Disconnect();
        }

        #region Send Message
        public void SendMsg(string msg, RPCMode Rpc, string methodName = "OnReceiverString")
        {
            this.networkViewBase.RPC(methodName, Rpc, msg);
        }

        public IEnumerator SendMsg(string msg, RPCMode Rpc, string methodName, float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            this.SendMsg(msg, Rpc, methodName);
        }

        public void SendObj<T>(T data, RPCMode Rpc, string methodName = "OnReceiverData")
        {
            PackageTranfer package = new PackageTranfer() { DataTranfer = data.SerializeObjectByte(), TYPE = data.GetType().ToString() };
            this.networkViewBase.RPC(methodName, Rpc, package.SerializeObjectByte());
        }

        #endregion

        #region OnReceiver

        [RPC]
        public void OnReceiverString(string msg)
        {
#if UNITY_EDITOR
            Debug.LogWarning("S->C: " + msg);
#endif
            if (OnReceiverStringEvent != null)
            {
                OnReceiverStringEvent(this, new StringNetwork() { MSG = msg });
            }
            transform.parent.SendMessage("OnReceiverString", msg);

        }

        [RPC]
        public void OnReceiverData(byte[] data)
        {
#if UNITY_EDITOR
            Debug.LogWarning("S->C: " + data.Length);
#endif
            PackageTranfer netData = data.DeserializeByte<PackageTranfer>();
            DataNetwork sender = new DataNetwork() { Data = netData };
            if (OnRecevicerDataEvent != null)
            {
                OnRecevicerDataEvent(this, sender);
            }
            else
            {
                transform.parent.SendMessage("OnReceiverData", sender);

            }
        }


        [RPC]
        public void OnReceiverData(byte[] data, NetworkMessageInfo info)
        {
#if UNITY_EDITOR
            Debug.LogWarning("S->C: " + data.Length);
#endif
            PackageTranfer netData = data.DeserializeByte<PackageTranfer>();
            DataNetwork sender = new DataNetwork() { Data = netData, Info = info.sender };
            if (OnRecevicerDataEvent != null)
            {
                OnRecevicerDataEvent(this, sender);
            }
            else
            {
                transform.parent.SendMessage("OnReceiverData", sender);

            }
        }

        [RPC]
        public void OnReceiverString(string msg, NetworkMessageInfo info)
        {
#if UNITY_EDITOR
            Debug.LogWarning("S->C: " + msg);
#endif
            if (OnReceiverStringEvent != null)
            {
                OnReceiverStringEvent(this, new StringNetwork() { MSG = msg, Info = info.sender });
            }
            else
            {
                transform.parent.SendMessage("OnReceiverString", msg);
            }
        }

        public void OnReceiverString(string msg, Client c)
        {
#if UNITY_EDITOR
            Debug.LogWarning("C->S: " + msg);
#endif
            if (OnReceiverStringEvent != null)
            {
                OnReceiverStringEvent(this, new StringNetwork() { MSG = msg, Info = c.player, type = c.Type });
            }

        }
        #endregion


    }
    public class NetworkEvent : EventArgs
    {
        public NetworkPlayer Info { get; set; }
        public TypeClient type { get; set; }
    }

    public class StringNetwork : NetworkEvent
    {
        public string MSG { get; set; }
    }

    public class DataNetwork : NetworkEvent
    {
        public PackageTranfer Data;
    }

}
