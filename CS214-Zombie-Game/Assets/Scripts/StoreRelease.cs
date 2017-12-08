using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRelease : MonoBehaviour {

	public GameObject zombiePrefab;
	GameObject zb;
	// Use this for initialization
	public void Store(GameObject zombie) {
		zb = zombie;
		zombie.SetActive (false);
	}
	
	// Update is called once per frame
	public void Release () {
		if (zb == null) {
			zb = Instantiate (zombiePrefab, transform.position, transform.rotation);
		}
		zb.SetActive (true);
		Destroy (gameObject);
	}
}
