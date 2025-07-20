using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bomb : Projectile
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject spriteObj;
    [SerializeField] private LayerMask whatIsEnemy;

    private bool exploding;
    private Rigidbody2D rb;
    private Collider2D[] targets;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        spriteObj.SetActive(true);
        exploding = false;
    }

    protected override void Checks()
    {
        if (gameObject.activeSelf == false)
        {
            return;
        }
        if (player.hp <= 0)
        {
            gameObject.SetActive(false);
        }

        if (Time.time - startTime >= lifeTime && !exploding)
        {
            exploding = true;
            StartCoroutine(Explode());
        }
    }
    private IEnumerator Explode()
    {
        spriteObj.SetActive(false);
        // Check OverlapCircle, get results into targets, and then damage them accordingly
        targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatIsEnemy);

        foreach (Collider2D target in targets)
        {
            if (target.tag == "Enemy")
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        particles.Play();
        yield return new WaitForSeconds(particles.main.duration);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Ground")
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else if (collision.tag == "Enemy" && !exploding)
        {
            exploding = true;
            StartCoroutine(Explode());
        }
    }
}
