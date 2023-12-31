using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DataSynchronizer : MonoBehaviourPun
{
    private int synchronizedValue;
    public event System.Action<int> OnSynchronizedValueUpdated;

    private void Start()
    {
        if (photonView.IsMine)
        {
            synchronizedValue = 0;
            photonView.RPC("InitializeSynchronizedValue", RpcTarget.AllBuffered, synchronizedValue);
        }
    }

    [PunRPC]
    private void InitializeSynchronizedValue(int initialValue)
    {
        synchronizedValue = initialValue;
        OnSynchronizedValueUpdated?.Invoke(synchronizedValue);
    }

    [PunRPC]
    private void UpdateSynchronizedValue(int newValue)
    {
        synchronizedValue = newValue;
        OnSynchronizedValueUpdated?.Invoke(synchronizedValue);
    }
    
    public void ModifySynchronizedValue(int amount)
    {
        if (photonView.IsMine)
        {
            synchronizedValue += amount;
            photonView.RPC("UpdateSynchronizedValue", RpcTarget.AllBuffered, synchronizedValue);
        }
    }
}
