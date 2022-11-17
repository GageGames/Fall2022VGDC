using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
	[SerializeField] private Door door;

	[HideInInspector]
	public UnityEvent OnKeyPickup = new UnityEvent();

	void Start() {
		if (door == null) {
			Debug.LogError("key needs a reference to its assigned door!");
			Destroy(gameObject);
		}
		else {
			door.RegisterKey();
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponentInParent<Player>() != null) {
			if (door != null) {
				door.KeyCollected();
			}
			OnKeyPickup.Invoke();
			Destroy(gameObject);
		}
	}
}
