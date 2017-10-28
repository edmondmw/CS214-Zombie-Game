using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpForce = 10f;
    public int damage = 20;
    public float hitRange = 1.5f;
    public float attackDelay = 1f;

    float nextAttack;
    Rigidbody rb;
    bool grounded = true;
    bool alternateSwingAnim = true;
    Animator anim;
    
	void Start ()
    {
        // Makes cursor disappear
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        anim = transform.Find("MainCamera").Find("Arms").GetComponent<Animator>();
        nextAttack = Time.time;
	}

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    void Update ()
    {
        // For movement
        float vertical = Input.GetAxis("Vertical") * speed;
        float horizontal = Input.GetAxis("Horizontal") * speed;
        vertical *= Time.deltaTime;
        horizontal *= Time.deltaTime;

        transform.Translate(horizontal, 0, vertical);

        // Attacking
        if(Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= nextAttack)
            {
                nextAttack = Time.time + attackDelay;
                StartCoroutine(Attack());
            }
        }
  
        // Unlock the cursor when esc is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    IEnumerator Attack()
    {
        if (alternateSwingAnim)
        {
            anim.SetTrigger("Swing01");
        }
        else
        {
            anim.SetTrigger("Swing02");
        }
        alternateSwingAnim = !alternateSwingAnim;
        
        // Don't want to apply damage until after animation has played a bit
        yield return new WaitForSeconds(attackDelay);

        RaycastHit hit;
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, hitRange))
        {
            if(hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Health>().TakeDamage(damage);
                Debug.Log(hit.transform.GetComponent<Health>().currentHealth);
            }
        }

    }
}
