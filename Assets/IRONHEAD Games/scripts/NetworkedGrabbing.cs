using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    PhotonView m_photonView;


    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TransferOwnership()
    {
        m_photonView.RequestOwnership();
    }

    public void OnSelectEnter()
    {
        Debug.Log("Grabbed");
        if (m_photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Not requesting ownership. Already mine.");
            m_photonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);
        }
        else
        {
            TransferOwnership();

        }

    }

    public void OnSelectExit()
    {
        Debug.Log("Released");
        m_photonView.RPC("StopNetworkGrabbing",RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != m_photonView)
        {
            return;
        }

        Debug.Log("OnOwnerShip Requested for: "+ targetView.name+ " from "+ requestingPlayer.NickName);
        m_photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Transfer is complete. New owner: "+ targetView.Owner.NickName);

        if (photonView.IsMine)
        {
            m_photonView.RPC("StartNetworkGrabbing",RpcTarget.AllBuffered);
        }

    }

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        if (!photonView.IsMine)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;

        }
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        if (!photonView.IsMine)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;

        }
    }
}
