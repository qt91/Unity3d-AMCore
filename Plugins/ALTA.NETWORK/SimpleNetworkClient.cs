using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Alta.INetwork
{
    public class SimpleNetworkClient : NetworkBase
    {
        public string IP = "127.0.0.1";
        public bool ReConnect = false;
        public bool StartConnect = false;
        public float TimeDelayAutoConnect = 3.0f;
        public string DEFINE_CLIENT = "DISPLAY";
        private float _timeReConnect = 0f;
        private bool _manualConnect = false;
        private bool _flagSendDefine = false;


        protected virtual void Start()
        {
            if (this.StartConnect)
            {
                this.Connect();
            }
        }
       
        public void Connect()
        {
            if (!this.status)
            {
                _manualConnect = true;
                _flagSendDefine = false;
                Network.Connect(this.IP, this.port);
            }
        }

        void Update()
        {
            base.Update();
            if (!this.Connected && this._manualConnect)
            {
                this._timeReConnect += Time.deltaTime;
                if (this._timeReConnect >= TimeDelayAutoConnect)
                {
                    this._timeReConnect = 0f;
                    this.Connect();
                }
            }
            else if (this.Connected && !this._flagSendDefine)
            {
                this.SendMsg(this.DEFINE_CLIENT, RPCMode.Server);
                this._flagSendDefine = true;
            }

        }

        #region Send Message
        public void SendMsg(string msg, string methodName = "OnReceiverString")
        {
#if UNITY_EDITOR
            Debug.LogWarning("C->S: " + msg);
#endif
            networkViewBase.RPC(methodName, RPCMode.Server, msg);
        }

        public IEnumerator SendMsg(string msg, string methodName, float timeDelay)
        {
            yield return new WaitForSeconds(timeDelay);
            this.SendMsg(msg, RPCMode.Server, methodName);
        }

        public void SendObj<T>(T data, string methodName = "OnReceiverData")
        {
            PackageTranfer package = new PackageTranfer() { DataTranfer = data.SerializeObjectByte(), TYPE = data.GetType().ToString() };
            this.networkViewBase.RPC(methodName, RPCMode.Server, package.SerializeObjectByte());
        }
        #endregion    
    }
}
