using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapGameScene : MonoBehaviour
{
    public Animator buttonAnimator;  // Reference to the button's Animator
    public string animationName = "ButtonClick";  // The name of the button click animation
    public string sceneToLoad = "HandwashingMapScene";  // The scene to load

    public void LoadScene()
    {
        // Play the button's click animation
        buttonAnimator.Play(animationName);

        // Start a coroutine to wait for the animation to finish
        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        // Wait for the length of the animation
        yield return new WaitForSeconds(buttonAnimator.GetCurrentAnimatorStateInfo(0).length);

        // After the animation finishes, load the scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
