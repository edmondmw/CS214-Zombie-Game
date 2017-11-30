using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{

	public int startingHealth = 100;
	public int currentHealth;
	public float disappearTime = 10;


	private Animator anim;
    private ZombieMove move;

	public void Awake()
	{
		anim = GetComponent<Animator>();
		currentHealth = startingHealth;
        move = GetComponent <ZombieMove> ();
	}

	//Play a random dead animation and destroy the zombie after disappearTime.
	void Death()
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
    public void TakeDamage(int damage)
    {
        GameObject obj=GameObject.FindGameObjectWithTag ("Player");
        TakeDamage (damage,obj);
    }

    //The zombie moves against the direction of the object ob hit it.
    public void TakeDamage(int damage,GameObject ob)
	{
		currentHealth -= damage;
		if (currentHealth <= 0) {
			Death ();
		} else {
            move.isHit = true;
            move.BackwardByHit (ob);
		}
	}




    
}

