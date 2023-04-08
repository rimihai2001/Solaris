using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Variable linking to the player's Transform component
    public Transform playerTransform;
    // Variable that stores the difference between the position of the camera and the position of the player
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // The offset variable is calculated at the beginning of the game
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // The camera moves with the player while maintaining the initial distance from the player
        transform.position = playerTransform.position + offset;
    }
}
