using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;          
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   
    [SerializeField] private bool m_AirControl = false;                         
    [SerializeField] private LayerMask m_WhatIsGround;                          
    [SerializeField] private LayerMask m_WhatIsWall;
    [SerializeField] private Transform m_GroundCheck;                           
    [SerializeField] private Transform m_CeilingCheck;                          
    [SerializeField] private Transform m_LeftSideCheck;                          
    [SerializeField] private Transform m_RightSideCheck;                          
    [SerializeField] private Collider2D m_CrouchDisableCollider;                

    const float k_GroundedRadius = .2f; 
    const float k_OnWallRadius = .2f; 
    private bool m_Grounded;            
    private bool m_LeftOnWall;
    private bool m_OnWall;
    private bool m_doubleJumped;
    const float k_CeilingRadius = .2f; 
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                m_doubleJumped = false;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        bool onWall = false;
  
        Collider2D[] collidersRightSide = Physics2D.OverlapCircleAll(m_RightSideCheck.position, k_OnWallRadius, m_WhatIsWall);
        for (int i = 0; i < collidersRightSide.Length; i++)
        {
            if (collidersRightSide[i].gameObject != gameObject)
            {
               // Debug.Log("RightOnWall");
                onWall = true;
            }
        }
        m_OnWall = onWall;
    }


    public void Move(float move, bool crouch, bool jump, bool doubleJump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        if (m_Grounded || m_AirControl)
        {

            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= m_CrouchSpeed;

                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 20f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


            if (move > 0 && !m_FacingRight)
            {

                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        } else if (!m_Grounded)
        {
            Debug.Log(m_Rigidbody2D.velocity.x);
            if (move > 0)
            {
                if(m_Rigidbody2D.velocity.x < 3)
                {
                    m_Rigidbody2D.AddForce(new Vector2(20f, 0f));
                }
                if( !m_FacingRight)
                {
                    Flip();
                }
            }
            else if (move < 0)
            {
                if (m_Rigidbody2D.velocity.x > -3)
                {
                    m_Rigidbody2D.AddForce(new Vector2(-20f, 0f));
                }
                if (m_FacingRight)
                {
                    Flip();
                }
            }
        }

        if (m_Grounded && jump)
        {
            m_Grounded = false; 
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        } else if (!m_Grounded && jump)
        {
            if (m_OnWall)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);

                float scaleX = transform.localScale.x;
                m_Rigidbody2D.AddForce(new Vector2(Mathf.Sign(scaleX) * -300f, m_JumpForce));
                m_OnWall = false;

                Flip();
            }
            else if (!m_doubleJumped)
            {
                m_doubleJumped = true;
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce)); 
            }
        }
    }


    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_RightSideCheck.position, k_OnWallRadius);
    }
}