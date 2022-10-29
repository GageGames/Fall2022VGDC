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

		Resistance = ConfigData.Resistance;
		rb.mass = ConfigData.Mass;
	}
}
