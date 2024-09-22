using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Transform destination;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                collider.transform.position = destination.position;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.gravityScale = 2.5f;
            }
        }
    }
}