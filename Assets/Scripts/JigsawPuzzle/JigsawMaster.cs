using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JigsawMaster : MonoBehaviour
{
    public GameObject levelLoader;

    public int nextScene;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.Find("LevelLoader");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
