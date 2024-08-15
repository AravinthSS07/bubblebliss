using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void nextScene(int next)
    {
        SceneManager.LoadScene(next);
    }
}
