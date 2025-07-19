using UnityEngine;
using UnityEngine.Windows;

// A bit messy cause it was imported from a system where I used states to control the player
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rb;
    private int facingDirection;
    private Vector2 workspace;

    private bool canJump;

    private void Start()
    {
        facingDirection = -1;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        int NormInputX = playerInput.NormInputX;
        int NormInputY = playerInput.NormInputY;
        bool ShootInput = playerInput.attackInput;

        if (NormInputY > 0 && canJump)
        {
            canJump = false;
            AddVelocityY();
        }

        if (CheckIfGrounded())
        {
            canJump = true;
        }

        AddVelocityX(NormInputX);
        CheckIfShouldFlip(NormInputX);
    }
    #region Movement
    // Move left or right
    private void AddVelocityX(int input)
    {
        rb.linearVelocity = new Vector2 (speed * input, rb.linearVelocity.y);
        workspace.x = speed * input;
    }

    // Jump, fixed amount
    private void AddVelocityY()
    {
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }

    private void CheckIfShouldFlip(int input)
    {
        if (input != 0 && input != facingDirection)
        {
            facingDirection *= -1;
            rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.05f, whatIsGround) && rb.linearVelocityY == 0;
    }
    #endregion

    private void Fire()
    {

    }
}
