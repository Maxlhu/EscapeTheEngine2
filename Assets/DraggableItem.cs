using Unity.VisualScripting;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    public bool isDragging = false;

    private void Start()
    {
        mainCamera = Camera.main; // R�cup�rer la cam�ra principale
    }

    private void OnMouseDown()
    {
        isDragging = true;
        // Calculer l'offset par rapport � la position de la souris
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (isDragging) {
            // D�placer l'objet avec la souris
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        // R�cup�rer la position de la souris dans le monde
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.nearClipPlane; // Distance de la cam�ra
        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }
}
