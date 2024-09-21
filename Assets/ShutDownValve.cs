using System.Collections;
using UnityEngine;

public class ShutDownValve : MonoBehaviour
{
    public GameObject firstItemPrefab; // L'objet à obtenir
    public GameObject firstMessageUI; // Référence à l'UI du message
    private bool isItemGranted = false; // Vérifie si l'objet a été accordé
    public int levelState = 0;

    private void OnMouseDown()
    {
        if (!isItemGranted)
        {
            GrantItem();
        }
    }

    private void GrantItem()
    {
        if (levelState == 0) {
            // Instancier l'objet à la position de la vanne
            Instantiate(firstItemPrefab, transform.position, Quaternion.identity);
            isItemGranted = true;

            // Afficher le message à l'utilisateur
            firstMessageUI.SetActive(true);
            StartCoroutine(HideMessageAfterDelay(10f)); // Appeler la coroutine avec un délai de 10 secondes
        }
        if (levelState == 1) {
            //// Instancier l'objet à la position de la vanne
            //Instantiate(firstItemPrefab, transform.position, Quaternion.identity);
            //isItemGranted = true;

            //// Afficher le message à l'utilisateur
            //firstMessageUI.SetActive(true);
            //StartCoroutine(HideMessageAfterDelay(10f)); // Appeler la coroutine avec un délai de 10 secondes
        }
    }


    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attendre pendant le délai spécifié
        firstMessageUI.SetActive(false); // Masquer le message après le délai
    }

    public void AllowItemGranting()
    {
        isItemGranted = false; // Réinitialiser pour permettre de cliquer à nouveau
        levelState++;
    }
}
