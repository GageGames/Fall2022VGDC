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

	private void Awake()
	{
		// TODO: get position from PhysicsEntity, which should get it from PhysicsData
		curAnchor = new Anchor(transform.position/*PhysicsEntity.GetPosition()*/);
		physEntity = GetComponent<PhysicsEntity>();
	}

	public override Anchor GetAnchor(Vector3 targetPosition)
	{
		print("Anchor requested!");
		return curAnchor;
	}

	protected override void UpdateAnchorage()
	{
		// TODO: get position from PhysicsEntity, which should get it from PhysicsData
		curAnchor.SetPosition(transform.position/*PhysicsEntity.GetPosition()*/);
	}

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
			physEntity.ApplyImpulse(dir, strength);
		}
	}
}
