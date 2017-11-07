using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Photon.MonoBehaviour {

    public string versionNum = "0.1";
    public string roomName = "roomA";
    public bool isConnected = false; 
    public Transform spawnPoint;
    public GameObject player;
    public GameObject enemy;
    

	// Use this for initialization
	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings(versionNum);
        Debug.Log("Starting conn");
	}

    public void OnJoinedLobby()
    {
        Debug.Log("joined lobby");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
    }

    public void OnJoinedRoom()
    {
        Debug.Log("joined room");
        isConnected = true;
        GameObject aPlayer = PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        GameObject aZombie = PhotonNetwork.Instantiate(enemy.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        aZombie.GetComponent<ZombieBehavior>().player = aPlayer;
    }
}
