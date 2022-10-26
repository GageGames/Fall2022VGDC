using UnityEngine;

[CreateAssetMenu(fileName = "NewPhysicsDataConfig", menuName = "Gameplay Config/Physics Data Config")]
[System.Serializable]
public class PhysicsDataConfig : ScriptableObject
{
	[Tooltip("The percentage of incoming force that is ignored, on a scale from 0 to 1")]
	[Range(0f, 1f)]
	public float Resistance = 0;

	[Tooltip("The mass of the rigidbody associated with this object")]
	public float Mass = 1;
}
