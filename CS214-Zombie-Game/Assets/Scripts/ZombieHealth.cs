using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{

	public int startingHealth = 100;
	public int currentHealth;
	public float disappearTime = 10;

	private Animator anim;
	private bool isChangeColor;
	private bool isDead;

    // Sound
    public Sound beingSlashed;

	public void Awake()
	{
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
        beingSlashed.source.Play();
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			isDead = true;
			Death();
		}
	}
	/*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            currentHealth -= collision.gameObject.GetComponent<PlayerController>().damage;

            //Color turn red when get hits.
            StartCoroutine(HitColor());
        }
    }

    IEnumerator HitColor()
    {
        if (isChangeColor)
        {
            yield break;
        }
        isChangeColor = true;

        Color originColor =GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = originColor;
        isChangeColor = false;

    }
    */
}

