using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Animator transitionAnimator;
    public float transitionTime = 1f;

    // Call this method to load a scene by its index
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneWithTransition(sceneIndex));
    }

    IEnumerator LoadSceneWithTransition(int sceneIndex)
    {
        // Trigger the transition animation
        transitionAnimator.SetTrigger("Start");

        // Wait for the animation to finish
        yield return new WaitForSeconds(transitionTime);

        // Load the scene
        SceneManager.LoadScene(sceneIndex);
    }
}
