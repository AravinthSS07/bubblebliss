using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;  // Reference to StartPanel
    public GameObject gamePanel;   // Reference to GamePanel
    private SpawnManager spawnManager;

    void Start()
    {
        // Show StartPanel and hide GamePanel
        startPanel.SetActive(true);
        gamePanel.SetActive(false);

        // Get reference to SpawnManager
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // This function is triggered by the Start button
    public void StartGame()
    {
        // Hide StartPanel and show GamePanel
        startPanel.SetActive(false);
        gamePanel.SetActive(true);

        // Start spawning germs
        if (spawnManager != null)
        {
            spawnManager.StartSpawning();
        }
    }
}
