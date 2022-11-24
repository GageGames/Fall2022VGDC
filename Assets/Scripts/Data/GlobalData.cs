using UnityEngine;

public class GlobalData : MonoBehaviour
{
	public GlobalConfig GlobalConfigInstance;

	private void Awake()
	{
		GlobalConfigInstance = Resources.Load("Data/GlobalConfig") as GlobalConfig;
	}
}
