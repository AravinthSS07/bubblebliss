using UnityEngine;
using UnityEngine.UI; // Required for accessing Image components

public class SwipeToClean : MonoBehaviour
{
    public Image dirtyHands;  // Reference to the dirty hands image
    public Image cleanHands;  // Reference to the clean hands image

    private bool isCleaning = false;
    private Vector2 lastSwipePosition;
    private int swipeCount = 0;  // Number of swipes
    private const int requiredSwipes = 200;  // Number of swipes needed to complete cleaning

    void Start()
    {
        // Ensure cleanHands starts inactive
        cleanHands.gameObject.SetActive(false);
    }

    void Update()
    {
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
                if (!isCleaning)
                {
                    isCleaning = true;
                    lastSwipePosition = localPoint;
                }

                Vector2 swipeDelta = localPoint - lastSwipePosition;
                lastSwipePosition = localPoint;

                // Check if swipe distance is significant to count as a swipe
                if (swipeDelta.magnitude > 10f)  // Adjust threshold as needed
                {
                    swipeCount++;
                    if (swipeCount >= requiredSwipes)
                    {
                        StartCleaning();
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isCleaning = false;
        }
    }

    void StartCleaning()
    {
        // Swap dirty hands image with clean hands image
        dirtyHands.gameObject.SetActive(false);
        cleanHands.gameObject.SetActive(true);
        Debug.Log("Cleaning completed.");
    }
}
