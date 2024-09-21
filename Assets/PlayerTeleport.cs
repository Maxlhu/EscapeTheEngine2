using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private List<GameObject> activeTeleporters = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && activeTeleporters.Contains(hit.collider.gameObject))
            {
                Teleporter teleporter = hit.collider.GetComponent<Teleporter>();
                Debug.Log("Teleporter: " + teleporter);

                if (teleporter != null)
                {
                    Transform destination = teleporter.GetDestination();
                    transform.position = destination.position;
                    Debug.Log("Teleported to: " + destination.name);

                }
            }
        }
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
