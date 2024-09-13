using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public TextMeshProUGUI timerText; // Reference to TextMeshProUGUI for displaying the timer

    private bool isSoapActive = false;  // Track if soap is active
    private bool isWaterTapActive = false;  // Track if water tap is active

    private Vector2 lastSwipePosition;
    private bool isCleaning = false;
    private int swipeCount = 0;  // Number of swipes
    private const int requiredSwipes = 10000;  // Number of swipes needed to complete cleaning

    private float timerDuration = 20f;  // Timer duration in seconds
    private float timeRemaining;  // Time left on the timer
    private bool timerRunning = false;  // To check if the timer is running

    void Start()
    {
        // Initialize button states and images
        dirtyHands.gameObject.SetActive(true);
        cleanHands.gameObject.SetActive(false);
        waterTapButton.gameObject.SetActive(false);
        towelButton.gameObject.SetActive(false);
        messagePanel.SetActive(false); // Hide message panel initially
        timerText.text = "00:00"; // Initialize the timer text

        // Add listeners to buttons
        soapButton.onClick.AddListener(OnSoapButtonClick);
        waterTapButton.onClick.AddListener(OnWaterTapButtonClick);
        towelButton.onClick.AddListener(OnTowelButtonClick);
    }



    void Update()
    {
        // Update the timer if it's running
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                // Stop the timer when it hits zero and activate the water tap
                timeRemaining = 0;
                timerRunning = false;
                UpdateTimerDisplay(timeRemaining); // Ensure the timer shows 00:00
                ActivateWaterTap(); // Activate water tap when timer ends
            }
        }

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

            isSoapActive = false;

            // The water tap button will now be activated after 20 seconds.
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
            StartTimer(); // Start the 20-second timer
        }
    }

    // Function to start the timer
    void StartTimer()
    {
        timeRemaining = timerDuration;
        timerRunning = true;
    }

    // Function to update the timer display
    void UpdateTimerDisplay(float timeRemaining)
    {
        // Clamp timeRemaining to ensure it doesn't go below 0
        timeRemaining = Mathf.Clamp(timeRemaining, 0, timerDuration);

        // Calculate minutes and seconds
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Ensure the timer shows 00:00 when time runs out
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void OnWaterTapButtonClick()
    {
        if (isWaterTapActive)
        {
            Debug.Log("Water tap button clicked.");

            // Disable the water tap button after it's clicked
            waterTapButton.gameObject.SetActive(false);

            // Hide dirty hands and show clean hands
            if (dirtyHands.gameObject.activeSelf)
            {
                dirtyHands.gameObject.SetActive(false); // Hide dirty hands
            }

            if (!cleanHands.gameObject.activeSelf)
            {
                cleanHands.gameObject.SetActive(true); // Show clean hands
            }

            ActivateTowel(); // Activate towel button after washing

            isWaterTapActive = false; // Set water tap to inactive
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
