using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTiltRotate : MonoBehaviour
{
    [SerializeField] GameObject visualModel;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward);//so the player orients correctly at the start of the level
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = GetComponent<Rigidbody>().velocity;
        if(vel.magnitude >= 0.01f)
        {
            Quaternion newRot = Quaternion.LookRotation(vel, Vector3.up);
            visualModel.transform.rotation = Quaternion.Lerp(visualModel.transform.rotation, newRot, Time.deltaTime * 10);
            visualModel.transform.rotation = Quaternion.Euler(vel.magnitude/4.0f, visualModel.transform.rotation.eulerAngles.y,visualModel.transform.rotation.eulerAngles.z);
        }
    }
}
