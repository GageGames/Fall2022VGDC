using UnityEngine;

[CreateAssetMenu(fileName = "NewPitConfig", menuName = "Configs/Pit Config")]
[System.Serializable]

public class PitConfig : ScriptableObject
{
	[Tooltip("The amount of damage applied with every second of contact with this pit")]
	public float DamagePerSecond = 2f;
}
