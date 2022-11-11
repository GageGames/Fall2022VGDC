using System.Collections.Generic;
using UnityEngine;

public enum POIType
{
	Default,
	Key,
	Door
}

public class PointOfInterest : MonoBehaviour
{
	public static List<PointOfInterest> AllPointsOfInterest = new List<PointOfInterest>();

	public POIType Type = POIType.Default;

	private void OnEnable() => AllPointsOfInterest.Add(this);
	private void OnDisable() => AllPointsOfInterest.Remove(this);
}
