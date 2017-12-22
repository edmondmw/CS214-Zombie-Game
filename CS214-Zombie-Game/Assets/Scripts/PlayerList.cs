using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerList {

    private static GameObject[] players= GameObject.FindGameObjectsWithTag ("Player");

    public static GameObject[] GetPlayers()
    {
        
        return players;

    }

    public static void ResetPlayerList()
    {
        players = GameObject.FindGameObjectsWithTag ("Player");
    }


}
