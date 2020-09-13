using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region UI Callback MethodCollection.IsSynchronized 

    public void JoinRandomRoom()
    {
    	PhotonNetwork.JoinRandomRoom();
    }

    #endregion

    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    	Debug.Log(message);
    	CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {

    	Debug.Log("A room is ceated with the name: " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnJoinedRoom()
    {
   
    	Debug.Log("The Local player: " + PhotonNetwork.NickName + "Joined to " + PhotonNetwork.CurrentRoom.Name +"Player Count " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    	Debug.Log(newPlayer.NickName+ "joined "  + PhotonNetwork.CurrentRoom.Name + ". Player Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    #region Private Methods

    void CreateAndJoinRoom()
    {

    	string randomRoomName = "Room_" + Random.Range(0,10000);
    	RoomOptions roomOptions = new RoomOptions();
    	roomOptions.MaxPlayers = 20;
    	PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }

    #endregion

}
