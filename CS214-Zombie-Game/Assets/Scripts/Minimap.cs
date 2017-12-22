using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {
    // Doesn't do much(I mean no difference with/without script)
    public Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = player.position.y + 20;
        transform.position = newPosition;
    }
}
