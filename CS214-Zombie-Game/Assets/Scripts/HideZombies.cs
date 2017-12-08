using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideZombies : MonoBehaviour {

	public GameObject box;
	private GameObject zombiePoint;
	void OnTriggerEnter(Collider c)
	{
		if(c.CompareTag("Box"))
		{
			c.GetComponent <StoreRelease>().Release ();
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (c.CompareTag ("Enemy")) {
			zombiePoint = (GameObject)Instantiate (box, c.transform.position,c.transform.rotation);
			zombiePoint.GetComponent <StoreRelease> ().Store (c.gameObject);
		}
				
	}



}
