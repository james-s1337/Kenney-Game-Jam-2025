using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed;
    [SerializeField] private int hp;
    [SerializeField] private int damage;

    private int direction = -1;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeStartDirection()
    {
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Flip();
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void Flip()
    {
        direction *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
