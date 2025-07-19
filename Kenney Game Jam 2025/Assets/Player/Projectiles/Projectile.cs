using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime; // How long this projectile lasts before disappearing

    protected Player player;
    protected int direction;
    protected float startTime; // Time when the projectile is first fired   

    protected virtual void Awake()
    {
        direction = 1;
        player = GameObject.Find("ALIEN").GetComponent<Player>();

        gameObject.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        if (direction != player.facingDirection)
        {
            direction *= -1;
            Debug.Log(direction);
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

        if (Time.time - startTime >= lifeTime)
        {
            gameObject.SetActive(false);
        }
    }
}
