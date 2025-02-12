using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    public float force = 8f;

    private Vector2 direction;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        direction = player.GetComponent<PlayerController>().GetDirection();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = force * direction;
        Invoke("Die", 4f);  //wait 4 seconds before destroying bullet
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            Die();
    }
}
