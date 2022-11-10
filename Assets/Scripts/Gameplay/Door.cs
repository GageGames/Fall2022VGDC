using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
	[SerializeField] private bool requiresAllSubscribedKeys;

	[ConditionalField("requiresAllSubscribedKeys", true)]
	[SerializeField] private int keysRequired = 0;

	private int keysLeft = 0;

	public UnityEvent OnKeyCollected = new UnityEvent();
	public UnityEvent OnDoorOpened = new UnityEvent();

	void Start() {
		if (!requiresAllSubscribedKeys) {
			keysLeft = keysRequired;
		}
	}

	public void RegisterKey()
	{
		keysLeft++;
	}

	public void KeyCollected() {
		keysLeft--;
		OnKeyCollected.Invoke();

		if (keysLeft <= 0) {
			OpenDoor();
			OnDoorOpened.Invoke();
		}
	}

	/// currently destroys door object, can be changed to more complex behavior later
	private void OpenDoor() {
		Destroy(gameObject);
	}
}
