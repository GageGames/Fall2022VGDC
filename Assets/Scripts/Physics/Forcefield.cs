using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pushes movable objects in a direction. (Functions like a fan)
public class Forcefield : MonoBehaviour
{
    [SerializeField] float strength;
    [SerializeField] Vector3 direction;
    static ImpulseSourceType type = new ImpulseSourceType(ImpulseSourceTag.Field);

    private void OnTriggerEnter(Collider other) {
        Transform Othert = other.transform;
        while (Othert.parent != null)
        {
            Othert = Othert.parent;
        }
        IImpulseReceiver receiver = Othert.GetComponent<IImpulseReceiver>();
        if (receiver != null) {
            receiver.ApplyImpulse(direction, strength, type);
        }    
    }
}
