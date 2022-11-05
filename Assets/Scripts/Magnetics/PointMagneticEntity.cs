using UnityEngine;

// Inherits from MagneticEntity
// Tracks an anchor for tethered objects.
// Reuses a single anchor at the object's centerpoint
// Ideal for small, simple objects

[RequireComponent(typeof(PhysicsEntity))]
public class PointMagneticEntity : MagneticEntity
{
	private Anchor curAnchor;
	private PhysicsEntity physEntity;
	private ImpulseSourceType impulseSourceType = new ImpulseSourceType(ImpulseSourceTag.Magnetic);

	private void Start()
	{
		physEntity = GetComponent<PhysicsEntity>();
		curAnchor = new Anchor(physEntity.GetPosition());
	}

	public override Anchor GetAnchor(Vector3 targetPosition)
	{
		//print("Anchor requested");
		return curAnchor;
	}

	protected override void UpdateAnchorage()
	{
		curAnchor.SetPosition(physEntity.GetPosition());
	}

	// TODO: Cache
	protected override void RefreshTethers()
	{
		tethers = curAnchor.GetTethers();
	}

	protected override void ReadTethers()
	{
		foreach (Tether tether in tethers)
		{
			Vector3 pos = tether.GetOpposite(curAnchor).Position;
			float strength = tether.Strength * Time.deltaTime;
			Vector3 dir = pos - curAnchor.Position;
			dir.y = 0;
			physEntity.ApplyImpulse(dir, strength, impulseSourceType);
		}
	}
}
