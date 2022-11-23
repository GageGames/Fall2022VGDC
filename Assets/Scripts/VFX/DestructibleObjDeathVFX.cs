using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjDeathVFX : MonoBehaviour
{
    [SerializeField] GameObject VFXSmokePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() 
    {
        GameObject vfx = Instantiate(VFXSmokePrefab, transform.position,Quaternion.identity) as GameObject;    
        Destroy(vfx, 2.0f);
    }
}
