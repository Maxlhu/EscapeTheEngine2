using UnityEngine;
using UnityEngine.UI;

public class ChatCommandScript : MonoBehaviour
{
    public InputField inputField;
    public Text hintText;  // Utilisé pour afficher les indices
    public GameObject player;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on the player!");
        }
        else
        {
            Debug.Log("Rigidbody2D successfully referenced.");
        }

        // Forcer le focus sur l'InputField au début
        FocusInputField();
    }

    void Update()
    {
        if (inputField.interactable)  // Vérifie que l'InputField est interactif
        {
            if (Input.GetKeyDown(KeyCode.Return))  // Appuyer sur "Entrée" pour valider la commande
            {
                ProcessCommand(inputField.text);  // Vérifie la commande entrée
                inputField.text = "";  // Réinitialise le champ de texte après l'entrée
                FocusInputField();  // Redonner le focus à l'InputField
            }
        }
        else
        {
            Debug.LogWarning("InputField is not interactable!");
        }
    }

    void ProcessCommand(string command)
    {
        // Désactiver le script PlayerMovementSouris pour éviter des conflits
        if (player.GetComponent<PlayerMovementSouris>().enabled)
        {
            player.GetComponent<PlayerMovementSouris>().enabled = false;
            Debug.Log("PlayerMovementSouris has been disabled.");
        }

        if (command.ToLower() == "go")
        {
            Debug.Log("Go command received.");
            rb.velocity = new Vector2(moveSpeed * 2.5f, rb.velocity.y);  // Applique la vélocité
            hintText.text = "";  // Efface les indices
        }
        else if (command.ToLower() == "back")
        {
            Debug.Log("Back command received.");
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);  // Applique la vélocité pour reculer
            hintText.text = "";  // Efface les indices
        }
        else if (command.ToLower() == "jump")
        {
            JumpPlayer();  // Appel du saut
        }
        else
        {
            DisplayHint("Command not recognized. Try 'Go', 'Back', or 'Jump'.");  // Affiche l'erreur dans les indices
        }
    }

    void MovePlayer(Vector2 direction)
    {
        Debug.Log("MovePlayer called with direction: " + direction);

        if (rb != null)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            Debug.Log("New velocity applied: " + rb.velocity);
        }
        else
        {
            Debug.LogError("Rigidbody2D is null, cannot move the player.");
        }
    }

    void JumpPlayer()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.001f)  // Vérifie si le joueur est au sol avant de sauter
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            DisplayHint("");  // Efface les indices
        }
        else
        {
            DisplayHint("You can't jump while in the air!");  // Affiche un message d'aide si le joueur est en l'air
        }
    }

    void DisplayHint(string hint)
    {
        hintText.text = hint;  // Affiche l'indice dans le texte des indices, et non dans l'InputField
    }

    // Fonction pour forcer le focus sur l'InputField
    void FocusInputField()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }
}
