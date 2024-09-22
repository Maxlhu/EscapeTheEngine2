using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.5f;
    Vector2 targetPosition;


    int direction = 1;
    public GameObject movingPlatform;

    void Start()
    {
        gameObject.tag = "MovingPlatform";

        //Position de départ de la plateforme
        targetPosition = endPoint.position;
    }

    private void Update()
    {

        if (Vector2.Distance(transform.position, startPoint.position) < .1f) targetPosition = endPoint.position;

        if (Vector2.Distance(transform.position, endPoint.position) < .1f) targetPosition = startPoint.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

    }


    //pour voir les lignes de la plateforme.
    private void OnDrawGizmos()
    {

        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }


    //pour que le joueur suive la plateforme, comment out si pas besoin 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.platform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}