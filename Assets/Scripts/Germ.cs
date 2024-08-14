using UnityEngine;
using UnityEngine.EventSystems;

public class Germ : MonoBehaviour, IPointerClickHandler
{
    // Called when the mouse (or touch) clicks on the germ
    void OnMouseDown()
    {
        // Destroy the germ object
        Destroy(gameObject);

        // Update the score through the GameManager
        GameManager.instance.IncreaseScore();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Destroy the germ object
        Destroy(gameObject);

        // Update the score through the GameManager
        GameManager.instance.IncreaseScore();
    }

}
