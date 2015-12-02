using UnityEngine;
public interface NetworkInterFace
{
    [RPC]
    void OnReceiverString(string msg, NetworkMessageInfo info);
    [RPC]
    void OnReceicerObject(NetworkMessageInfo info, object obj);
}
