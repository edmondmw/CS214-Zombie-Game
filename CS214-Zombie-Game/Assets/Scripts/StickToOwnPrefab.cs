using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToOwnPrefab : MonoBehaviour {

    public GameObject player;

	private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
