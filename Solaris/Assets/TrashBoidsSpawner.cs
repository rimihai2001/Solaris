using UnityEngine;

public class ThrashBoidsSpawner : MonoBehaviour
{
    // Adjust the move speed as desired
    public float moveSpeed = 1f;

    // Time interval between direction changes
    public float directionChangeInterval = 10f;

    // Current random direction
    private Vector3 randomDirection;

    public GameObject boidPrefab;
    public int minBoids = 7;
    public int maxBoids = 10;

    void Start()
    {
        // Generate initial random direction
        randomDirection = Random.insideUnitSphere.normalized;

        StartCoroutine(ChangeDirectionCoroutine());
        SpawnBoids();
    }

    // Spawn boids based on minBoids and maxBoids
    private void SpawnBoids()
    {
        int numBoids = Random.Range(minBoids, maxBoids + 1);

        for (int i = 0; i < numBoids; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-75f, -50f), Random.Range(-75f, -50f), Random.Range(-950f, -925f));
            Quaternion spawnRotation = Quaternion.identity;
            GameObject boid = Instantiate(boidPrefab, spawnPosition, spawnRotation);
            // Set the velocity of the rigidbody to move in the random direction
            boid.GetComponent<Rigidbody>().velocity = randomDirection * moveSpeed;
        }
    }

    // Coroutine to change the random direction at regular intervals
    private System.Collections.IEnumerator ChangeDirectionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(directionChangeInterval);

            // Generate new random direction
            randomDirection = Random.insideUnitSphere.normalized;

            // Update the velocity of all spawned boids with the new random direction
            GameObject[] boids = GameObject.FindGameObjectsWithTag("SpaceTrash");
            foreach (GameObject boid in boids)
            {
                boid.GetComponent<Rigidbody>().velocity = randomDirection * moveSpeed;
            }
        }
    }
}
