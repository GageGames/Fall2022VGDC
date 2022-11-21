using UnityEngine;

public class AcceleratorField : MonoBehaviour
{
	[SerializeField] AnimationCurve SpeedBoostCurve;
	[SerializeField] float maxSpeed;
	[SerializeField] float accelerationStrength;
	public AudioClip SoundEffect;
	static ImpulseSourceType type = new ImpulseSourceType(ImpulseSourceTag.Field);

	private void Awake()
	{
		SFXManager.PlayLoopedSound(SoundEffect, () => false, transform.position, transform);
	}

	private void OnTriggerStay(Collider other)
	{
		PhysicsData pd = other.GetComponentInParent<PhysicsData>();
		IImpulseReceiver receiver = other.GetComponentInParent<IImpulseReceiver>();
		if (receiver != null)
		{
			receiver.ApplyImpulse(pd.rb.velocity.normalized, SpeedBoostCurve.Evaluate(pd.rb.velocity.magnitude / maxSpeed) * accelerationStrength, type);
		}
	}
}
