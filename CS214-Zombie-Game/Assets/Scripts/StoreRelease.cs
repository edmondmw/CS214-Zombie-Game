using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRelease : MonoBehaviour {

	public GameObject zombiePrefab;
	GameObject zb;
    public int currentHealth;
	// Use this for initialization
	public void Store(GameObject zombie) {
		zb = zombie;
        transform.position = zb.transform.position;
        transform.rotation = zb.transform.rotation;
        currentHealth = zb.GetComponent<ZombieHealth> ().currentHealth;
		zb.SetActive (false);
        gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	public void Release () {
		if (zb == null) {
			//zb = Instantiate (zombiePrefab, transform.position, transform.rotation);
            zb = ObjectPooler.SharedInstance.GetPooledObject("Enemy"); 
            if (zb != null) {
                zb.transform.position = transform.position;
                zb.transform.rotation = transform.rotation;
                zb.GetComponent<ZombieHealth> ().currentHealth = currentHealth;
            }
		}
		zb.SetActive (true);
        gameObject.SetActive (false);
	}
}
