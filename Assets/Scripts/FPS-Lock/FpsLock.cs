using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}
