using UnityEngine;
using UnityEngine.UI; // Required for accessing Image components

public class SwipeToClean : MonoBehaviour
{
    public Image dirtyHands;  // Reference to the dirty hands image
    public Image cleanHands;  // Reference to the clean hands image

    private bool isCleaning = false;

    void Start()
    {
        // Ensure cleanHands starts inactive
        cleanHands.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check if the mouse is pressed
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dirtyHands.rectTransform,
                mousePosition,
                null,
                out Vector2 localPoint);

            if (dirtyHands.rectTransform.rect.Contains(localPoint))
            {
                isCleaning = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCleaning)
            {
                StartCleaning();
                isCleaning = false;
            }
        }
    }

    void StartCleaning()
    {
        // Swap dirty hands image with clean hands image
        dirtyHands.gameObject.SetActive(false);
        cleanHands.gameObject.SetActive(true);
        Debug.Log("Cleaning started.");
    }
}
