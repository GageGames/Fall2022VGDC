using System.Collections.Generic;
using UnityEngine;

// Creates and manages a tether

[RequireComponent(typeof(MagneticEntity))]
public class Gun : MonoBehaviour
{
	public Tether ActiveTether { get; private set; }

	protected MagneticEntity magEntity;

	[HideInInspector]
	public float Strength = 80f;
	[HideInInspector]
	public float DetectionRadius = 5f;

	private void Awake()
	{
		magEntity = GetComponent<MagneticEntity>();
	}

	[Tooltip("Get anchors near target position")]
	public FireResult GetFireData(Vector3 targetPos)
	{
		FireResult output = new FireResult();
		output.CursorOrigin = targetPos;
		output.AvailableTargets = new List<Anchor>();

		// Find all magnetic entities within range
		MagneticEntity[] magneticTargets = FindAvailableTargetsInRadius(targetPos);

		// Get the anchors from the target entities
		float closestDist = Mathf.Infinity;
		foreach (MagneticEntity potentialTarget in magneticTargets)
		{
			// Grab an anchor from the target
			Anchor targetAnchor = potentialTarget.GetAnchor(targetPos);
			output.AvailableTargets.Add(targetAnchor);

			// Check to find closest anchor
			float dist = Vector3.Distance(targetAnchor.Position, targetPos);
			if (dist < closestDist)
			{
				// Target is closer, store its anchor and distance
				output.SelectedTarget = targetAnchor;
				closestDist = dist;
			}
		}

		return output;
	}

	[Tooltip("Get ideal anchor and create a tether")]
	public FireResult Fire(Vector3 targetPos, bool pull)
	{
		//print("Firing Gun");

		FireResult fireData = GetFireData(targetPos);

		if (fireData.SelectedTarget == null)
		{
			return fireData;
		}

		Anchor self = magEntity.GetAnchor(transform.position);

		if (ActiveTether != null)
		{
			ActiveTether.Detach();
			ActiveTether = null;
		}

		ActiveTether = Tether.CreateTether(self, fireData.SelectedTarget);
		ActiveTether.Strength = Strength * (pull ? 1f : -1f);

		return fireData;
	}

	[Tooltip("Detach fired tether")]
	public void Detach()
	{
		//print("Detaching Gun");

		if (ActiveTether != null)
		{
			ActiveTether.Detach();
			ActiveTether = null;
		}
	}

	[Tooltip("Finds all magnetic entities within range of the target position")]
	MagneticEntity[] FindAvailableTargetsInRadius(Vector3 targetPos)
	{
		// First, find all potential targets by checking for physics objects
		Collider[] potentialTargets = Physics.OverlapSphere(targetPos, DetectionRadius);
		if (potentialTargets.Length == 0)
		{
			return null;
		}

		// Trim potential targets down to objects with Anchors and find the closest
		List<MagneticEntity> targets = new List<MagneticEntity>();
		foreach (Collider potentialTarget in potentialTargets)
		{
			// Check if the potential target is a magnetic entity that *isn't* the entity attached to this gun
			MagneticEntity targetEntity = potentialTarget.GetComponent<MagneticEntity>();
			if (targetEntity != null && targetEntity != magEntity)
			{
				targets.Add(targetEntity);
			}
		}

		return targets.ToArray();
	}
}
