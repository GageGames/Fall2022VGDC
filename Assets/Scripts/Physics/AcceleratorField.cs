using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratorField : MonoBehaviour
{
    [SerializeField] AnimationCurve SpeedBoostCurve;
    [SerializeField] float maxStrength;
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
            receiver.ApplyImpulse(pd.rb.velocity.normalized, SpeedBoostCurve.Evaluate(pd.rb.velocity.magnitude) * maxStrength, type);
		}
	}
}
