using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelloader : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionTime = 1f;

    public void nextScene(int index)
    {
        StartCoroutine(switchScene(index));
    }

    IEnumerator switchScene(int index)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }

}
