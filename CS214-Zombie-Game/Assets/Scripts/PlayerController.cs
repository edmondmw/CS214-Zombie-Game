using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpForce = 10f;

    Rigidbody rb;
    bool grounded = true;
    Animator anim;

	void Start ()
    {
        // Makes cursor disappear
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        // TODO: temporary maybe move to another script
        anim = transform.Find("Camera").Find("Sword").GetComponent<Animator>();
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
        float vertical = Input.GetAxis("Vertical") * speed;
        float horizontal = Input.GetAxis("Horizontal") * speed;
        vertical *= Time.deltaTime;
        horizontal *= Time.deltaTime;

        transform.Translate(horizontal, 0, vertical);

        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
        }

        // Unlock the cursor when esc is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
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
}
