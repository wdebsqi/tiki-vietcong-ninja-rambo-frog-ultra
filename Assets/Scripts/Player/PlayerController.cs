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
    public PlayerHealthManager playerHM;
    public GameObject deathParticles;
    public Rigidbody2D enemyHittingRb;
    #endregion

    #region Boolean variables
    bool movesRight = true;
    bool isGrounded;
    public bool isKnockedBack = false;
    public bool canMove = true;
    #endregion

    #region Movement settings
    float HorizontalMovement;
    [Header("Movement settings")]
    public int MovementSpeed = 8;
    public float BasicJumpPower = 25f;
    public int MaxJumpsCount = 1;
    private int CurrentJumpsCount = 0;
    public int hitOnX = 100;
    public int hitOnY = 20;
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
        // Movement - getting input from user
        if (canMove)
            HorizontalMovement = Input.GetAxisRaw("Horizontal");
        else
            HorizontalMovement = 0;

        #region Movement mechanics
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
        if (((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W))))
        {
            Jump();
        }
        #endregion

    }

    // Update is called fixed amount of times (depending on framerate)
    private void FixedUpdate()
    {
    // Checking if box detected colider that is ground
        isGrounded = Physics2D.BoxCast(PlayerLegPosition.position, BoxSize, 0f, Vector2.down, 0, GroundLayer);

    // Movement - Jumping - isGrounded value is false while jumping
        animator.SetBool("isJumping", !isGrounded);

        // Movement - adding movementspeed
        if (canMove)
            rigidbody2.velocity = new Vector2((MovementSpeed) * HorizontalMovement, rigidbody2.velocity.y);

    // Movement - resetting available jumps amount
        if (isGrounded)
        {
            CurrentJumpsCount = 0;
        }

        if (playerHM.isHit && isKnockedBack == false)
        {
            isKnockedBack = true;
            Knockback();
        }
    }

    // Function - Movement - makes player jump
    #region Jump()
    void Jump()
    {
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
    #endregion

    // Function - Animations - makes object flip 180* when changing movement direction (left/right)
    #region Flip()
    void Flip()
    {
        // if moves right
        if(HorizontalMovement == 1)
        {
            // check if was not moving right
            if (!movesRight)
            {
                // if so, rotate, because direction was changed
                transform.Rotate(0, 180f, 0);
            }
            // check that now is moving right
            movesRight = true;
        }
        // if moves left
        else if(HorizontalMovement == -1)
        {
            // check if was moving right
            if (movesRight)
            {
                // if so, rotate, because direction was changed
                transform.Rotate(0, 180f, 0);
            }
            // check that now is not moving right
            movesRight = false;
        }
    }
    #endregion

    // Function - Movement - knocks player according to where 
    public void Knockback()
    {
        //Debug.Log("Velocity: " + rigidbody2.velocity.ToString());
        //Debug.Log("Enemy velocity: " + enemyHittingRb.velocity.ToString());
        canMove = false;

        Knock(0, hitOnY);
        /*
        // player is FALLING on enemy
        if (rigidbody2.velocity.y < 0)
        {
            Knock(rigidbody2.velocity.x, 2 * hitOnY);
        }
        // player is JUMPING and enemy is FALLING
        else if (rigidbody2.velocity.y > 0 && enemyHittingRb.velocity.y < 0)
        {
            Knock(0, 2 * enemyHittingRb.velocity.y);
        }
        // enemy is FALLING on player
        else if (enemyHittingRb.velocity.y < 0)
        {
            Knock(-rigidbody2.velocity.x, -rigidbody2.velocity.y);
        }
        // enemy is moving RIGHT and player is moving LEFT
        else if (enemyHittingRb.velocity.x > 0 && rigidbody2.velocity.x < 0)
        {
            Knock(2 * hitOnX, hitOnY);
        }
        // enemy is moving RIGHT and player is moving RIGHT
        else if (enemyHittingRb.velocity.x > 0 && rigidbody2.velocity.x > 0)
        {
            Knock(-hitOnX, hitOnY);
        }
        // enemy is moving LEFT and player is moving RIGHT
        else if (enemyHittingRb.velocity.x < 0 && rigidbody2.velocity.x > 0)
        {
            Knock(2 * -hitOnX, hitOnY);
        }
        // enemy is moving LEFT and player is moving LEFT
        else if (enemyHittingRb.velocity.x > 0 && rigidbody2.velocity.x < 0)
        {
            Knock(hitOnX, hitOnY);
        }
        // enemy is moving LEFT and hits the NON-MOVING player
        else if (enemyHittingRb.velocity.x < 0 && rigidbody2.velocity.x == 0)
        {
            Knock(2 * -hitOnX, hitOnY);
        }
        // enemy is moving RIGHT and hits the NON-MOVING player
        else if (enemyHittingRb.velocity.x > 0 && rigidbody2.velocity.x == 0)
        {
            Knock(2 * hitOnX, hitOnY);
        }*/
        StartCoroutine(StopUserInput(0.25f));
        StartCoroutine(KnockbackCooldown(1));

        void Knock(float _hitOnX, float _hitOnY)
        {
            rigidbody2.AddForce(new Vector2(_hitOnX, _hitOnY), ForceMode2D.Impulse);
        }
    }

    IEnumerator KnockbackCooldown(int time)
    {
        yield return new WaitForSeconds(time);
        isKnockedBack = false;
    }

    IEnumerator StopUserInput(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    // Function(Unity Event) - Animations & movement - stopping enemy and playing death animation
    #region BeforeDeath() -- for event
    public void BeforeDeath()
    {
        MovementSpeed = 0;
        rigidbody2.velocity = new Vector2(0, rigidbody2.velocity.y);
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
    #endregion

    // Gizmos - Help - drawing helpful things :)
    #region OnDrawGizmos()
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawCube(PlayerLegPosition.position, BoxSize);
    }
    #endregion

}
