using UnityEngine;

public class ButtonSoundHandler : MonoBehaviour
{
    public void OnButtonClick()
    {
        // Call the AudioManager's method to play the button sound
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonSound();
        }
    }
}
