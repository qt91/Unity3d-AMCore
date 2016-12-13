using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alta.INetwork
{
    [Serializable]
    public class PackageTranfer
    {
        public string TYPE;
        public byte[] DataTranfer;

        public T to<T>()
        {
            if (typeof(T).ToString() == TYPE)
            {
                object tmp = FunctionExtention.DeserializeByte(DataTranfer);
                if (tmp is T)
                {
                    return (T)FunctionExtention.DeserializeByte(DataTranfer);
                }
               
            }
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
    public class PackageCache: PackageTranfer
    {
        public TypeClient sendTo;
        public string Method;
    }
}
