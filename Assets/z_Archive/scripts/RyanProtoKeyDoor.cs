using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyanProtoKeyDoor : MonoBehaviour
{
	public GameObject[] keys;
	public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//fast implementation
		for (int i = 0; i < keys.Length; i++)
		{
			if(keys[i] == null && i == keys.Length - 1)
			{
				Destroy(door);
			}
			else
			{
				break;
			}
		}
    }
}
