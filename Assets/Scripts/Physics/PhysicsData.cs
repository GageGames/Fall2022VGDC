using UnityEngine;

// Stores pure data on the state of an object that interacts with physics systems

[RequireComponent(typeof(Rigidbody))]
public class PhysicsData : MonoBehaviour
{
	// TODO: Read start values from PhysicsDataConfig 
	//[SerializeField]
	//protected PhysicsDataConfig ConfigData;

	[Tooltip("The percentage of incoming force that is ignored, on a scale from 0 to 1")]
	[Range(0f, 1f)]
	public float Resistance = 0;

	public Rigidbody rb { get; private set; }
	public Vector3 Position => rb.position;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
}
