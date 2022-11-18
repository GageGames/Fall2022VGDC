using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTilting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
        // TODO: move these somewhere nicer
        float maxTilt = 45.0f;
        float tiltFactor = 5.0f;
        // xTilt and zTilt are in degrees
        float xTilt = tiltFactor * velocity.x;
        float zTilt = tiltFactor * velocity.x;
        if (xTilt > maxTilt) xTilt = maxTilt;
        if (xTilt < -maxTilt) xTilt = -maxTilt;
        if (zTilt > maxTilt) zTilt = maxTilt;
        if (zTilt < -maxTilt) zTilt = -maxTilt;

        Quaternion newRotation = Quaternion.Euler(new Vector3(xTilt, 0, -zTilt));
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, newRotation, Time.deltaTime);
    }
}
