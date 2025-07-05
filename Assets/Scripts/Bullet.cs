using UnityEngine;

public class Bullet : MonoBehaviour
{

    const float liveTime = 2f;
    Timer deathTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = liveTime;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer.Finished)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyForce(Vector2 direction)
    {
        Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
        const float magnitude = 2f;
        rb2d.AddForce(magnitude * direction, ForceMode2D.Impulse);

    }
}
