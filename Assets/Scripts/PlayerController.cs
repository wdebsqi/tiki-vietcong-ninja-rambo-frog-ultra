using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region References
    public Rigidbody2D rigidbody2;
    public LayerMask GroundLayer;
    public Transform PlayerLegPosition;
    public Animator animator; 
    #endregion

    #region Booleans variables
    bool isGrounded;
    #endregion

    #region Movement settings
    float HorizontalMovement;
    [Header("Movement settings")]
    public int MovementSpeed = 8;
    public float BasicJumpPower = 25f;
    public int MaxJumpsCount = 1;
    private int CurrentJumpsCount = 0;
    #endregion

    #region Physics
    public Vector2 BoxSize;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement = Input.GetAxisRaw("Horizontal");
        // Moving  ---- if statement
        if (HorizontalMovement != 0)
        {
            
            Flip();
            animator.SetBool("isRunning", true); // Setting true value of bool - isRunning
        }
        else
        {
            animator.SetBool("isRunning", false); // Setting false value of bool - isRunning
        }

        // Jumping ---- if statement
        if (((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.Space)) || (Input.GetKeyDown(KeyCode.W))))
        {
            Jump();
            
        }

    }

    // Update is called fixed amount of times (depending on framerate)
    private void FixedUpdate()
    {
        // Checking if Raycast detected colider that is ground
        isGrounded = Physics2D.BoxCast(PlayerLegPosition.position, BoxSize, 0f, Vector2.down, 0, GroundLayer);

        animator.SetBool("isJumping", !isGrounded);

        rigidbody2.velocity = new Vector2((MovementSpeed) * HorizontalMovement, rigidbody2.velocity.y);

        if (isGrounded)
        {
            CurrentJumpsCount = 0;
        }
    }

    void Jump()
    {
        Debug.Log(CurrentJumpsCount);
        if (isGrounded)
        {
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, BasicJumpPower);
        }
        else
        {
            if(CurrentJumpsCount < MaxJumpsCount)
            {
                CurrentJumpsCount++;
                rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, (BasicJumpPower * 0.75f));
                animator.SetTrigger("nextJump");
            }
        }
    }

    void Flip()
    {
        if(HorizontalMovement == 1)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(HorizontalMovement == -1)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(PlayerLegPosition.position, BoxSize);
    }
}
