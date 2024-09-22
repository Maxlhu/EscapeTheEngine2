using UnityEngine;
using UnityEngine.SceneManagement;  // Nécessaire pour changer de scène

public class CollectibleScript : MonoBehaviour
{
    public string nextSceneName;  // Nom de la prochaine scène à charger

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si c'est le joueur qui entre en collision avec l'objet
        if (other.CompareTag("Player"))
        {
            // Charger la prochaine scène
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Nom de la prochaine scène non défini !");
            }
        }
    }
}
