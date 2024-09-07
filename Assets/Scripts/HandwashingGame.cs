using UnityEngine;
using UnityEngine.UI; // For accessing UI components
using TMPro; // For TextMeshPro
using System.Collections;

public class HandwashingGame : MonoBehaviour
{
    public Button soapButton;
    public Button waterTapButton;
    public Button towelButton;

    public Image dirtyHands;
    public Image cleanHands;
    public GameObject messagePanel; // Reference to the new Panel for displaying the message
    public TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI for displaying messages

    private bool isSoapActive = false;  // Track if soap is active
    private bool isWaterTapActive = false;  // Track if water tap is active

    private Vector2 lastSwipePosition;
    private bool isCleaning = false;
    private int swipeCount = 0;  // Number of swipes
    private const int requiredSwipes = 100;  // Number of swipes needed to complete cleaning

    void Start()
    {
        // Initialize button states and images
        dirtyHands.gameObject.SetActive(true);
        cleanHands.gameObject.SetActive(false);
        waterTapButton.gameObject.SetActive(false);
        towelButton.gameObject.SetActive(false);
        messagePanel.SetActive(false); // Hide message panel initially

        // Add listeners to buttons
        soapButton.onClick.AddListener(OnSoapButtonClick);
        waterTapButton.onClick.AddListener(OnWaterTapButtonClick);
        towelButton.onClick.AddListener(OnTowelButtonClick);
    }

    void Update()
    {
        if (isSoapActive)
        {
            Vector2 touchPosition = Input.mousePosition;

            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dirtyHands.rectTransform,
                touchPosition,
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

                if (swipeDelta.magnitude > 10f)  // Adjust threshold as needed
                {
                    swipeCount++;
                    if (swipeCount >= requiredSwipes)
                    {
                        StartCleaning();
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0) || Input.touchCount == 0)
            {
                isCleaning = false;
            }
        }
    }

    void StartCleaning()
    {
        if (isSoapActive)
        {
            Debug.Log("StartCleaning called.");
            dirtyHands.gameObject.SetActive(false); // Hide dirty hands
            cleanHands.gameObject.SetActive(true); // Show clean hands
            Debug.Log("Dirty hands hidden and clean hands shown.");

            // Deactivate soap image after cleaning
            isSoapActive = false;

            // Activate water tap after cleaning
            ActivateWaterTap();
        }
    }

    void ActivateWaterTap()
    {
        Debug.Log("ActivateWaterTap called.");
        soapButton.gameObject.SetActive(false); // Disable soap button
        waterTapButton.gameObject.SetActive(true); // Enable water tap button
        isWaterTapActive = true;
    }

    public void OnSoapButtonClick()
    {
        if (!isSoapActive) // Prevent multiple activations
        {
            isSoapActive = true;
            StartCoroutine(SoapTimer());
        }
    }

    IEnumerator SoapTimer()
    {
        yield return new WaitForSeconds(200f); // Wait for 20 seconds
        if (isSoapActive)
        {
            ActivateWaterTap(); // Activate water tap after 20 seconds
        }
    }

    public void OnWaterTapButtonClick()
    {
        if (isWaterTapActive)
        {
            Debug.Log("Water tap button clicked.");
            waterTapButton.gameObject.SetActive(false); // Disable water tap button

            // Ensure clean hands are visible after clicking water tap
            if (!cleanHands.gameObject.activeSelf)
            {
                cleanHands.gameObject.SetActive(true);
            }

            // Logic for washing hands animation or effects here
            ActivateTowel(); // Activate towel button after washing

            // Debug message to confirm state
            Debug.Log("Clean hands should be visible now.");

            isWaterTapActive = false;
        }
    }

    void ActivateTowel()
    {
        Debug.Log("ActivateTowel called.");
        towelButton.gameObject.SetActive(true); // Enable towel button
    }

    public void OnTowelButtonClick()
    {
        towelButton.gameObject.SetActive(false); // Disable towel button after clicking

        // Show message panel
        messagePanel.SetActive(true); // Show the blue background panel

        // Set message text
        messageText.gameObject.SetActive(true); // Ensure message text is visible
        messageText.text = "You have cleaned your hands properly!";
    }
}
