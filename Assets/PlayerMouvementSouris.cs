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
    private Rigidbody2D rb;
    public float jumpForce = 400f;
    private bool doubleJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Déplacement avec clic gauche
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition3D.z = 0f;
            targetPosition = new Vector2(mouseWorldPosition3D.x, transform.position.y);
            isMoving = true;
        }

        // Saut avec clic droit
        if (Input.GetMouseButtonDown(1))
        {
            Jump();
        }

        // Si le joueur se déplace
        if (isMoving)
        {
            float direction = targetPosition.x - transform.position.x;
            horizontalMove = Mathf.Sign(direction) * runSpeed;

            float distance = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * targetPosition.x);

            // Arrêter le mouvement si un mur est détecté
            if (controller.m_OnWall)
            {
                isMoving = false; // Arrêter le mouvement en touchant un mur
                horizontalMove = 0f;
                StopMovement();
            }
            else if (distance < 0.05f)
            {
                isMoving = false;
                horizontalMove = 0f;
                StopMovement();
            }

            controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
        }
        else
        {
            horizontalMove = 0f;
        }

        animator.SetBool("isMovingHorizontal", horizontalMove != 0f);
    }

    void StopMovement()
    {
        // Arrêter immédiatement le mouvement en mettant la vélocité à zéro
        rb.velocity = Vector2.zero;
    }

    // Fonction de saut
    void Jump()
    {
        if (controller.m_Grounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            animator.SetTrigger("Jump");
            doubleJump = false;
        }
        else if (!doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce));
            doubleJump = true;
            animator.SetTrigger("DoubleJump");
        }
    }
}
