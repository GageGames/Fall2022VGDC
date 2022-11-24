using System.Collections.Generic;
using UnityEngine;

public class Snappable : MonoBehaviour
{
	public List<Transform> SnapPoints = new List<Transform>();

	private void OnDrawGizmosSelected()
	{
		Color colorCache = Gizmos.color;
		Gizmos.color = Color.blue;

		foreach (Transform point in SnapPoints)
		{
			Gizmos.DrawWireSphere(point.position, 0.1f);
		}

		Gizmos.color = colorCache;
	}
}
