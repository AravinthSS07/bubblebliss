using UnityEngine;
using UnityEngine.UI; // For UI components like Text

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30f; // Timer duration set to 30 seconds
    public bool timerIsRunning = false;
    public Text timeText; // Reference to the UI Text to display the time

    public void Begin()
    {
        // Start the timer automatically
        timerIsRunning = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Reduce time by deltaTime each frame
                UpdateTimerText();
            }
            else
            {
                timeRemaining = 0; // Ensure timeRemaining does not go negative
                timerIsRunning = false;
                UpdateTimerText(); // Update the UI one last time
                TimerEnded(); // Call a function when the timer ends
            }
        }
    }

    void UpdateTimerText()
    {
        // Ensure timeRemaining is not negative
        float displayTime = Mathf.Max(timeRemaining, 0);

        // Convert float to a readable time format (minutes:seconds)
        int minutes = Mathf.FloorToInt(displayTime / 60);
        int seconds = Mathf.FloorToInt(displayTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update the UI
    }

    void TimerEnded()
    {
        // Action when the timer ends
        Debug.Log("Time has run out!");
        // You can add any action here, like triggering an event or ending a game
    }
}

