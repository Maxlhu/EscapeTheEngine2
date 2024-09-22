using UnityEngine;

public class PlayerMovementSouris : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    public LayerMask groundLayer;

    private Vector2 targetPosition;
    private bool isMoving = false;
    private float horizontalMove = 0f;
    private bool isFacingRight = true;
    private Rigidbody2D rb;  // Référence au Rigidbody2D
    public float jumpForce = 400f;  // Force de saut
    private bool doubleJump = false;

    void Start()
    {
        // Obtenir le Rigidbody2D du joueur
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseWorldPosition = new Vector2(mouseWorldPosition3D.x, mouseWorldPosition3D.y);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, Mathf.Infinity, groundLayer);

            if (hit.collider != null)
            {
                targetPosition = hit.point;
                isMoving = true;
            }
        }
        
        // Saut avec le clic droit
        if (Input.GetMouseButtonDown(1))
        {
            Jump();  // Appeler la fonction de saut lorsque le clic droit est détecté
        }
        if (isMoving)
        {
            float direction = targetPosition.x - transform.position.x;
            horizontalMove = Mathf.Sign(direction) * runSpeed;

            float distance = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * targetPosition.x);

            if (distance < 0.05f)
            {
                isMoving = false;
                horizontalMove = 0f;
                StopMovement();  // Arrêter le mouvement immédiatement
            }

            controller.Move(horizontalMove * Time.fixedDeltaTime, false, false, false);

        }
        else
        {
            horizontalMove = 0f;
        }

        animator.SetBool("isMovingHorizontal", horizontalMove != 0f);
    }

    void StopMovement()
    {
        // Forcer l'arrêt immédiat en mettant la vélocité à zéro
        rb.velocity = Vector2.zero;
    }
    // Fonction de saut
    // Fonction de saut et double saut
    void Jump()
    {
        if (controller.m_Grounded)  // Vérifier si le joueur est au sol avant de sauter
        {
            rb.AddForce(new Vector2(0f, jumpForce));  // Appliquer une force de saut
            animator.SetTrigger("Jump");  // Déclencher l'animation de saut
            doubleJump = false;  // Réinitialiser le double saut
        }
        else if (!doubleJump)  // Si le joueur est en l'air, permettre un double saut
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);  // Réinitialiser la vélocité verticale pour un double saut fluide
            rb.AddForce(new Vector2(0f, jumpForce));  // Appliquer la force du double saut
            doubleJump = true;  // Désactiver le double saut après usage
            animator.SetTrigger("DoubleJump");  // Animation du double saut (si elle existe)
        }
    }
}
