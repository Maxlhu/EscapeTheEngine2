using UnityEngine;
using UnityEngine.SceneManagement;  // Nécessaire pour changer de scène

public class CollectibleScript : MonoBehaviour
{
    public string nextSceneName ;  // Nom de la prochaine scène

    void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si c'est le joueur qui entre en collision avec l'objet
        if (other.CompareTag("Player"))
        {
            // Masquer ou détruire l'objet collecté
            gameObject.SetActive(false);

            // Charger la prochaine scène
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
