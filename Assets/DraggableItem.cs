using Unity.VisualScripting;
using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    public bool isDragging = false;

    private void Start()
    {
        mainCamera = Camera.main; // Récupérer la caméra principale
    }

    private void OnMouseDown()
    {
        isDragging = true;
        // Calculer l'offset par rapport à la position de la souris
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (isDragging) {
            // Déplacer l'objet avec la souris
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        // Récupérer la position de la souris dans le monde
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.nearClipPlane; // Distance de la caméra
        return mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }
}
