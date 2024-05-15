using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    private Image[] hearts; // Array to hold heart images
    private int lives;      // Current number of lives
    private int maxLives;   // Maximum number of lives

    void Start()
    {
        // Get all Image components under the Canvas
        hearts = GetComponentsInChildren<Image>();

        // Set initial lives and maxLives based on the number of hearts
        lives = hearts.Length;
        maxLives = hearts.Length;

        // Enable all hearts initially
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = true;
        }
    }

    // Update is not needed for this script

    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <= maxLives && value >= 0)
            {
                lives = value;
                UpdateHeartUI();
                if (lives == 0)
                {
                    pauseGame();
                }
            }
        }
    }

    void UpdateHeartUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Enable the heart images up to the current number of lives
            hearts[i].enabled = i < lives;
        }
    }

    void pauseGame()
    {
        // Implement your pause game logic here
        Debug.Log("Game Paused");
    }
}
