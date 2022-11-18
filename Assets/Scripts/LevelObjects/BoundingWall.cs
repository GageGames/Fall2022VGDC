using System.Collections.Generic;
using UnityEngine;

public class BoundingWall : MonoBehaviour
{
	public Vector3 WallSize = Vector3.one;
	public bool CloseLoop = false;
	public GameObject WallPrefab;

	public List<Vector3> Points = new List<Vector3>();

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Color colorCache = Gizmos.color;
		Matrix4x4 matrixCache = Gizmos.matrix;
		Gizmos.matrix = transform.localToWorldMatrix;
		for (int i = 0; i < Points.Count; i++)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(Points[i], 1);
			if (i < Points.Count - 1)
			{
				Gizmos.color = Color.white;
				Gizmos.DrawLine(Points[i], Points[i+1]);
			}
		}
		Gizmos.matrix = matrixCache;
		Gizmos.color = colorCache;
	}
#endif
}
