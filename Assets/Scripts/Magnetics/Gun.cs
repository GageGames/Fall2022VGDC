using UnityEngine;

// Creates and manages a tether

public class Gun : MonoBehaviour
{
	public Tether ActiveTether { get; private set; }

	protected MagneticEntity magEntity;

	private void Awake()
	{
		magEntity = GetComponent<MagneticEntity>();
	}

	// Find an anchor and create a tether
	public void Fire(Vector3 targetPos)
	{
		print("Firing Gun");

		Anchor target = FindClosestAnchorInRadius(targetPos);

		if (target == null)
		{
			return;
		}

		Anchor self = magEntity.GetAnchor(transform.position);

		if (ActiveTether != null)
		{
			ActiveTether.Detach();
			ActiveTether = null;
		}

		ActiveTether = Tether.CreateTether(self, target);
	}

	public void Detach ()
	{
		print("Detaching Gun");

		if (ActiveTether != null)
		{
			ActiveTether.Detach();
			ActiveTether = null;
		}
	}

	// Finds the closest Anchor to the target position within range
	Anchor FindClosestAnchorInRadius(Vector3 targetPos)
	{
		// TODO: Get radius from gameplay tuning data scriptableobject
		// First, find all potential targets by checking for physics objects
		Collider[] potentialTargets = Physics.OverlapSphere(targetPos, 5);
		if (potentialTargets.Length == 0)
		{
			return null;
		}

		// Trim potential targets down to objects with Anchors and find the closest
		Anchor closestTarget = null;
		float closestDist = Mathf.Infinity;
		foreach (Collider potentialTarget in potentialTargets)
		{
			MagneticEntity targetEntity = potentialTarget.GetComponent<MagneticEntity>();
			if (targetEntity != null)
			{
				Anchor targetAnchor = targetEntity.GetAnchor(targetPos);
				float dist = Vector3.Distance(targetAnchor.Position, targetPos);
				if (dist < closestDist)
				{
					closestTarget = targetAnchor;
					closestDist = dist;
				}
			}
		}

		return closestTarget;
	}
}
