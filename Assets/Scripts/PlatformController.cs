using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D rb;
    private bool flip = false; //start moving to the right

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb==null)
            Debug.Log("No rigidbody");
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while(true)
        {
            Debug.Log("Platform moving. Direction: " + (flip ? "Left" : "Right"));
            if(!flip)
                rb.velocity = new Vector2(speed, rb.velocity.y);
            else
                rb.velocity = new Vector2(-speed, rb.velocity.y);

            yield return new WaitForSeconds(2f);

            flip = !flip;

            Debug.Log("Direction changed. New direction: " + (flip ? "Left" : "Right"));
        }
    }
}
