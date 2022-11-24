using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// on trigger enter, enables every gameobject in an editor list and disables every gameobject in another editor list,
// before destroying its own gameobject

public class TriggerVolume : MonoBehaviour
{
	[SerializeField] private List<GameObject> enableList;
	[SerializeField] private List<GameObject> disableList;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponentInParent<Player>())
		{
			foreach (GameObject obj in enableList)
			{
				obj.SetActive(true);
			}

			foreach (GameObject obj in disableList)
			{
				obj.SetActive(false);
			}

			Destroy(gameObject);
		}
	}
}
