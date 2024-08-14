using UnityEngine;

public class Germ : MonoBehaviour
{
    // Called when the mouse (or touch) clicks on the germ
    void OnMouseDown()
    {
        // Destroy the germ object
        Destroy(gameObject);

        // Update the score through the GameManager
        GameManager.instance.IncreaseScore();
    }
}
