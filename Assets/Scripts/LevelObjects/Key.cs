using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
	[HideInInspector]
	public UnityEvent OnKeyPickup = new UnityEvent();

	[SerializeField] Door door;

	void Start()
	{
		if (!door)
		{
			Debug.LogError("Key must have a Door reference assigned");
			Destroy(gameObject);
			return;
		}

		door.RegisterKey();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponentInParent<Player>())
		{
			door?.KeyCollected();
			OnKeyPickup?.Invoke();

			Destroy(gameObject);
		}
	}
}
