using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    //private variables
    private int direction = 1;  //facing right
    private SpriteRenderer rend;
    
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        Debug.DrawRay(transform.position, new Vector2(0, -3), Color.magenta, 0.5f);

        if(hit.collider == null)    //if nothing under dragon
        {
            direction *= -1; //turn around
            rend.flipX = !rend.flipX;  //turn sprite left
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y) , Time.deltaTime);

        //dragon gets jumped on
        RaycastHit2D headhit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Shoot"))
        {
            Destroy(gameObject);
        }
    }
}
