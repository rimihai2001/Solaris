using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SpaceshipController : MonoBehaviour
{
    // Public variables
    public float asteroidHitRadius = 2f;   // Radius within which the spaceship can hit an asteroid
    public float asteroidDetectionRadius = 10f;  // Radius within which asteroids can be detected
    public float moveSpeed = 5f;   // Speed at which the spaceship moves

    // Private variables
    private NavMeshAgent spaceship; // Reference to the spaceship's NavMeshAgent
    private List<Vector3> asteroidPositions; // List of positions of all asteroids
  

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the spaceship's NavMeshAgent
        spaceship = GetComponent<NavMeshAgent>();

        // Get the positions of all asteroids
        asteroidPositions = new List<Vector3>();
        foreach (GameObject asteroid in Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "Asteroid").ToList())
        {
            asteroidPositions.Add(asteroid.transform.position);
        }

        // Find a path to hit all asteroids
        List<Vector3> path = AStarPathfinding.FindPath(transform.position, asteroidPositions.ToArray());

        // Follow the path to hit all asteroids
        StartCoroutine(FollowPath(path));
    }

    // Coroutine to follow the path to hit all asteroids
    IEnumerator FollowPath(List<Vector3> path)
    {
        // Move the spaceship along the path, hitting each asteroid as it is reached
        foreach (Vector3 position in path)
        {
            yield return StartCoroutine(MoveSpaceshipTo(position));
            HitAsteroidAt(position);
        }
    }

    // Coroutine to move the spaceship to the given position
    IEnumerator MoveSpaceshipTo(Vector3 position)
    {
        // Set the spaceship's destination to the given position
        spaceship.destination = position;

        // Wait until the spaceship has reached its destination
        while (spaceship.remainingDistance > 0)
        {
            yield return null;
        }
    }

    // Hits the asteroid at the given position
    void HitAsteroidAt(Vector3 position)
    {
        // Find the asteroid at the given position
        ProceduralAsteroid asteroid = FindAsteroidAt(position);

        // Destroy the asteroid
        if (asteroid != null)
        {
            Destroy(asteroid.gameObject);
        }
    }

    // Finds the asteroid at the given position
    ProceduralAsteroid FindAsteroidAt(Vector3 position)
    {
        // Find all asteroids within the asteroid detection radius
        Collider[] colliders = Physics.OverlapSphere(position, asteroidDetectionRadius, LayerMask.GetMask("Asteroid"));

        // Find the asteroid at the given position
        foreach (Collider collider in colliders)
        {
            ProceduralAsteroid asteroid = collider.GetComponent<ProceduralAsteroid>();
            if (Vector3.Distance(asteroid.transform.position, position) < asteroidHitRadius)
            {
                return asteroid;
            }
        }

        return null;
    }
}
