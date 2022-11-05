using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPauser : MonoBehaviour
{
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Singleton<GameManager>.Instance.Pause();
		}        
    }
}
