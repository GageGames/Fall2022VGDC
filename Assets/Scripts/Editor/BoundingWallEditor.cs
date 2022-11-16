using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[CustomEditor(typeof(BoundingWall))]
public class BoundingWallEditor : Editor
{
	float selectionBuffer = 10f;

	SerializedObject so;
	SerializedProperty propWallPrefab;
	SerializedProperty propWallPoints;

	private void OnEnable()
	{
		so = serializedObject;

		propWallPrefab = so.FindProperty("WallPrefab");
		propWallPoints = so.FindProperty("Points");
	}

	public override void OnInspectorGUI()
	{
		if (GUILayout.Button("Bake Walls"))
		{
			BakeWallObjects();
		}

		DrawDefaultInspector();
	}

	private void OnSceneGUI()
	{
		if (propWallPoints == null) return;

		SerializedProperty wps = propWallPoints.Copy(); // copy so we don't iterate the original

		if (!wps.isArray) return;

		so.Update();

		wps.Next(true); // skip generic field
		wps.Next(true); // advance to array size field

		// Get the array size
		int arrayLength = wps.intValue;


		// Handle placing new points


		Event e = Event.current;
		bool leftMouseDown = e.button == 0;
		bool holdingAlt = (e.modifiers & EventModifiers.Alt) != 0;

		Vector3 pos = so.targetObject.GetComponent<Transform>().position;
		Plane dragPlane = new Plane(Vector3.up, pos);

		Vector3? newPoint = null;

		switch (e.type)
		{
			case EventType.MouseDown:
				//Debug.Log("Mouse down");
				if (leftMouseDown && holdingAlt)
				{
					//Debug.Log("Mouse down with alt");
					Ray r = HandleUtility.GUIPointToWorldRay(e.mousePosition);
					if (dragPlane.Raycast(r, out float dist))
					{
						Vector3 spawnPosition = r.GetPoint(dist);
						newPoint = spawnPosition;

						wps.intValue++;
						so.ApplyModifiedProperties();
					}

					e.Use();
				}
				break;
		}


		// Handle inputs for points


		wps.Next(true); // advance to first array index

		// Write values to list
		int lastIndex = arrayLength - 1;
		for (int i = 0; i < arrayLength; i++)
		{
			int hotControlID = GUIUtility.GetControlID($"WallPoint{i}".GetHashCode(), FocusType.Passive);

			Vector3 targetOrigin = wps.vector3Value + pos;

			DrawWallPoint(hotControlID, targetOrigin, dragPlane, ref targetOrigin);

			Vector3 output = targetOrigin - pos;
			output.y = 0;

			wps.vector3Value = output;

			if (i < lastIndex) wps.Next(false); // advance without drilling into children
		}


		// Add new point if one was placed earlier


		if (newPoint != null)
		{
			wps.Next(false);
			wps.vector3Value = newPoint.Value;
		}

		so.ApplyModifiedProperties();
	}

	void DrawWallPoint(int hotControlID, Vector3 pointPosition, Plane draggingPlane, ref Vector3 newPosition)
	{
		float size = HandleUtility.GetHandleSize(pointPosition);

		if (!Camera.current) return;
		float handleCursorDistance = HandleUtility.DistanceToCircle(pointPosition, 1/size);
		//Debug.Log(size + ", " + handleCursorDistance);

		Event e = Event.current;
		bool leftMouseDown = e.button == 0;
		bool isDragging = GUIUtility.hotControl == hotControlID && leftMouseDown;
		bool isHovering = HandleUtility.nearestControl == hotControlID;

		switch (e.type)
		{
			case EventType.Layout:
				HandleUtility.AddControl(hotControlID, Mathf.Max(handleCursorDistance - selectionBuffer, 0));
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

	void BakeWallObjects()
	{
		if (propWallPoints == null) return;

		SerializedProperty wps = propWallPoints.Copy(); // copy so we don't iterate the original

		if (!wps.isArray) return;

		so.Update();

		wps.Next(true); // skip generic field
		wps.Next(true); // advance to array size field

		// Get the array size
		int arrayLength = wps.intValue;

		wps.Next(true); // advance to first array index

		Transform transform = so.targetObject.GetComponent<Transform>();

		Vector3 prevPos = wps.vector3Value + transform.position;
		Vector3 firstPos = prevPos;

		// Clear existing walls
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(transform.GetChild(i).gameObject);
		}

		// Spawn new walls
		for (int i = 0; i < arrayLength - 1; i++)
		{
			wps.Next(false); // advance without drilling into children

			Vector3 nextPos = wps.vector3Value + transform.position;

			GameObject wall = (GameObject) PrefabUtility.InstantiatePrefab(propWallPrefab.objectReferenceValue, transform);
			wall.transform.position = (nextPos + prevPos) * 0.5f;
			wall.transform.rotation = Quaternion.Euler(0, Mathf.Atan2(nextPos.x - prevPos.x, nextPos.z - prevPos.z) * Mathf.Rad2Deg, 0);
			wall.transform.localScale = new Vector3(1, 1, Vector3.Distance(nextPos, prevPos));

			prevPos = nextPos;
		}

		// Spawn last wall
		GameObject lastWall = (GameObject)PrefabUtility.InstantiatePrefab(propWallPrefab.objectReferenceValue, transform);
		lastWall.transform.position = (prevPos + firstPos) * 0.5f;
		lastWall.transform.rotation = Quaternion.Euler(0, Mathf.Atan2(prevPos.x - firstPos.x, prevPos.z - firstPos.z) * Mathf.Rad2Deg, 0);
		lastWall.transform.localScale = new Vector3(1, 1, Vector3.Distance(prevPos, firstPos));
	}
}
