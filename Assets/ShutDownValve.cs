using System.Collections;
using UnityEngine;

public class ShutDownValve : MonoBehaviour
{
    public GameObject firstItemPrefab; // L'objet � obtenir
    public GameObject firstMessageUI; // R�f�rence � l'UI du message
    private bool isItemGranted = false; // V�rifie si l'objet a �t� accord�
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
            // Instancier l'objet � la position de la vanne
            Instantiate(firstItemPrefab, transform.position, Quaternion.identity);
            isItemGranted = true;

            // Afficher le message � l'utilisateur
            firstMessageUI.SetActive(true);
            StartCoroutine(HideMessageAfterDelay(10f)); // Appeler la coroutine avec un d�lai de 10 secondes
        }
        if (levelState == 1) {
            //// Instancier l'objet � la position de la vanne
            //Instantiate(firstItemPrefab, transform.position, Quaternion.identity);
            //isItemGranted = true;

            //// Afficher le message � l'utilisateur
            //firstMessageUI.SetActive(true);
            //StartCoroutine(HideMessageAfterDelay(10f)); // Appeler la coroutine avec un d�lai de 10 secondes
        }
    }


    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Attendre pendant le d�lai sp�cifi�
        firstMessageUI.SetActive(false); // Masquer le message apr�s le d�lai
    }

    public void AllowItemGranting()
    {
        isItemGranted = false; // R�initialiser pour permettre de cliquer � nouveau
        levelState++;
    }
}
