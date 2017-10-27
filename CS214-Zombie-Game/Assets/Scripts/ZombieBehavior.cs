using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour {

    public GameObject player;
    Vector3 playerPosition;
    public float moveSpeed=0.1f;
    public float detectableRange=50f;
    public float attackDistance=4f;
    public float maxAttackDistance = 5f;
    public float turnSpeed = 0.01f;
    public int hp=100;
    public float disappearTime=0;

    private float distanceFromPlayer;
    private Animator anim;
    private Rigidbody rb;
    private int attackHash = Animator.StringToHash("isAttack");
    private bool isDead;

	// Use this for initialization
	void Start () {
        //rb=GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead) {
            if (hp <= 0)
            {
                isDead = true;
                Dead();
            }
            else
            {
                playerPosition = player.transform.position;
                distanceFromPlayer = Vector3.Distance(playerPosition, transform.position);

                if (distanceFromPlayer <= detectableRange)
                {
                    Search(playerPosition);


                    if (distanceFromPlayer <= attackDistance)
                    {
                        Attack(true);
                    }
                    else if (distanceFromPlayer > maxAttackDistance)
                    {
                        Attack(false);
                        Move();
                    }
                }
            }


        }
    }

    void Search(Vector3 objectPosition)
    {
        Vector3 direction = playerPosition - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed);   
    }

    void Move()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed));
    }

    void Attack(bool isAttack)
    {
        anim.SetBool(attackHash,isAttack);
     }

    //Play a random dead animation and destroy the zombie after disappearTime.
    void Dead()
    {
        int deadStyle = Random.Range(0, 3);

        switch (deadStyle)
        {
            case 0:
                anim.Play("left_fall");
                break;
            case 1:
                anim.Play("right_fall");
                break;
            default:
                anim.Play("back_fall");
                break;

        }
        Destroy(gameObject, disappearTime);
        
    }
}
