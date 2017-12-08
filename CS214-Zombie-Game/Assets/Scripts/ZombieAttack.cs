using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {
	public int damage;


	void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Player"))
        {
            if (PhotonNetwork.connected)
            {
                other.transform.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, damage);
            }
            else
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }
        }

	}
}
