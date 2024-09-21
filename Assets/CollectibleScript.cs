using UnityEngine;
using UnityEngine.SceneManagement;  // N�cessaire pour changer de sc�ne

public class CollectibleScript : MonoBehaviour
{
    public string nextSceneName ;  // Nom de la prochaine sc�ne

    void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si c'est le joueur qui entre en collision avec l'objet
        if (other.CompareTag("Player"))
        {
            // Masquer ou d�truire l'objet collect�
            gameObject.SetActive(false);

            // Charger la prochaine sc�ne
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
