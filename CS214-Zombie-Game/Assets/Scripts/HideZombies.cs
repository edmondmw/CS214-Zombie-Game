using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideZombies : MonoBehaviour {

	public GameObject zombiePointPrefab;
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
            //zombiePoint = (GameObject)Instantiate (zombiePointPrefab, c.transform.position,c.transform.rotation);

			zombiePoint = ObjectPooler.SharedInstance.GetPooledObject ("Box");
			zombiePoint.GetComponent <StoreRelease> ().Store (c.gameObject);
		}
				
	}



}
