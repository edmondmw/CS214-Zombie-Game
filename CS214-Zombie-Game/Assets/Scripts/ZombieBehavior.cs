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
	private Animator anim;
	//private Rigidbody rb;
	private ZombieHealth health;
	private int attackHash = Animator.StringToHash ("isAttack");
	private bool isAttacking;


	// Use this for initialization
	void Start ()
	{
		//rb=GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		health = GetComponent<ZombieHealth> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (health.currentHealth > 0) {
			playerPosition = player.transform.position;
			distanceFromPlayer = Vector3.Distance (playerPosition, transform.position);

			if (distanceFromPlayer <= detectableRange) {
				Search (playerPosition);
				if (distanceFromPlayer > attackDistance && !isAttacking) {
					Move ();
				}

				if (distanceFromPlayer <= attackDistance) {
					Attack (true);
				}
				if (isAttacking && distanceFromPlayer > maxAttackDistance) {
					Attack (false);
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

	void Attack (bool isAttack)
	{
		anim.SetBool (attackHash, isAttack);
		isAttacking = isAttack;
	}




}