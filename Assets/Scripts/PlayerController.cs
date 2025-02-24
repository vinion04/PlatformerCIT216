using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //private
    private Animator animator;
    private Vector2 movementVector;
    private bool isGrounded = false;
    private bool jump = false;
    private float shootRate = 0.3f;
    private float nextShoot = 0;
    private bool facingRight = true;
    private AudioSource shootSound;

    //public
    public SpriteRenderer sprite_r;
    public Rigidbody2D body;
    public float speed = 3;
    public float jumpForce = 300;
    public float maxSpeed = 7f;
    public float gravityMultiplier = 3f;
    public GameObject shoot;    //for our bullets
    public Transform shootPoint;
    public GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_r = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        shootSound = GetComponent<AudioSource>();

    }
    void FixedUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(movementVector.x));
        if(movementVector.x > 0 && Mathf.Abs( body.velocity.x) < maxSpeed)
            body.velocity = new Vector2(movementVector.x * speed, body.velocity.y);
        else if (movementVector.x < 0 && Mathf.Abs(body.velocity.x) < maxSpeed)
            body.velocity = new Vector2(movementVector.x * speed, body.velocity.y);

        if(jump)
        {
            body.AddForce(Vector2.up * jumpForce);
            jump = false;
            isGrounded = false;
        }

        if(body.velocity.y < 0) //falling
        {
            body.gravityScale = gravityMultiplier;
        }
        else
        {
            body.gravityScale = 1;
        }

    }
    void Update()
    {
        if (movementVector.x > 0 && !facingRight)
        {
            Flip();
            facingRight = true;
        }

        else if (movementVector.x < 0 && facingRight)
        {
            Flip();
            facingRight = false;
        }

    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);

    }

    public void OnJump(InputValue movementValue)
    {
       if(isGrounded && jump == false)
        {
            jump = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Touching ground");
        }
    }

    public void OnShoot(InputValue movementValue)
    {
        Debug.Log("Shooting");
        if (Time.time >= nextShoot)
        {
            nextShoot = Time.time + shootRate;
            animator.SetTrigger("isShooting");
            Instantiate(shoot, shootPoint.position, facingRight ? shootPoint.rotation : Quaternion.Euler(0, 180, 0));
            shootSound.PlayOneShot(shootSound.clip, 1f);
        }
    }

    //restart the game if player leaves platform or falls in lava
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Boundary"))
        {
            GameManager.instance.DecreaseLives();   //decrease lives when out of bounds
            SceneManager.LoadScene(0);
        }
        else if(collision.gameObject.CompareTag("GoodBoundary"))
        {
            Debug.Log("Hit good boundary");
            SceneManager.LoadScene(1);
        }
        else if(collision.gameObject.CompareTag("EndBoundary"))
        {
            Debug.Log("Hit end boundary");
            gameManager.EndGame();
        }
    }

    public Vector2 GetDirection()
    {
        if (facingRight)
            return Vector2.right;
        else
            return Vector2.left;
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
    }
}
