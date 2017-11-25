using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{

	public int startingHealth = 100;
	public int currentHealth;
	public float disappearTime = 10;
    public float hitBackward=5f;
    public float hitBackwardUp=10f;

	private Animator anim;
	private bool isChangeColor;
    private Rigidbody rb;


	public void Awake()
	{
        rb = GetComponent <Rigidbody> ();
		anim = GetComponent<Animator>();
		currentHealth = startingHealth;

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
            BackwardByHit (ob);
		}
	}

    private void BackwardByHit(GameObject ob)
    {
        rb.isKinematic = false;
        Vector3 direction = transform.position - ob.transform.position;
        direction.y = hitBackwardUp;
        rb.AddForce (direction.normalized*hitBackward,ForceMode.Impulse);
        rb.isKinematic = true;
    }


    
}

