using UnityEngine;
using UnityEngine.UI;

public class SoapDrag : MonoBehaviour
{
    public Image soapImage;  // Reference to the soap image
    public Image dirtyHands; // Reference to the dirty hands image
    public Image cleanHands; // Reference to the clean hands image
    public Button waterTapButton; // Reference to the water tap button

    private Vector2 offset;
    private bool isDragging = false;
    private bool soapApplied = false;
    private float soapUsageTime = 0f;  // Time spent applying soap
    private const float requiredSoapTime = 20f;  // Required soap time in seconds
    private Vector2 initialSoapPosition;

    void Start()
    {
        // Ensure cleanHands starts inactive
        cleanHands.gameObject.SetActive(false);

        // Disable the water tap button until the soap has been used for the required time
        waterTapButton.interactable = false;

        // Store the initial position of the soap
        initialSoapPosition = soapImage.rectTransform.anchoredPosition;

        waterTapButton.onClick.AddListener(OnWaterTapClick);
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                soapImage.rectTransform.parent as RectTransform,
                Input.mousePosition,
                null,
                out mousePosition
            );
            soapImage.rectTransform.anchoredPosition = mousePosition - offset;

            if (IsOverDirtyHands())
            {
                // Increase the time spent applying soap when it's dragged over dirty hands
                soapUsageTime += Time.deltaTime;

                // Check if the required soap usage time has been reached
                if (soapUsageTime >= requiredSoapTime)
                {
                    soapApplied = true;
                    waterTapButton.interactable = true;  // Enable the water tap button
                }
            }
        }
    }

    public void OnBeginDrag()
    {
        isDragging = true;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            soapImage.rectTransform.parent as RectTransform,
            Input.mousePosition,
            null,
            out offset
        );
        offset = soapImage.rectTransform.anchoredPosition - offset;
    }

    public void OnEndDrag()
    {
        isDragging = false;

        // Reset soap position if soapApplied is false
        if (!soapApplied)
        {
            soapImage.rectTransform.anchoredPosition = initialSoapPosition;
        }
    }

    private bool IsOverDirtyHands()
    {
        // Check if the soap is over dirty hands
        return RectTransformUtility.RectangleContainsScreenPoint(
            dirtyHands.rectTransform,
            Input.mousePosition
        );
    }

    private void OnWaterTapClick()
    {
        if (soapApplied)
        {
            // Show clean hands only if soap has been applied for the required time
            dirtyHands.gameObject.SetActive(false);
            cleanHands.gameObject.SetActive(true);

            // Reset the soap usage time and position
            soapUsageTime = 0f;
            soapImage.rectTransform.anchoredPosition = initialSoapPosition;

            // Disable the water tap button until the soap is applied again
            waterTapButton.interactable = false;
            soapApplied = false;
        }
    }
}
