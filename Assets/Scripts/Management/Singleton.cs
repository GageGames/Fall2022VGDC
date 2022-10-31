using UnityEngine;

public static class Singleton<T> where T : MonoBehaviour
{
	private static readonly object padlock = new object();

	public static T Instance
	{
		get
		{
			lock (padlock)
			{
				Debug.Log(instance);
				if (instance != null)
				{
					Debug.Log(instance.gameObject);
					if (instance.gameObject != null)
					{
						Debug.Log(instance.gameObject.GetInstanceID());
					}
				}
				
				if (instance == null)
				{
					Init();
				}
				return instance;
			}
		}
	}

	static T instance = null;

	static void Init()
	{
		instance = new GameObject(typeof(T).Name).AddComponent<T>();

		// DO NOT REMOVE
		// If you remove this, it will cause a stack overflow when attaching 
		// a coroutine to the new instance immediately after this init
		// I have literally no fucking clue
		Debug.Log(instance.gameObject.GetInstanceID());
	}
}
