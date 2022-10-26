using MyBox;
using UnityEngine;

// Stores pure data on the state of an object that interacts with physics systems

[RequireComponent(typeof(Rigidbody))]
public class PhysicsData : MonoBehaviour
{
	[SerializeField]
	protected bool UseConfigData = true;
	[ConditionalField("UseConfigData")]
	[SerializeField]
	protected PhysicsDataConfig ConfigData;

	[ConditionalField("UseConfigData", true)]
	public float Resistance;

	[HideInInspector]
	public Rigidbody rb { get; private set; }
	public Vector3 Position => rb.position;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();

		if (UseConfigData)
		{
			Resistance = ConfigData.Resistance;
		}
	}
}
