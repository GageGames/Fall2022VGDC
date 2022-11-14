using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Snapper : EditorWindow
{
	[MenuItem("Custom/Snapper")]
	public static void OpenSnapper() => GetWindow<Snapper>();

	private void OnGUI()
	{
		if (GUILayout.Button("Snap Selected To Active"))
		{
			SnapSelectedToActive();
		}
	}

	[MenuItem("Custom/Snap Selected To Active #&s")]
	static void SnapSelectedToActive()
	{
		SnapData[] snapped = GetSnapToActiveDeltas();
		List<Transform> snapTargets = new List<Transform>();

		foreach (SnapData snapData in snapped)
		{
			snapTargets.Add(snapData.Target);
		}

		Undo.RecordObjects(snapTargets.ToArray(), "Snapped Selected to Active");

		foreach (SnapData snapData in snapped)
		{
			snapData.Target.position += snapData.Delta;
		}
	}

	static SnapData[] GetSnapToActiveDeltas()
	{
		Snappable activeObj = Selection.activeGameObject?.GetComponent<Snappable>();

		if (!activeObj) return null;

		List<SnapData> output = new List<SnapData>();

		// Snap each selected object to the active gameobject
		foreach (GameObject obj in Selection.gameObjects)
		{
			// Only snap snappable objects
			if (!obj.GetComponent<Snappable>()) continue;
			// Don't snap the active gameobject to itself :P
			if (obj == activeObj.gameObject) continue;

			float closestDist = Mathf.Infinity;
			Transform activeSnapPoint = null;
			Transform currentSnapPoint = null;

			// Compare every snapping point on the current object against every snapping point on the active object
			// to get the closest two snap points
			foreach (Transform currentPoint in obj.GetComponent<Snappable>().SnapPoints)
			{
				foreach (Transform activePoint in activeObj.SnapPoints)
				{
					float dist = Vector3.Distance(currentPoint.position, activePoint.position);

					if (dist < closestDist)
					{
						closestDist = dist;
						activeSnapPoint = activePoint;
						currentSnapPoint = currentPoint;
					}
				}
			}

			if (activeSnapPoint == null || currentSnapPoint == null)
			{
				Debug.LogError("Failed to get valid snapping points (did you forget to assign them?)");
				continue;
			}

			output.Add(new SnapData(currentSnapPoint.root, activeSnapPoint.position - currentSnapPoint.position));
		}

		return output.ToArray();
	}
}

[System.Serializable]
public class SnapData
{
	public Transform Target;
	public Vector3 Delta;

	public SnapData(Transform target, Vector3 delta)
	{
		Target = target;
		Delta = delta;
	}
}