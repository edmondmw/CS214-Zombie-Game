using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour
{

    public GameObject[] players;
    public float detectableRange = 50f;
    public float maxAttackDistance = 4.5f;
    public float distanceOnStair=10f;
    public float distanceOnGround=4.5f;
    public float stopDistanceOnStair=9f;
    public float stopDistanceOnGround;
    [HideInInspector]public bool isHit;
    [HideInInspector]public Vector3 hitPosition;
    public int damage=10;


    private Vector3 position;
    private Vector3 groundCheck;
    private bool isOnStair;
    private bool isOnGround=true;
    private NavMeshAgent nma;
    private float distance;
    private ZombieHealth health;
    private ZombieAttack attack;
    private float minDistance;
    private int playerNumber;
    private int targetNumber;
    private GameObject target;
    private int attackHash = Animator.StringToHash ("isAttack");
    private Animator anim;	
    private static float t=0f;



    // Use this for initialization
    void Awake ()
    {
        health = GetComponent<ZombieHealth> ();
        GetComponentInChildren<ZombieAttack> ().damage = damage;
        ResetPlayerList ();
        nma = GetComponent <NavMeshAgent> ();
        anim = GetComponentInParent <Animator> ();
        stopDistanceOnGround = nma.stoppingDistance;

    }
	
    // Update is called once per frame
    void Update ()
    {
        position = transform.position;
        groundCheck=position+new Vector3(0,-0.2f,0); 
        isOnStair = Physics.Linecast (position, groundCheck, 1 << LayerMask.NameToLayer ("Stair"));
        if(isOnGround==isOnStair)
        {
            ChangeDistance ();
            isOnGround = !isOnGround;
        }
            

		if (isHit) {
			
			transform.position = new Vector3 (Mathf.Lerp (position.x, hitPosition.x, t), position.y, Mathf.Lerp (position.z, hitPosition.z, t));
			t += 0.5f * Time.deltaTime;
			if (t>1.0f) {

				isHit = false;
				t = 0f;
			}
		} else {
		
			if (playerNumber > 0 && health.currentHealth > 0) {
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

                    //Debug.Log (minDistance);
                    //Go and attack if in detectableRange
					if (minDistance <= detectableRange) {

						if (minDistance <= maxAttackDistance) {
							Vector3 direction = players [targetNumber].transform.position;
							direction.y = position.y;
							transform.LookAt (direction);

                                Attack ();


						}

						if (minDistance > maxAttackDistance) {
                            anim.SetBool (attackHash,false);
						}

						nma.destination = players [targetNumber].transform.position;

                    }
				} else {
					ResetPlayerList ();
				}


			} else {
				nma.Stop ();
			}
		}
    }
    void ResetPlayerList()
    {
        players = GameObject.FindGameObjectsWithTag ("Player");
        playerNumber = players.Length;
    }

    void Attack()
    {
        anim.SetBool (attackHash,true);

    }

    void ChangeDistance()
    {
        
        if(isOnStair){
            nma.stoppingDistance = stopDistanceOnStair;
            maxAttackDistance = distanceOnStair;
        }else{
            nma.stoppingDistance = stopDistanceOnGround;
            maxAttackDistance = distanceOnGround;
        }
    }

        

}
