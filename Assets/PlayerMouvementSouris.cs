using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSouris : MonoBehaviour
{
    public CharacterController2D controller;  // Contr�leur du personnage
    public Animator animator;  // Animator pour les animations
    public float runSpeed = 40f;  // Vitesse de d�placement
    public LayerMask groundLayer;  // Le layer qui repr�sente le sol ou les plateformes
    public Rigidbody2D rb;  // Le Rigidbody2D pour g�rer la physique

    private Vector3 targetPosition;  // La position o� le joueur doit se rendre
    private bool isMoving = false;  // Indique si le personnage est en train de se d�placer

    void Update()
    {
        // V�rifier si le bouton gauche de la souris est cliqu�
        if (Input.GetMouseButtonDown(0))
        {
            // Convertir la position de la souris en coordonn�es du monde
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;  // On met la position Z � 0 pour rester dans le plan 2D

            // V�rifier si le clic est sur un sol ou une plateforme
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero, Mathf.Infinity, groundLayer);

            if (hit.collider != null)  // Si le raycast touche le sol ou une plateforme
            {
                targetPosition = hit.point;  // D�finir la position cible
                isMoving = true;  // Activer le mouvement
            }
        }

        // Si le personnage est en mouvement
        if (isMoving)
        {
            // Calculer la direction vers la cible
            Vector2 direction = (targetPosition - transform.position).normalized;
            // Appliquer la vitesse de d�placement via le Rigidbody2D
            rb.velocity = new Vector2(direction.x * runSpeed, rb.velocity.y);

            // Si le personnage a atteint la position cible, arr�ter le mouvement
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                rb.velocity = new Vector2(0, rb.velocity.y);  // Arr�ter compl�tement le mouvement horizontal
                animator.SetBool("isMovingHorizontal", false);  // Arr�ter l'animation de course
            }
            else
            {
                animator.SetBool("isMovingHorizontal", true);  // Activer l'animation de course
            }
        }
        else
        {
            // Si le personnage n'est plus en mouvement, assure-toi que le mouvement est arr�t� et que l'animation est en idle
            rb.velocity = new Vector2(0, rb.velocity.y);  // S'assurer que le mouvement est bien arr�t�
            animator.SetBool("isMovingHorizontal", false);  // Assure que l'animation Idle est bien d�clench�e
        }

        // DEBUG: Voir si l'animation est bien g�r�e
        Debug.Log("isMoving: " + isMoving + ", Velocity: " + rb.velocity.x);
    }

    void FixedUpdate()
    {
        // Pas besoin de gestion sp�cifique dans FixedUpdate, tout est g�r� dans Update
    }
}
