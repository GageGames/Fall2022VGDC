using UnityEngine;

// Stores pure data on the state of an object that interacts with physics systems

[RequireComponent(typeof(Rigidbody))]
public class PhysicsData : MonoBehaviour
{
	[SerializeField]
	protected PhysicsDataConfig ConfigData;

	[HideInInspector]
	public float Resistance;

	[HideInInspector]
	public Rigidbody rb { get; private set; }
	public Vector3 Position => rb.position;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();

		if (!ConfigData)
		{
			Debug.LogError("PhysicsData must have config data assigned!");
			Resistance = 0;
			return;
		}

		Resistance = ConfigData.Resistance;
		rb.mass = ConfigData.Mass;
		rb.isKinematic = ConfigData.Kinematic;
	}
}
