using UnityEngine;
using UnityEngine.Events;

public class PowerCell : MonoBehaviour, ITouchable
{
    private Player player;

    private float rotAngle = 30.0f;
    private int numOfWeap;
    float r;

    public UnityEvent OnTouched;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        numOfWeap = System.Enum.GetValues(typeof(WeaponType)).Length;
        InvokeRepeating("PassiveRotate", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, rotAngle, ref r, 1f);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void PassiveRotate()
    {
        rotAngle *= -1;
    }

    public void OnTouch()
    {
        // Random weapon
        int weapNum = Random.Range(0, numOfWeap);
        if ((WeaponType)weapNum == player.currentWeap.GetWeapType())
        {
            if (weapNum >= 0 && weapNum < numOfWeap-1)
            {
                weapNum++;
            }
            else
            {
                weapNum--;
            }
        }
        player.ChangeWeap((WeaponType)weapNum);

        OnTouched?.Invoke();
        // Play sound

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            OnTouch();
        }
    }
}
