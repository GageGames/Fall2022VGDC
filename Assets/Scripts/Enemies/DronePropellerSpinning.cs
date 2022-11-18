using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePropellerSpinning : MonoBehaviour
{
    [SerializeField]
    private bool clockwise;

    [SerializeField]
    private float rotationSpeed;

    void Update()
    {
        float angleChange = rotationSpeed * Time.deltaTime * (clockwise ? 1.0f : -1.0f);
        Quaternion rotation = gameObject.transform.rotation;
        rotation.eulerAngles += new Vector3(0, angleChange, 0);
        gameObject.transform.rotation = rotation;
    }
}
