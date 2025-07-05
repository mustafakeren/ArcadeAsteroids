using UnityEngine;

public class Ship : MonoBehaviour
{

    [SerializeField]
    GameObject prefabBullet;

    Rigidbody2D rb2d;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 1f;
    const float rotateDegreesPerSecond = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = rotateDegreesPerSecond * Time.deltaTime;
        float rotation = Input.GetAxis("Rotate");
        if (rotation < 0)
        {
            rotationAmount *= -1;
            transform.Rotate(Vector3.forward, rotationAmount);

        }
        if (rotation > 0)
        {
            transform.Rotate(Vector3.forward, rotationAmount);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject bullet = Instantiate<GameObject>(prefabBullet);
            bullet.transform.position = transform.position;
            bullet.AddComponent<Bullet>().ApplyForce(thrustDirection);

        }
    }

    /// <summary>
    /// FixedUpdate is called at fixed intervals and is frame rate independent.
    /// Used for physics-based actions like applying thrust.
    /// </summary>
    void FixedUpdate()
    {
        float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;
        thrustDirection.x = -Mathf.Sin(angleInRadians);
        thrustDirection.y = Mathf.Cos(angleInRadians);
        
        if (Input.GetAxis("Thrust") > 0)
        {
            rb2d.AddForce(ThrustForce * thrustDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);  
        }

    }
}
