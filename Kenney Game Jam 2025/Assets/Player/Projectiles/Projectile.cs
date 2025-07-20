using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime; // How long this projectile lasts before disappearing
    [SerializeField] protected int damage;

    protected Player player;
    protected int direction;
    protected float startTime; // Time when the projectile is first fired   

    protected virtual void Awake()
    {
        direction = 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        if (direction != player.facingDirection)
        {
            direction *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        transform.position = player.transform.position;

        startTime = Time.time;
    }

    protected void Update()
    {
        Checks();
    }

    protected virtual void Checks()
    {
        if (gameObject.activeSelf == false)
        {
            return;
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (Time.time - startTime >= lifeTime || player.hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<Enemy>().dead)
            {
                return;
            }
            collision.GetComponent<Enemy>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
