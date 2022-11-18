using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTilting : MonoBehaviour
{
    [SerializeField]
    private float maxTilt = 45.0f;
    [SerializeField]
    private float tiltFactor = 5.0f;
    void Update()
    {
        Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
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
