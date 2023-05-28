using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    // TextMeshProUGUI for displaying the asteroids destroyed count
    public TextMeshProUGUI asteroidsDestroyed;
    // TextMeshProUGUI for displaying the asteroids count
    public TextMeshProUGUI asteriodsCount;

    private int asteroidsDestroyedCount = -1;

    private void Start()
    {
        // Increase the asteroids destroyed count and update the text
        IncreaseAsteroidsDestroyedCount();
    }

    // Increase the asteroids destroyed count and update the text
    public void IncreaseAsteroidsDestroyedCount()
    {
        asteroidsDestroyedCount += 1;
        asteroidsDestroyed.text = "Asteroids destroyed: " + asteroidsDestroyedCount.ToString();
    }

    // Initialize the asteroids count text
    public void InitializeAsteroidsCount(int count)
    {
        asteriodsCount.text = "out of " + count.ToString();
    }

    // Show the asteroids destroyed and asteroids count texts
    public void Show()
    {
        asteroidsDestroyed.gameObject.SetActive(true);
        asteriodsCount.gameObject.SetActive(true);
    }
}