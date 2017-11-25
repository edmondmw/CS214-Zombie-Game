﻿using System.Collections;
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
    private GameObject target;


    // Use this for initialization
    void Awake ()
    {
        health = GetComponent<ZombieHealth> ();
        attack = GetComponentInChildren<ZombieAttack> ();
        ResetPlayerList ();
        nma = GetComponent <NavMeshAgent> ();
    }
	
    // Update is called once per frame
    void Update ()
    {





        if (playerNumber>0&&health.currentHealth > 0) {
            targetNumber = 0;
            if (players [targetNumber] != null) {
                minDistance = Vector3.Distance (players [0].transform.position, transform.position);
			
                if (playerNumber > 1) {
                    for (int i = 1; i < playerNumber; i++) {
                        distance = Vector3.Distance (players [i].transform.position, transform.position);
                        if (distance < minDistance) {
                            minDistance = distance;
                            targetNumber = i;
                        }
                    }
                } 

                Debug.Log (minDistance);
                if (minDistance <= detectableRange) {
                    if  (minDistance <=maxAttackDistance)
                    {
                        Vector3 direction = players [targetNumber].transform.position;
                        direction.y = transform.position.y;
                        transform.LookAt (direction);


                        
                    if(minDistance <= nma.stoppingDistance)
                        {attack.Attack (true);}
                           
                    }

                    if (minDistance > maxAttackDistance) {
                        attack.Attack (false);
                        nma.destination=players [targetNumber].transform.position;
                    }

                }
            } else {
                ResetPlayerList ();
            }


        }else{
            nma.Stop ();
        }
    }
    void ResetPlayerList()
    {
        players = GameObject.FindGameObjectsWithTag ("Player");
        playerNumber = players.Length;
    }


}
