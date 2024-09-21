using UnityEngine;
using System.Collections;

public class ShutDownValve : MonoBehaviour
{
    public GameObject firstItemPrefab; // L'objet à obtenir
    private bool isItemGranted = false; // Vérifie si l'objet a été accordé
    public GameObject firstMessageUI; // Référence à l'UI du message

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private BoxCollider2D boxCollider2D; // Reference to the BoxCollider2D

    private void Start()
    {
        // Get references to the components on the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        if (!isItemGranted)
        {
            GrantItem();
        }
    }

    private void GrantItem()
    {
        // Instancier l'objet à la position de la vanne
        Instantiate(firstItemPrefab, transform.position, Quaternion.identity);
        isItemGranted = true;
        spriteRenderer.enabled = false;
        boxCollider2D.enabled = false;
        firstMessageUI.SetActive(true); // Afficher le message à l'utilisateur
        StartCoroutine(HideMessageAfterDelay(10f)); // Appeler la coroutine avec un délai de 10 secondes
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attendre pendant le délai spécifié
        firstMessageUI.SetActive(false); // Masquer le message après le délai
    }

}
