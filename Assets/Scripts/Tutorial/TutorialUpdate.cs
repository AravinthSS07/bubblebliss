using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUpdate : MonoBehaviour
{
    public void tutorialCompleted()
    {
        if (!PlayerPrefs.HasKey("HasDoneTutorial"))
        {
            PlayerPrefs.SetString("HasDoneTutorial", "yes");
        }
    }
}