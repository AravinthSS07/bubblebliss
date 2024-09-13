using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceGame : MonoBehaviour
{
    public GameObject levelLoader;

    private int currentSequence = 0;
    [SerializeField]
    private int correctSequence = 12345678;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.Find("levelloader");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed(int buttonNumber)
    {
        currentSequence = currentSequence * 10 + buttonNumber;

        if (currentSequence == correctSequence)
        {
            Debug.Log("Correct sequence entered!");
            levelLoader.GetComponent<levelloader>().nextScene(10);
        }
        else if (currentSequence.ToString().Length > correctSequence.ToString().Length)
        {
            Debug.Log("Incorrect sequence entered!");
            currentSequence = 0;
            levelLoader.GetComponent<levelloader>().nextScene(11);
        }
    }
}
