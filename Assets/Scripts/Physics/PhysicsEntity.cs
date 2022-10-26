using UnityEngine;

// Handles moving an object that reacts to physics. Interfaces with data component and other physics-related classes

[RequireComponent(typeof(PhysicsData))]
public class PhysicsEntity : MonoBehaviour, IImpulseReceiver
{
	[SerializeField]
	protected PhysicsData data;

	public Vector3 GetPosition ()
	{
		return data.Position;
	}

	public void ApplyImpulse(Vector3 direction, float strength, ImpulseSourceType type)
	{
		//print($"physics impulse applied! Direction: {direction.normalized} Strength: {strength}");

		Debug.DrawRay(transform.position, direction.normalized, Color.blue, 0);

		data.rb.AddForce(direction.normalized * strength * (1 - data.Resistance), ForceMode.Impulse);
	}
}
