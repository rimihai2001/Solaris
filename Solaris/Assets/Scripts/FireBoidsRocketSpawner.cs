using UnityEngine;

public class FireBoidsRocketSpawner : MonoBehaviour
{
    public GameObject boidPrefab;
    public int minBoids = 2;
    public int maxBoids = 10;
    public float moveSpeed = 5f; // Adjust the move speed as desired
    public float stopDistance = 10f; // Distance to stop behind the spaceship

    private Vector3 randomDirection; // Current random direction
    public GameObject spaceship; // Reference to the spaceship GameObject

    void Start()
    {
        randomDirection = Random.insideUnitSphere.normalized; // Generate initial random direction
        SpawnBoids();
    }

    private void SpawnBoids()
    {
        int numBoids = Random.Range(minBoids, maxBoids + 1);

        for (int i = 0; i < numBoids; i++)
        {
            // Generate a random position within the desired range behind the spaceship
            float spawnDistance = Random.Range(-50f, -25f);
            Vector3 spawnOffset = -spaceship.transform.forward * spawnDistance;
            Vector3 spawnPosition = spaceship.transform.position - spawnOffset;

            Quaternion spawnRotation = Quaternion.identity;
            GameObject boid = Instantiate(boidPrefab, spawnPosition, spawnRotation);
            boid.GetComponent<Rigidbody>().velocity = randomDirection * moveSpeed; // Set the velocity of the rigidbody to move in the random direction
        }
    }

    void Update()
    {
        Vector3 targetPosition = spaceship.transform.position; // Set the target position as the spaceship's position
        Vector3 direction = targetPosition - transform.position; // Calculate the direction towards the target position

        // Update the velocity of all spawned boids with the new direction
        GameObject[] boids = GameObject.FindGameObjectsWithTag("FireParticle");
        foreach (GameObject boid in boids)
        {
            boid.GetComponent<Rigidbody>().velocity = direction.normalized * moveSpeed;
        }
    }
}
