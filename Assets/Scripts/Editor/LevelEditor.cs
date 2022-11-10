using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

class LevelWallPoint {
	public Vector3 Position;
	public int HotControlID;
}

public class LevelEditor : EditorWindow
{
	[SerializeField]
	List<LevelWallPoint> WallPoints = new List<LevelWallPoint>();

	float selectionBuffer = 10f;

	SerializedObject so;
	SerializedProperty propWallPoints;

	private void OnEnable()
	{
		so = new SerializedObject(this);
		propWallPoints = so.FindProperty("WallPoints");
	}

	private void OnSceneGUI()
	{
		foreach (LevelWallPoint point in WallPoints)
		{
			point.HotControlID = GUIUtility.GetControlID($"WallPoint{point.Position}".GetHashCode(), FocusType.Passive);

			Vector3 targetOrigin = point.Position;
			Plane dragPlane = new Plane(Vector3.up, targetOrigin);

			DrawWallPoint(point.HotControlID, targetOrigin, dragPlane, ref targetOrigin);
			point.Position = targetOrigin;
		}
	}

	void LoadWall()
	{

	}

	void DrawWallPoint(int hotControlID, Vector3 pointPosition, Plane draggingPlane, ref Vector3 newPosition)
	{
		float size = HandleUtility.GetHandleSize(pointPosition);
		float selectionRange = selectionBuffer * size;

		if (!Camera.current) return;
		float handleCursorDistance = HandleUtility.DistanceToCircle(pointPosition, selectionRange);

		Event e = Event.current;
		bool leftMouseDown = e.button == 0;
		bool isDragging = GUIUtility.hotControl == hotControlID && leftMouseDown;
		bool isHovering = HandleUtility.nearestControl == hotControlID;

		switch (e.type)
		{
			case EventType.Layout:
				HandleUtility.AddControl(hotControlID, Mathf.Max(handleCursorDistance - selectionRange, 0));
				break;
			case EventType.MouseDown:
				if (isHovering && leftMouseDown)
				{
					so.Update();

					GUIUtility.hotControl = hotControlID;

					e.Use();
				}
				break;
			case EventType.MouseDrag:
				if (isDragging)
				{
					//Debug.Log ("Dragging!");

					Ray r = HandleUtility.GUIPointToWorldRay(e.mousePosition);
					if (draggingPlane.Raycast(r, out float dist))
					{
						newPosition = r.GetPoint(dist);
					}

					so.ApplyModifiedProperties();

					e.Use();
				}
				break;
			case EventType.MouseUp:
				if (isDragging)
				{
					so.ApplyModifiedProperties();
					GUIUtility.hotControl = 0;
					e.Use();
				}
				break;
		}
	}
}
