using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Windows;

// A bit messy cause it was imported from a system where I used states to control the player
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Weapon[] weapons;

    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource victorySound;
    [SerializeField] private AudioSource switchSound;

    public Slider SFXSlider;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    public int facingDirection { get; private set; }

    private bool canJump;
    private bool canFire;

    public int hp;

    public Weapon currentWeap { get; private set; }

    public UnityEvent OnDeath, OnWin;
    public UnityEvent<int> OnDamageTaken;

    private void Start()
    {
        SFXSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }

    private void ChangeSFXVolume()
    {
        hurtSound.volume = SFXSlider.value;
        deathSound.volume = SFXSlider.value;
        victorySound.volume = SFXSlider.value;
        switchSound.volume = SFXSlider.value;
    }

    public void ResetPlayer()
    {
        hp = 3;
        canFire = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        facingDirection = -1;
        transform.rotation = Quaternion.identity;
        ChangeWeap(WeaponType.Single);
        SetVelocityZero();
        transform.position = new Vector3(0, 1, -3);

        ChangeSFXVolume();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            return;
        }

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
            canFire = false;
            Fire();
        }
    }
    #region Movement
    private void SetVelocityZero()
    {
        rb.linearVelocity = Vector2.zero;
    }
    // Move left or right
    private void AddVelocityX(int input)
    {
        rb.linearVelocity = new Vector2 (speed * input, rb.linearVelocity.y);
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

    #region Weapon Management
    public void ChangeWeap(WeaponType weap)
    {
        // Change animation bool (Burst, Explosive)
        if (weap == WeaponType.Burst)
        {
            anim.SetBool("Burst", true);
            anim.SetBool("Explosive", false);
        }
        else if (weap == WeaponType.Goop)
        {
            anim.SetBool("Burst", false);
            anim.SetBool("Explosive", true);
        }
        else
        {
            anim.SetBool("Burst", false);
            anim.SetBool("Explosive", false);
        }
        switchSound.Play();
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
    #endregion

    public void TakeDamage(int damage)
    {
        hp -= damage;
        StartCoroutine("HurtFlash");
        OnDamageTaken?.Invoke(hp);
        // Sound
        hurtSound.Play();
        if (hp <= 0)
        {
            // Dead
            // Death sound
            deathSound.Play();
            OnDeath?.Invoke();
        }
        // Send an event to the UI to update HP display using hp as parameter
    }

    private IEnumerator HurtFlash()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spaceship" && collision.gameObject.GetComponent<Spaceship>().canEscape)
        {
            // Victory sound
            victorySound.Play();
            OnWin?.Invoke();
        }
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
