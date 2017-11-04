using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {
	public int damage;
	[HideInInspector]public bool isAttacking;
	private int attackHash = Animator.StringToHash ("isAttack");

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponentInParent <Animator> ();
	}

	void OnTriggerEnter(Collider other){

		if (isAttacking) {

			if(other.gameObject.CompareTag ("Body")) 
			{

				other.GetComponentInParent <Health>().TakeDamage (damage);
			}
		}
	}

	public void Attack (bool isAttack)
	{
		anim.SetBool (attackHash, isAttack);
		isAttacking = isAttack;
	}



}
