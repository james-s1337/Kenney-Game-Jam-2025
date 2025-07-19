using NUnit.Framework;
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
    [SerializeField] private Weapon[] weapons;

    private Rigidbody2D rb;
    public int facingDirection { get; private set; }
    private Vector2 workspace;

    private bool canJump;
    private bool canFire;

    private Weapon currentWeap;

    private void Start()
    {
        canFire = true;
        ChangeWeap(WeaponType.Burst);
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

        if (currentWeap.CanFire())
        {
            canFire = true;
        }

        if (canFire && ShootInput)
        {
            Debug.Log("Fire!");
            canFire = false;
            Fire();
        }
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
    public void ChangeWeap(WeaponType weap)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.GetWeapType() == weap)
            {
                currentWeap = weapon;
                return;
            }
        }
    }
    
    private void Fire()
    {
        currentWeap.Fire();
    }
}
