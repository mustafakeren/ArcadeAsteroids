using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject prefabAsteroid;
    
    float asteroidRadius;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instantiate asteroid prefab to get radius, then destroy it
        GameObject tempAsteroid = Instantiate(prefabAsteroid);
        asteroidRadius = tempAsteroid.GetComponent<CircleCollider2D>().radius;
        Destroy(tempAsteroid);
        
        // Create asteroid moving left from just outside the right side of screen
        GameObject leftMovingAsteroid = Instantiate(prefabAsteroid);
        Vector3 spawnPosition = new Vector3(ScreenUtils.ScreenRight + asteroidRadius, 0, 0);
        leftMovingAsteroid.GetComponent<Asteroid>().Initialize(Direction.Left, spawnPosition);
        
        // Create asteroid moving down from just outside the top of screen
        GameObject downMovingAsteroid = Instantiate(prefabAsteroid);
        spawnPosition = new Vector3(0, ScreenUtils.ScreenTop + asteroidRadius, 0);
        downMovingAsteroid.GetComponent<Asteroid>().Initialize(Direction.Down, spawnPosition);
        
        // Create asteroid moving up from just outside the bottom of screen
        GameObject upMovingAsteroid = Instantiate(prefabAsteroid);
        spawnPosition = new Vector3(0, ScreenUtils.ScreenBottom - asteroidRadius, 0);
        upMovingAsteroid.GetComponent<Asteroid>().Initialize(Direction.Up, spawnPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
