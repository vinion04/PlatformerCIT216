using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    public float force = 8f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = force * Vector2.right;
        Invoke("Die", 4f);  //wait 4 seconds before destroying bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
