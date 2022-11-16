using System.Collections.Generic;
using UnityEngine;

public class BoundingWall : MonoBehaviour
{
	public GameObject WallPrefab;
	public List<Vector3> Points = new List<Vector3>();

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Color colorCache = Gizmos.color;
		for (int i = 0; i < Points.Count; i++)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(Points[i] + transform.position, 1);
			if (i < Points.Count - 1)
			{
				Gizmos.color = Color.white;
				Gizmos.DrawLine(Points[i] + transform.position, Points[i+1] + transform.position);
			}
		}
		Gizmos.color = colorCache;
	}
#endif
}
