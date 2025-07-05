using UnityEngine;

public class Asteroid : MonoBehaviour
{


    [SerializeField]
    Sprite AsteroidSprite0;

    [SerializeField]
    Sprite AsteroidSprite1;

    Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    const float maxImpulseForce = 1f;
    const float minImpulseForce = 0.5f;


    void Start()
    {

    }

    void OnBecameInvisible()
    {
        // Change sprite when wrapping around screen
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int randomSprite = Random.Range(0, 2);
        if (randomSprite == 0)
        {
            spriteRenderer.sprite = AsteroidSprite0;
        }
        else
        {
            spriteRenderer.sprite = AsteroidSprite1;
        }
    }

    public void Initialize(Direction direction, Vector3 vector3)
    {
        // Set the position of the asteroid
        transform.position = vector3;

        // Generate random angle in 30 degree arc (PI/6 radians)
        float randomAngle = Random.Range(0, Mathf.PI / 6);

        // Determine base angle based on direction
        float baseAngle = 0;
        switch (direction)
        {
            case Direction.Right:
                baseAngle = -15 * Mathf.Deg2Rad; // -15 to 15 degrees

                break;
            case Direction.Up:
                baseAngle = 75 * Mathf.Deg2Rad; // 75 to 105 degrees

                break;
            case Direction.Left:
                baseAngle = 165 * Mathf.Deg2Rad; // 165 to 195 degrees

                break;
            case Direction.Down:
                baseAngle = 255 * Mathf.Deg2Rad; // 255 to 285 degrees

                break;
        }

        float angle = baseAngle + randomAngle;
        StartMoving(angle);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(collision.gameObject);
            
            // Check if asteroid is too small to split
            if (gameObject.transform.localScale.x < 0.5f)
            {
                // Asteroid is too small, just destroy it
                Destroy(gameObject);
            }
            else
            {
                // Scale down this asteroid before instantiating copies
                Vector3 localScale = gameObject.transform.localScale;
                localScale.x = localScale.x / 2;
                localScale.y = localScale.y / 2;
                gameObject.transform.localScale = localScale;

                // Instantiate two smaller asteroids
                GameObject piece1 = Instantiate(gameObject, transform.position, Quaternion.identity);
                GameObject piece2 = Instantiate(gameObject, transform.position, Quaternion.identity);
                
                // Get the Asteroid components and make them move in random directions
                Asteroid asteroid1 = piece1.GetComponent<Asteroid>();
                Asteroid asteroid2 = piece2.GetComponent<Asteroid>();
                
                // Generate random angles for each piece
                float randomAngle1 = Random.Range(0, 2 * Mathf.PI);
                float randomAngle2 = Random.Range(0, 2 * Mathf.PI);
                
                // Start the smaller asteroids moving
                asteroid1.StartMoving(randomAngle1);
                asteroid2.StartMoving(randomAngle2);
                
                Destroy(gameObject);
            }
        }
    }

    public void StartMoving(float angle)
    {
        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        float magnitude = Random.Range(minImpulseForce, maxImpulseForce);
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(magnitude * moveDirection, ForceMode2D.Impulse);
    }
}
