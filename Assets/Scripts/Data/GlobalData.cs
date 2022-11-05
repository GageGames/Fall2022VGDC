using UnityEngine;

public class GlobalData : MonoBehaviour
{
	public static GlobalConfig GlobalConfigInstance;

	private void Awake()
	{
		GlobalConfigInstance = Resources.Load("Data/GlobalConfig") as GlobalConfig;
	}
}
