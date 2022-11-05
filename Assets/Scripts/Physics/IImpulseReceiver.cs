using UnityEngine;

public enum ImpulseSourceTag
{
	Generic,
	Magnetic,
	PhysicalKnockback,
	DangerousKnockback,
	Explosive,
	Field,
}

public struct ImpulseSourceType
{
	public ImpulseSourceTag impulseSourceTag;

	public ImpulseSourceType(ImpulseSourceTag istg)
	{
		impulseSourceTag = istg;
	}
}

public interface IImpulseReceiver
{
	public void ApplyImpulse(Vector3 direction, float strength, ImpulseSourceType type);
}

