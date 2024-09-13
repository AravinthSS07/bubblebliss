using UnityEngine;
using UnityEngine.EventSystems;

public class Germ : MonoBehaviour, IPointerClickHandler
{
    // Called when the mouse (or touch) clicks on the germ
    void OnMouseDown()
    {
        DestroyGerm();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DestroyGerm();
    }

    // Function to destroy the germ and increase the score
    void DestroyGerm()
    {
        // Destroy the germ object
        Destroy(gameObject);

        // Update the score through the GameManager
        GameManager.instance.IncreaseScore();
    }
}
