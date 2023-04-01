using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerScript : MonoBehaviour
{
    public GameObject asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAsteroid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnAsteroid()
    {
        //Varible to know if a coin will spawn on the tile or not
        int asteroidsToSpawn = Random.Range(0, 10);

        for (int i = 0; i < asteroidsToSpawn; i++)
        {
            // save the object we spawned
            GameObject temp = Instantiate(asteroidPrefab, transform);

            // set the position of the coin equal to a random point in the collider
            temp.transform.position = GetRandomPoint();
        }
       
       

        }

    Vector3 GetRandomPoint()
    {
        // generate a point with random coordinates
        Vector3 point = new Vector3(
           Random.Range(-100, 100),
            Random.Range(-100, 100),
            Random.Range(-2000, 2000)
            );

        return point;
    }
}
