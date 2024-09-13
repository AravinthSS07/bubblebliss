using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private float waitBeforeAnim = 0;

    IEnumerator switchScene(int index)
    {
        yield return new WaitForSeconds(waitBeforeAnim);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel(int index)
    {
        StartCoroutine(switchScene(index));
    }

    public void nextScene(int index)
    {
        StartCoroutine(switchScene(index));
    }
}
