using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTest : MonoBehaviour
{
    public AudioClip audioClip;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SFXManager.PlaySound (audioClip);
        }
        if (Input.GetMouseButtonDown (1))
        {
            SFXManager.PlayLoopedSound (audioClip, () => Input.GetMouseButtonUp (1));
        }
    }
}
