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
        roomName = "Room " + Random.Range(0, 999);
        Debug.Log("Starting connection");
    }

    public void OnJoinedLobby()
    {
        Debug.Log("Connected");
        isConnected = true;
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        isConnected = false;
        GameObject aPlayer = PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        GameObject aZombie = PhotonNetwork.Instantiate(enemy.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        aZombie.GetComponent<ZombieBehavior>().player = aPlayer;
    }

    private void OnGUI()
    {
        if(isConnected)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));
            
            roomName = GUILayout.TextField(roomName);

            if(GUILayout.Button("Create Room"))
            {
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.IsVisible = true;
                roomOptions.MaxPlayers = 4;
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, null);
            }

            foreach(RoomInfo game in PhotonNetwork.GetRoomList())
            {
                if(GUILayout.Button(game.Name + ' ' + game.PlayerCount + '/' + game.MaxPlayers))
                {
                    RoomOptions roomOptions = new RoomOptions();
                    roomOptions.IsVisible = true;
                    roomOptions.MaxPlayers = 4;
                    PhotonNetwork.JoinOrCreateRoom(game.Name, roomOptions, null);
                }
            }

            GUILayout.EndArea();
        }
    }
}
