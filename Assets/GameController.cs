using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ChangeSpriteOnEvent changeSpriteOnEvent; // Reference to the ChangeSpriteOnEvent script
    Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Killer"))
        {
            Die();
        }
    }

    void Die()
    {
        Respawn();
        changeSpriteOnEvent.ChangeSprite();
    }

    void Respawn()
    {
        transform.position = startPos;
    }


}
