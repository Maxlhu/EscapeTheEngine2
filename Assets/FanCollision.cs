using UnityEngine;

public class FanCollision : MonoBehaviour
{
    public GameObject valve; // Reference to the shut down valve
    public GameObject brokenValve;
    private SpriteRenderer fanSpriteRenderer; // Reference to the fan's SpriteRenderer

    private void Start()
    {
        brokenValve.SetActive(false);
        fanSpriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer of the fan
    }


    // Called continuously while the object is inside the trigger
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ValveItem"))
        {
            DraggableItem dragDropScript = collision.GetComponent<DraggableItem>();
            if (dragDropScript != null && !dragDropScript.isDragging)
            {
                // Destroy the item
                Destroy(collision.gameObject);
                brokenValve.SetActive(true);
                fanSpriteRenderer.enabled = false;

            }
        }
    }
}
