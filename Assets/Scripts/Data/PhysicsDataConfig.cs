using MyBox;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPhysicsDataConfig", menuName = "Configs/Physics Data Config")]
[System.Serializable]
public class PhysicsDataConfig : ScriptableObject
{
	[Tooltip("Whether the rigidbody associated with this object is kinematic or not")]
	public bool Kinematic = false;

	[Tooltip("The percentage of incoming force that is ignored, on a scale from 0 to 1")]
	[ConditionalField("Kinematic", true)]
	[Range(0f, 1f)]
	public float Resistance = 0;

	[Tooltip("The mass of the rigidbody associated with this object")]
	[ConditionalField("Kinematic", true)]
	public float Mass = 1;

	[Tooltip("The physics material of the collider(s) associated with this object")]
	public PhysicMaterial physicMaterial;
}
