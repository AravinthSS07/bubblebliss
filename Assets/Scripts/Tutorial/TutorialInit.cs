using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInit : MonoBehaviour
{
    private GameObject levelLoader;

    // Start is called before the first frame update
    public void checkTutorial()
    {
        levelLoader = GameObject.Find("LevelLoader");

        if (PlayerPrefs.HasKey("HasDoneTutorial"))
        {
            levelLoader.GetComponent<LevelLoader>().LoadNextLevel(3);
        }
        else
        {
            levelLoader.GetComponent<LevelLoader>().LoadNextLevel(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
