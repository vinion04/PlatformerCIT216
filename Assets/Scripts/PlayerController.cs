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

    //public
    public SpriteRenderer sprite_r;
    public Rigidbody2D body;
    public float speed = 3;
    public float jumpForce = 300;
    public float maxSpeed = 7f;
    public float gravityMultiplier = 3f;
    public GameObject shoot;    //for our bullets
    public Transform shootPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_r = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(movementVector.x));
        if (movementVector.x > 0 && Mathf.Abs( body.velocity.x) < maxSpeed)
        {
            sprite_r.flipX = false;
            body.AddForce(Vector2.right * speed);
        }

        else if (movementVector.x < 0 && Mathf.Abs(body.velocity.x) < maxSpeed)
        {
            sprite_r.flipX = true;
            body.AddForce(Vector2.left * speed);
        }

        if(jump)
        {
            //StartCoroutine("LerpJump");
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
            Instantiate(shoot, shootPoint.position, shootPoint.rotation);
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
    }
}
