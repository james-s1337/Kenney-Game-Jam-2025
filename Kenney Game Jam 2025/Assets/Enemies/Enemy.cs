using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private int damage;
    [SerializeField] private ParticleSystem deathParticles;

    private int direction = -1;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;

    public bool dead { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y);
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            StartCoroutine("Die");
        }
        else
        {
            StartCoroutine("HurtFlash");
        }
    }

    public void ChangeStartDirection()
    {
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dead)
        {
            return;
        }

        if (collision.gameObject.tag == "Wall")
        {
            Flip();
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            StartCoroutine("Die");
        }
        if (collision.gameObject.tag == "Spaceship")
        {
            collision.gameObject.GetComponent<Spaceship>().TakeDamage(1);
            StartCoroutine("Die");
        }
    }
    
    private IEnumerator Die()
    {
        dead = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        boxCollider.isTrigger = true;
        // Make sprite disappear (lerp size)
        sprite.color = Color.red;
        // Play particles
        deathParticles.Play();
        // Death sound
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator HurtFlash()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    private void Flip()
    {
        direction *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
