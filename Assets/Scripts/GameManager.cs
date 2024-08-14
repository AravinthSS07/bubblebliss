using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance for easy access
    public int score;  // Variable to hold the current score
    public TMP_Text scoreText;  // UI Text component to display the score

    void Awake()
    {
        // Implement singleton pattern to ensure only one instance of GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Keep GameManager persistent across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    // Method to increase the score and update the UI
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}
