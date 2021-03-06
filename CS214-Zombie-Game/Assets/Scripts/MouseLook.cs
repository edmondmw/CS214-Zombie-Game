﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class MouseLook : MonoBehaviour {

    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    Vector2 mouseLook;
    Vector2 smoothVector;
    GameObject character;
    NetworkIdentity netID;

	// Use this for initialization
	void Start ()
    {
        character = this.transform.parent.gameObject;
       // netID = GetComponentInParent<NetworkIdentity>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       // if (netID != null && !netID.isLocalPlayer)
         //   return;

        Vector2 mouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        smoothVector.x = Mathf.Lerp(smoothVector.x, mouseDir.x, 1f / smoothing);
        smoothVector.y = Mathf.Lerp(smoothVector.y, mouseDir.y, 1f / smoothing);
        mouseLook += smoothVector;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
	}
}
