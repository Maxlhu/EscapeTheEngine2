using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularController : MonoBehaviour
{
    public Transform platformCircular;

    public GameObject platformCircularMotion;

    public Transform RotationCenter;

    public float AngularSpeed, RotationRadius;
    private float posX, posY, angle = 0;


    private void Start()
    {
        gameObject.tag = "Platform Circular Motion";
    }
    

    private void Update()
    {
        posX = RotationCenter.position.x + Mathf.Cos(angle) * RotationRadius;
        posY = RotationCenter.position.y + Mathf.Sin(angle) * RotationRadius;

        transform.position = new Vector2(posX, posY);

        angle = angle + Time.deltaTime * AngularSpeed;

        if (angle >= 360)
        {
            angle = 0;
        }
    }

        //pour que le joueur suive la plateforme, comment out si pas besoin 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
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
