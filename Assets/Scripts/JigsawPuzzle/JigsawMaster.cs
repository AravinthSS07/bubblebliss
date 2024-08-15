using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JigsawMaster : MonoBehaviour
{
    public GameObject levelLoader;

    public int nextScene;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eight;
    public GameObject nine;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.Find("LevelLoader");
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.locked && p2.locked && p3.locked && p4.locked && p5.locked && p6.locked && p7.locked && p8.locked && p9.locked)
        {
            levelLoader.GetOrAddComponent<LevelLoader>().loadNextLevel(nextScene);
        }
    }
}
