using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private List<GameObject> activeTeleporters = new List<GameObject>();
    public Animator animator;
    private Teleporter currentTeleporter;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && activeTeleporters.Contains(hit.collider.gameObject))
            {
                currentTeleporter = hit.collider.GetComponent<Teleporter>();
                Debug.Log("Teleporter: " + currentTeleporter);

                if (currentTeleporter != null)
                {
                    animator.SetBool("isTeleporting", true);
                    Invoke("TeleportPlayer", 0.5f);
                }
            }
        }
    }

    void TeleportPlayer()
    {
        Transform destination = currentTeleporter.GetDestination();
        transform.position = destination.position;
        Debug.Log("Teleported to: " + destination.name);
        animator.SetBool("isTeleporting", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (!activeTeleporters.Contains(collision.gameObject))
            {
                activeTeleporters.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            activeTeleporters.Remove(collision.gameObject); 
        }
        Debug.Log("Active Teleporters: " + activeTeleporters);
    }
}
