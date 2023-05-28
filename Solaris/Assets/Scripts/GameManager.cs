using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Button to start the game
    public Button btnStart;
    // Button to stop the game
    public Button btnStop;

    // Reference to the SolarSystem script
    public SolarSystem solarSystem;
    // GameObject representing the spaceship
    public GameObject ship;
    // GameObject representing the background image
    public GameObject backgroundImg;
    // GameObject representing the game title
    public GameObject gameTitle;
    // Reference to the SpaceshipScript component
    private SpaceshipScript spaceship;

    // GameObject containing the TextManager script
    public GameObject textManagerObject;
    // Reference to the TextManager script
    private TextManager textManager;

    void Start()
    {
        // Register the StartMovement method to the onClick event of btnStart
        btnStart.onClick.AddListener(StartMovement);
        // Register the StopGame method to the onClick event of btnStop
        btnStop.onClick.AddListener(StopGame);

        // Get the SpaceshipScript component from the ship GameObject
        spaceship = ship.GetComponent<SpaceshipScript>();
        // Get the TextManager component from the textManagerObject GameObject
        textManager = textManagerObject.GetComponent<TextManager>();
    }

    // Start the movement of the spaceship and the solar system
    void StartMovement()
    {
        // Set the isStarted flag in the SpaceshipScript to true
        spaceship.isStarted = true;
        // Set the isStarted flag in the SolarSystem script to true
        solarSystem.isStarted = true;

        // Hide the btnStart button
        btnStart.gameObject.SetActive(false);
        // Hide the btnStop button
        btnStop.gameObject.SetActive(false);

        // Hide the backgroundImg GameObject if it is not null
        if (backgroundImg != null)
        {
            backgroundImg.SetActive(false);
        }

        // Hide the gameTitle GameObject if it is not null
        if (gameTitle != null)
        {
            gameTitle.SetActive(false);
        }

        // Call the show method in the TextManager script
        textManager.Show();
    }

    // Stop the game by quitting the application
    public void StopGame()
    {
        Application.Quit();
    }
}