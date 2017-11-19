using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 10f;
    public int damage = 20;
    public float hitRange = 1.5f;
    public float attackDelay = 1f;

    float nextAttack;
    Rigidbody rb;
    bool isSprinting = false;
    bool grounded = false;
    bool alternateSwingAnim = true;
    Animator anim;

    private bool isMoving;

    // Audio
    public Sound FootStepsSFX;
    public Sound SwingsSFX;


    void Start ()
    {
        // Makes cursor disappear
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        anim = transform.Find("MainCamera").Find("Arms").GetComponent<Animator>();
        nextAttack = Time.time;

        // Audio

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
        MoveHandler();
        CheckMovement();
        PlayMovementSound();

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

    void MoveHandler()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Sprinting. Only allow sprinting when going forward
        if (Input.GetKey(KeyCode.LeftShift) && vertical > 0 )
        {
            vertical *= sprintSpeed;
            anim.SetBool("IsRunning", true);
            isSprinting = true;
        }
        else
        {
            anim.SetBool("IsRunning", false);
            vertical *= walkSpeed;
            horizontal *= walkSpeed;
            isSprinting = false;
        }
           
        vertical *= Time.deltaTime;
        horizontal *= Time.deltaTime;

        transform.Translate(horizontal, 0, vertical);
    }


    IEnumerator Attack()
    {
        if (alternateSwingAnim)
        {
            anim.SetTrigger("Swing01");
            SwingSound();
        }
        else
        {
            anim.SetTrigger("Swing02");
            SwingSound();
        }
        alternateSwingAnim = !alternateSwingAnim;
        
        // Don't want to apply damage until after animation has played a bit
        yield return new WaitForSeconds(0.5f);

        RaycastHit hit;
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, hitRange))
        {
            if(hit.transform.CompareTag("Enemy"))
            {
				// TODO: Make health an abstract class and zombie health a child
                hit.transform.GetComponent<ZombieHealth>().TakeDamage(damage);
				Debug.Log(hit.transform.GetComponent<ZombieHealth>().currentHealth);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    private void PlayMovementSound()
    {
        if(isMoving)
        {
            if(isSprinting)
            {
                //playerSound.pitch = 2;
                FootStepsSFX.source.pitch = 2;
            }
            else
            {
                //playerSound.pitch = 1;
                FootStepsSFX.source.pitch = 1;
            }
            if(!FootStepsSFX.source.isPlaying)
                FootStepsSFX.source.Play();
        }
        else
        {
            FootStepsSFX.source.Stop();
        }
    }
    private void SwingSound()
    {
        SwingsSFX.source.Play();
    }
}
