using UnityEngine;

// Handles moving an object that reacts to physics. Interfaces with data component and other physics-related classes

[RequireComponent(typeof(PhysicsData))]
public class PhysicsEntity : MonoBehaviour
{
	[SerializeField]
	protected PhysicsData data;

	public void ApplyImpulse(Vector3 direction, float strength)
	{
		print($"physics impulse applied! Direction: {direction} Strength: {strength}");

		Debug.DrawRay(transform.position, direction, Color.blue, 5);

		data.rb.AddForce(direction * strength * (1 - data.Resistance), ForceMode.Impulse);
	}
}
