using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{

	public GameObject player;
	Vector3 playerPosition;
	public float moveSpeed = 0.1f;
	public float detectableRange = 50f;
	public float attackDistance = 2f;
	public float maxAttackDistance = 2.5f;
	public float turnSpeed = 0.01f;

	private float distanceFromPlayer;
	//private Rigidbody rb;
	private ZombieHealth health;
	private ZombieAttack attack;




	// Use this for initialization
	void Start ()
	{

		health = GetComponent<ZombieHealth> ();
		attack = GetComponentInChildren<ZombieAttack> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (health.currentHealth > 0) {
			playerPosition = player.transform.position;
			distanceFromPlayer = Vector3.Distance (playerPosition, transform.position);
			bool isAttacking = attack.isAttacking;
			if (distanceFromPlayer <= detectableRange) {
				Search (playerPosition);
				if (distanceFromPlayer > attackDistance && !isAttacking) {
					Move ();
				}

				if (distanceFromPlayer <= attackDistance) {
					attack.Attack (true);
				}
				if (isAttacking && distanceFromPlayer > maxAttackDistance) {
					attack.Attack (false);
					Move ();
				}
			}



		}
	}

	void Search (Vector3 objectPosition)
	{
		Vector3 direction = playerPosition - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (direction), turnSpeed);   
	}

	void Move ()
	{
		transform.Translate (new Vector3 (0, 0, moveSpeed));
	}





}