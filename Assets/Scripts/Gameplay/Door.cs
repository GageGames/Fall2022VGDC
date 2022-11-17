using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
	[SerializeField] bool RequireAllSubscribedKeys;

	[ConditionalField("RequireAllSubscribedKeys", true)]
	[SerializeField] int KeysRequired = 0;

	private int keysLeft = 0;

	[HideInInspector]
	public UnityEvent OnKeyCollected = new UnityEvent();
	[HideInInspector]
	public UnityEvent OnDoorOpened = new UnityEvent();

	void Start()
	{
		if (!RequireAllSubscribedKeys)
		{
			keysLeft = KeysRequired;
		}
	}

	public void RegisterKey()
	{
		if (RequireAllSubscribedKeys)
		{
			keysLeft++;
		}
	}

	public void KeyCollected()
	{
		keysLeft--;
		OnKeyCollected?.Invoke();

		if (keysLeft <= 0)
		{
			OpenDoor();
			OnDoorOpened?.Invoke();
		}
	}

	// Currently destroys door object, can be changed to more complex behavior later
	private void OpenDoor()
	{
		Destroy(gameObject);
	}
}
