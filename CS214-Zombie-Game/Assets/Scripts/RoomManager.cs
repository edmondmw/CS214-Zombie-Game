﻿using System.Collections;
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
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject aPlayer = PhotonNetwork.Instantiate(player.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        //aPlayer.GetComponent<PlayerController>().enabled = true;
        //aPlayer.transform.Find("MainCamera").gameObject.SetActive(true);
    }
}
