using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Alta.INetwork
{
    public class Client:IClient
    {
        public string Code
        {
            get
            {
                return this.player.guid;
            }
        }
        public NetworkPlayer player
        {
            get;
            set;
        }

        public TypeClient Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        private TypeClient type = TypeClient.None;
    }
}
