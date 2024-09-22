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
    private Rigidbody2D rb;  // R�f�rence au Rigidbody2D
    public float jumpForce = 400f;  // Force de saut
    private bool doubleJump = false;

    void Start()
    {
        // Obtenir le Rigidbody2D du joueur
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // D�placement avec le clic gauche n'importe o� dans la sc�ne
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition3D.z = 0f;  // Assurer que Z reste � 0 dans le plan 2D
            targetPosition = new Vector2(mouseWorldPosition3D.x, transform.position.y);  // Garder la m�me hauteur Y
            isMoving = true;
        }

        // Saut avec le clic droit
        if (Input.GetMouseButtonDown(1))
        {
            Jump();  // Appeler la fonction de saut lorsque le clic droit est d�tect�
        }

        // D�placement du joueur
        if (isMoving)
        {
            float direction = targetPosition.x - transform.position.x;
            horizontalMove = Mathf.Sign(direction) * runSpeed;

            float distance = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * targetPosition.x);

            if (distance < 0.05f)
            {
                isMoving = false;
                horizontalMove = 0f;
                StopMovement();  // Arr�ter le mouvement imm�diatement
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
        // Forcer l'arr�t imm�diat en mettant la v�locit� � z�ro
        rb.velocity = Vector2.zero;
    }

    // Fonction de saut et double saut
    void Jump()
    {
        if (controller.m_Grounded)  // Utilisation de ton syst�me de v�rification de ground
        {
            rb.AddForce(new Vector2(0f, jumpForce));  // Appliquer une force de saut
            animator.SetTrigger("Jump");  // D�clencher l'animation de saut
            doubleJump = false;  // R�initialiser le double saut
        }
        else if (!doubleJump)  // Si le joueur est en l'air, permettre un double saut
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);  // R�initialiser la v�locit� verticale pour un double saut fluide
            rb.AddForce(new Vector2(0f, jumpForce));  // Appliquer la force du double saut
            doubleJump = true;  // D�sactiver le double saut apr�s usage
            animator.SetTrigger("DoubleJump");  // Animation du double saut (si elle existe)
        }
    }
}
