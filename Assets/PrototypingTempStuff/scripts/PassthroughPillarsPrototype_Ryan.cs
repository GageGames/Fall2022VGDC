using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassthroughPillarsPrototype_Ryan : MonoBehaviour
{

	public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "SpeedPillar")
		{
			//should be constant rather than multiplicative
			this.gameObject.GetComponent<Rigidbody>().velocity *= 1.8f;
		}

		//gun detaches so player doesn't hold onto the pillars after passing through
		gun.Detach();
	}
}
