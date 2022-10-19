using System.Collections.Generic;
using UnityEngine;

// Inherits from MagneticEntity
// Tracks a list of anchors for tethered objects.
// Each time an anchor is requested, a new one is created at the closest point on this collider
// This is ideal for large objects/surfaces such as bounding walls

[RequireComponent(typeof(PhysicsEntity))]
public class SurfaceMagneticEntity : MagneticEntity
{
	private Collider col;
	private PhysicsEntity physEntity;
	private List<Anchor> curAnchors;

	private void Awake()
	{
		col = GetComponent<Collider>();
		physEntity = GetComponent<PhysicsEntity>();
	}

	public override Anchor GetAnchor(Vector3 targetPosition)
	{
		// Create and store new anchor
		Anchor output = new Anchor(col.ClosestPoint(targetPosition));
		curAnchors.Add(output);

		// Prep anchor to be recycled when detached from
		output.OnDetachTether.AddListener((tether) => RemoveTetheredAnchor(tether));

		return output;
	}

	void RemoveTetheredAnchor(Tether tether)
	{
		if (curAnchors.Contains(tether.Sender))
		{
			curAnchors.Remove(tether.Sender);
		}
		else if (curAnchors.Contains(tether.Recipient))
		{
			curAnchors.Remove(tether.Recipient);
		}
		else
		{
			Debug.LogError("Tried to remove an anchor used by a tether, but that tether was not attached to anchors that are part of this surface");
		}
	}

	protected override void UpdateAnchorage() { }

	protected override void RefreshTethers()
	{
		tethers.Clear();

		foreach (Anchor anchor in curAnchors)
		{
			tethers.AddRange(anchor.GetTethers());
		}
	}

	protected override void ReadTethers()
	{
		foreach (Anchor anchor in curAnchors)
		{
			foreach (Tether tether in tethers)
			{
				Vector3 pos = tether.GetOpposite(anchor).Position;
				float strength = tether.Strength * Time.deltaTime;
				Vector3 dir = pos - anchor.Position;
				// TODO: Don't create a new ImpulseSourceType every frame for every tether lmao
				physEntity.ApplyImpulse(dir, strength, new ImpulseSourceType(ImpulseSourceTag.Magnetic));
			}
		}
	}
}
