using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SandClock : MonoBehaviour
{
    [SerializeField] Image fillTopImage;
    [SerializeField] Image fillBottomImage;
    [SerializeField] Text roundText;


    // Events
    [HideInInspector] public UnityAction onAllRoundsCompleted;
    [HideInInspector] public UnityAction<int> onRoundStart;
    [HideInInspector] public UnityAction<int> onRoundEnd;

    [Space(30f)]
    public float roundDuration = 1f;
    public int totalRounds = 30;

    float defaultSandPyramidYPos;
    int currentRound = 0;
    float elapsedTime = 0f;
    bool isRoundActive = false;

    void Awake()
    {
        SetRoundText(totalRounds);
    }

    public void Start()
    {
        Debug.Log("Begin method called");

        if (isRoundActive) return;  // Prevent starting a new round if one is still running

        ++currentRound;

        // Start event
        onRoundStart?.Invoke(currentRound);

        StartCoroutine(StartRound());
    }

    private System.Collections.IEnumerator StartRound()
    {
        Debug.Log("StartRound coroutine started");

        isRoundActive = true;
        elapsedTime = 0f;

        // Fade in sand dots

        // Animate Pyramid
        StartCoroutine(ScalePyramid(0f, 1f, roundDuration / 3f));
        StartCoroutine(ScalePyramid(1f, 0f, roundDuration / 1.5f, roundDuration / 3f));

        // Move Pyramid

        ResetClock();

        // Round Text Fade in
        StartCoroutine(FadeText(roundText, 1f, 0.8f));

        // Fill Top Image
        while (elapsedTime < roundDuration)
        {

            elapsedTime += Time.deltaTime;
            Debug.Log("Elapsed time: " + elapsedTime);

            float normalizedTime = Mathf.Clamp01(elapsedTime / roundDuration);
            fillTopImage.fillAmount = 1f - normalizedTime;
            Debug.Log("fillTopImage.fillAmount: " + fillTopImage.fillAmount);
            OnTimeUpdate();
            yield return null;
        }

        OnRoundTimeComplete();
    }

    void OnTimeUpdate()
    {
        fillBottomImage.fillAmount = 1f - fillTopImage.fillAmount;
    }

    void OnRoundTimeComplete()
    {
        isRoundActive = false;  // Mark round as completed

        // Round end event
        onRoundEnd?.Invoke(currentRound);


        if (currentRound < totalRounds)
        {
            // More rounds available
            roundText.color = new Color(roundText.color.r, roundText.color.g, roundText.color.b, 0f);

            StartCoroutine(RotateSandClock(() =>
            {
                SetRoundText(totalRounds - currentRound);
                Start();
            }));
        }
        else
        {
            // All rounds completed
            onAllRoundsCompleted?.Invoke();
            SetRoundText(0);
            StartCoroutine(ShakeScale(0.8f, 0.3f));
        }
    }

    void SetRoundText(int value)
    {
        roundText.text = value.ToString();
    }

    public void ResetClock()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        fillTopImage.fillAmount = 1f;
        fillBottomImage.fillAmount = 0f;
    }

    // Utility Coroutines
    private System.Collections.IEnumerator FadeImage(Image image, float targetAlpha, float duration)
    {
        Color startColor = image.color;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, targetAlpha, normalizedTime));
            yield return null;
        }
        image.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }

    private System.Collections.IEnumerator FadeText(Text text, float targetAlpha, float duration)
    {
        Color startColor = text.color;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            text.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(startColor.a, targetAlpha, normalizedTime));
            yield return null;
        }
        text.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }

    private System.Collections.IEnumerator ScalePyramid(float startScale, float endScale, float duration, float delay = 0f)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        Vector3 startScaleVector = new Vector3(1f, startScale, 1f);
        Vector3 endScaleVector = new Vector3(1f, endScale, 1f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
    }

    private System.Collections.IEnumerator MoveRectTransformY(RectTransform rect, float targetY, float duration)
    {
        Vector2 startPos = rect.anchoredPosition;
        Vector2 targetPos = new Vector2(startPos.x, targetY);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, normalizedTime);
            yield return null;
        }
        rect.anchoredPosition = targetPos;
    }

    private System.Collections.IEnumerator RotateSandClock(UnityAction onComplete)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 180f);

        float duration = 0.8f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, normalizedTime);
            yield return null;
        }
        transform.rotation = endRotation;

        onComplete?.Invoke();
    }

    private System.Collections.IEnumerator ShakeScale(float duration, float magnitude)
    {
        Vector3 originalScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float x = originalScale.x + Random.Range(-magnitude, magnitude) * Mathf.Sin(elapsed * Mathf.PI * 2 / duration);
            float y = originalScale.y + Random.Range(-magnitude, magnitude) * Mathf.Sin(elapsed * Mathf.PI * 2 / duration);
            transform.localScale = new Vector3(x, y, originalScale.z);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
