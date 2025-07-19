using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime; // How long this projectile lasts before disappearing

    private Player player;
    private int direction;
    private float startTime; // Time when the projectile is first fired   

    private void Awake()
    {
        direction = 1;
        player = GameObject.Find("ALIEN").GetComponent<Player>();

        gameObject.SetActive(false);
    }

    private void OnEnable()
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

    private void Update()
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
