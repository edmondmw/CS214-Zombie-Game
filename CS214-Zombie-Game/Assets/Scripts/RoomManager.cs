using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Photon.MonoBehaviour {

    public string versionNum = "0.1";
    public string roomName = "roomA";
    public bool isConnected = false; 
    public Transform spawnPoint;
    public GameObject player;
    

	// Use this for initialization
	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings(versionNum);
        Debug.Log("Starting conn");
	}
	
	// Update is called once per frame
	void Update ()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
	}
}
