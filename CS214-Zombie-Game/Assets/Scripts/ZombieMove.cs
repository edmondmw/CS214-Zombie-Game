using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour
{

	public GameObject[] players;
	public float detectableRange = 50f;
	public float maxAttackDistance = 2f;

	private NavMeshAgent nma;
	private float distance;
	private ZombieHealth health;
	private ZombieAttack attack;
	private float minDistance;
	private bool isMultiPlayer;
	private int playerNumber;
	private int targetNumber;

	// Use this for initialization
	void Start ()
	{
		health = GetComponent<ZombieHealth> ();
		attack = GetComponentInChildren<ZombieAttack> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
		playerNumber = players.Length;
		nma = GetComponent <NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (health.currentHealth > 0) {
			minDistance = Vector3.Distance (players [0].transform.position, transform.position);
			targetNumber = 0;
			if (playerNumber > 1) {
				for (int i = 1; i < playerNumber; i++) {
					distance = Vector3.Distance (players [i].transform.position, transform.position);
					if (distance < minDistance) {
						minDistance = distance;
						targetNumber = i;
					}
				}
			} 
			bool isAttacking = attack.isAttacking;
			if (minDistance <= detectableRange) {
				if (minDistance <= nma.stoppingDistance) {
					Vector3 direction = players [targetNumber].transform.position;
					direction.y = transform.position.y;
					transform.LookAt(direction);
					attack.Attack (true);
				}
				if (isAttacking && minDistance > maxAttackDistance) {
					attack.Attack (false);

				}
				nma.SetDestination (players [targetNumber].transform.position);	
			}


		}
	}


}
