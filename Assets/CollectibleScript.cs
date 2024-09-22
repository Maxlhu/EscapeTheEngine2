using UnityEngine;
using UnityEngine.SceneManagement;  // N�cessaire pour changer de sc�ne

public class CollectibleScript : MonoBehaviour
{
    public string nextSceneName;  // Nom de la prochaine sc�ne � charger

    void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si c'est le joueur qui entre en collision avec l'objet
        if (other.CompareTag("Player"))
        {
            // Charger la prochaine sc�ne
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Nom de la prochaine sc�ne non d�fini !");
            }
        }
    }
}
