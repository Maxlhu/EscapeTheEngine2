using UnityEngine;

public class FanCollision : MonoBehaviour
{
    public GameObject valve; // Reference to the shut down valve

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

                // Make the shut down valve clickable again
                ShutDownValve valveScript = valve.GetComponent<ShutDownValve>();
                if (valveScript != null)
                {
                    valveScript.AllowItemGranting();
                }
            }
        }
    }
}
