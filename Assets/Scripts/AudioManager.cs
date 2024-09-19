using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source ----------")]
    [SerializeField] AudioSource musicSource;   // AudioSource for background music
    [SerializeField] AudioSource buttonSource;  // Separate AudioSource for button click sound

    [Header("Audio Clip ----------")]
    public AudioClip background;
    public AudioClip buttonClickSound;

    [Header("Volume Settings ----------")]
    public float normalVolume = 0.5f;  // Normal volume level for background music
    public float loweredVolume = 0.2f; // Lower volume level during button click
    public float volumeRestoreDelay = 0.5f; // Delay before restoring volume

    public static AudioManager instance; // Singleton instance

    private void Awake()
    {
        // Ensure only one instance of AudioManager persists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the object when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Play background music if it's not already playing
        if (!musicSource.isPlaying)
        {
            musicSource.clip = background;
            musicSource.loop = true; // Loop the background music
            musicSource.volume = normalVolume;
            musicSource.Play();
        }
    }

    // Method to play button sound effect and lower background music volume
    public void PlayButtonSound()
    {
        if (buttonClickSound != null)
        {
            // Lower the volume of the background music
            musicSource.volume = loweredVolume;

            // Play the button click sound using a separate AudioSource
            buttonSource.PlayOneShot(buttonClickSound);

            // Restore the volume after the delay
            Invoke("RestoreVolume", volumeRestoreDelay);
        }
    }

    // Restore background music to its normal volume
    private void RestoreVolume()
    {
        musicSource.volume = normalVolume;
    }
}
