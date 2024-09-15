using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTouch : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void OnPointerClick()
    {
        particleSystem.Play();
    }
}
