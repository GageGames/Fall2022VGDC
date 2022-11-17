using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class stormEffectPositionSetter : MonoBehaviour
{
    VisualEffect stormEffect;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posToPutStorm = playerTransform.position;
        posToPutStorm.y = 0;
        stormEffect.SetVector3("spawnPosition", posToPutStorm);
    }
}
