using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector2 movementVector;
    public float speed = 3;
    public float jumpForce = 300;

    public Animator animator;
    public SpriteRenderer sprite_r;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite_r = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(movementVector.x));
        if (movementVector.x > 0)
        {
            sprite_r.flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        else if (movementVector.x < 0)
        {
            sprite_r.flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }


    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);

    }

    public void OnJump(InputValue movementValue)
    {
        transform.Translate(Vector2.up * jumpForce * Time.deltaTime);
        Debug.Log("Jumping");
    }
}
