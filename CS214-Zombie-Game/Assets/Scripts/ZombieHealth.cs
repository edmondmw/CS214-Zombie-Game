using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{

	public int startingHealth = 100;
	public int currentHealth;
	public float disappearTime = 10;
    public float hitBackward=5f;

    public int id;

	private Animator anim;
	private ZombieMove zm;

    // Sound
    public Sound beingSlashed;

	public void Awake()
	{
		anim = GetComponent<Animator>();
		currentHealth = startingHealth;
		zm = GetComponent <ZombieMove> ();
	}

	//Play a random dead animation and destroy the zombie after disappearTime.
	void Death()
	{
        Spawner.numZombies--;

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

        if (PhotonNetwork.connected && PhotonNetwork.isMasterClient)
        {
            // TODO: add delay here
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, disappearTime);
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        GameObject obj=GameObject.FindGameObjectWithTag ("Player");
        TakeDamage (damage,obj);
    }


    
    //The zombie moves against the direction of the object ob hit it.
    public void TakeDamage(int damage,GameObject ob)
	{
        beingSlashed.source.Play();
		currentHealth -= damage;
		if (currentHealth <= 0) {
			Death ();
		} else {
			zm.hitPosition=transform.position+(transform.position - ob.transform.position).normalized*hitBackward;
			zm.isHit = true;

		}
	}




    
}

