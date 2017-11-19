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

    public Sound footStep;
    public Sound breath;
    public Sound attack1;
    public Sound attack2;
    private int attackMode = 1; // Used to keep track of what attack
    public float attackRate;
    private float nextAttackSound;

	// Use this for initialization
	void Start ()
	{
		//rb=GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		health = GetComponent<ZombieHealth> ();
        footStep.source.pitch = moveSpeed * 8;

        // sound
        StartCoroutine(BreathSound());
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

            // sound

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
        if(!footStep.source.isPlaying)
            footStep.source.Play();
	}

	void Attack (bool isAttack)
	{
		anim.SetBool (attackHash, isAttack);
		isAttacking = isAttack;
        TimedAttack();
	}

    IEnumerator BreathSound()
    {
        while (health.currentHealth > 0)
        {
            yield return new WaitForSeconds(10);
            if (health.currentHealth > 0)
            {
                breath.source.Play();
            }
        }
    }
    void TimedAttack()
    {
        if(Time.fixedTime > nextAttackSound)
        {
            AttackSound();
            nextAttackSound = Time.fixedTime + attackRate;
        }
    }
    private void AttackSound()
    {
        // Alternate attack
        if(breath.source.isPlaying)
        {
            breath.source.Stop();
        }
        if(attackMode == 1)
        {
            if (!attack1.source.isPlaying || !attack2.source.isPlaying)
            {
                attack1.source.Play();
                attackMode = 2;
            }
        }
        else if(attackMode == 2)
        {
            if (!attack1.source.isPlaying || !attack2.source.isPlaying)
            {
                attack2.source.Play();
                attackMode = 1;
            }
        }
        else
        {
            Debug.Log("Error, please assign attackMode to either 1 or 2");
        }
    }


}