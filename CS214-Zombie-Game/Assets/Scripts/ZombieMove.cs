using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMove : MonoBehaviour
{

    public GameObject[] players;
    public Sound footStep;
    public Sound breath;
    public Sound attack1;
    public Sound attack2;
    public float attackRate;
    public float notActiveDistance = 80f;
    public float detectableRange = 20f;
    public float maxAttackDistance = 4.5f;
    public float distanceOnStair = 10f;
    public float distanceOnGround = 4.5f;
    public float stopDistanceOnStair = 9f;
    public float stopDistanceOnGround;
    [HideInInspector]public bool isHit;
    [HideInInspector]public Vector3 hitPosition;
    public int damage = 10;
    public float idleSpeed = 0.05f;
    public float wanderingChangeTime = 10f;

    
    private Vector3 position;
    private Vector3 groundCheck;
    private bool isOnStair;
    private bool isOnGround = true;
    private NavMeshAgent nma;
    private float distance;
    private float nextAttackSound;
    private ZombieHealth health;
    private ZombieAttack attack;
    private float minDistance;
    private int playerNumber;
    private int targetNumber;
    private GameObject target;
    private int attackHash = Animator.StringToHash ("isAttack");
    private int attackMode = 1;
    // Used to keep track of what attack
    private Animator anim;
    private static float t = 0f;
    private float timer;
    private Vector3 targetPosition;


    // Use this for initialization
    void Awake ()
    {
        health = GetComponent<ZombieHealth> ();
        GetComponentInChildren<ZombieAttack> ().damage = damage;
        GetPlayers ();
        nma = GetComponent <NavMeshAgent> ();
        anim = GetComponentInParent <Animator> ();
        stopDistanceOnGround = nma.stoppingDistance;
    }

    void Start ()
    {
        StartCoroutine (BreathSound ());
    }
    // Update is called once per frame
    void Update ()
    {
        position = transform.position;
        if (playerNumber > 0 && health.currentHealth > 0) {
            targetNumber = 0;
            if (players [targetNumber] != null) {
                minDistance = Vector3.Distance (players [0].transform.position, position);

                if (playerNumber > 1) {
                    for (int i = 1; i < playerNumber; i++) {
                        distance = Vector3.Distance (players [i].transform.position, position);
                        if (distance < minDistance) {
                            minDistance = distance;
                            targetNumber = i;
                        }
                    }
                }
                Debug.Log (minDistance);
                if (isHit) {

                    transform.position = new Vector3 (Mathf.Lerp (position.x, hitPosition.x, t), position.y, Mathf.Lerp (position.z, hitPosition.z, t));
                    t += 0.5f * Time.deltaTime;
                    if (t > 1.0f) {

                        isHit = false;
                        t = 0f;
                    }
                } else {
                    //Go and attack if in detectableRange
                    if (minDistance <= detectableRange) {
                        if (minDistance <= maxAttackDistance) {
                            Vector3 direction = players [targetNumber].transform.position;
                            direction.y = position.y;
                            transform.LookAt (direction);

                            Attack ();


                        }

                        if (minDistance > maxAttackDistance) {
                            anim.SetBool (attackHash, false);
                        }

                        nma.destination = players [targetNumber].transform.position;
                        if (!footStep.source.isPlaying) {
                            footStep.source.Play ();

                        }

                    } else {
                        //Just simple move.
                        transform.Translate (Vector3.forward * idleSpeed);
                        if (timer >= wanderingChangeTime) {
                            RandomDestination ();
                            timer = 0;
                        } else {
                            timer += Time.deltaTime;
                        }
                    }

                    // If player isn't in range, then move randomly. Only want to set new destination when the zombie has stopped
                    /*else if(nma.velocity.magnitude == 0){
                        float radius = 50f;
                        Vector3 point = transform.position + Random.insideUnitSphere * radius;
                        NavMeshHit nh;
                        NavMesh.SamplePosition(point, out nh, radius, NavMesh.AllAreas);
                        nma.destination = nh.position;
                    } */   

                    //Check it is on Ground or on Stairs
                    groundCheck = position + new Vector3 (0, -0.2f, 0); 
                    isOnStair = Physics.Linecast (position, groundCheck, 1 << LayerMask.NameToLayer ("Stair"));
                    if (isOnGround == isOnStair) {
                        ChangeDistance ();
                        isOnGround = !isOnGround;
                    }
                } 


            } else {
                GetPlayers ();
            } 
        } else {
            nma.Stop ();
        }
    }

    private void GetPlayers()
    {
        players = PlayerList.GetPlayers ();

        playerNumber = players.Length;
    }
        
        

    void Attack ()
    {
        anim.SetBool (attackHash, true);
        TimedAttack ();
    }

    void ChangeDistance ()
    {
        
        if (isOnStair) {
            nma.stoppingDistance = stopDistanceOnStair;
            maxAttackDistance = distanceOnStair;
        } else {
            nma.stoppingDistance = stopDistanceOnGround;
            maxAttackDistance = distanceOnGround;
        }
    }

    void RandomDestination ()
    {
        targetPosition = transform.position + new Vector3 (Random.Range (-1f, 1f), 0, Random.Range (-1f, 1f));
        transform.LookAt (targetPosition);

    }

    IEnumerator BreathSound ()
    {
        while (health.currentHealth > 0) {
            yield return new WaitForSeconds (10);
            if (health.currentHealth > 0) {
                breath.source.Play ();
            }
        }
    }

    void TimedAttack ()
    {
        if (Time.fixedTime > nextAttackSound) {
            AttackSound ();
            nextAttackSound = Time.fixedTime + attackRate;
        }
    }

    private void AttackSound ()
    {
        // Alternate attack
        if (breath.source.isPlaying) {
            breath.source.Stop ();
        }
        if (attackMode == 1) {
            if (!attack1.source.isPlaying || !attack2.source.isPlaying) {
                attack1.source.Play ();
                attackMode = 2;
            }
        } else if (attackMode == 2) {
            if (!attack1.source.isPlaying || !attack2.source.isPlaying) {
                attack2.source.Play ();
                attackMode = 1;
            }
        } else {
            Debug.Log ("Error, please assign attackMode to either 1 or 2");
        }
    }

}
