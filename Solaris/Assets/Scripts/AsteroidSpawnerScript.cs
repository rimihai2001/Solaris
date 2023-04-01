using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    public GameObject asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn the asteroids
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroid()
    {
        //How many asteroids will spawn
        int asteroidsToSpawn = Random.Range(10, 50);

        for (int i = 0; i < asteroidsToSpawn; i++)
        {
            // save the object we spawned
            GameObject temp = Instantiate(asteroidPrefab, transform);

            // set the position of the asteroid equal to a random point in the collider
            temp.transform.position = GetRandomPoint();
        }
       
       

        }

    Vector3 GetRandomPoint()
    {
        // generate a point with random coordinates in the screen
        Vector3 point = new Vector3(
           Random.Range(-500, 500),
            Random.Range(-500, 500),
            Random.Range(-2400, 2400)
            );

        return point;
    }
}
