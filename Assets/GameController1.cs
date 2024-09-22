using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public ChangeSpriteOnEvent changeSpriteOnEvent; // Reference to the ChangeSpriteOnEvent script
    Vector2 startPos;
    public string m_nextLevel;

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
        if (collision.CompareTag("Finish"))
        {
            SceneManager.LoadScene(m_nextLevel);
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
