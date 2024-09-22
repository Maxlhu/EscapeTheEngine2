using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position from screen space to world space
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Update the object's position to the mouse position
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }
}
