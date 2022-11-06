using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
	SerializedObject so;
	SerializedProperty propSpawnRadius;
	SerializedProperty propSpawnpointType;

	SerializedProperty propMaxSpawnQuantity;
	SerializedProperty propMinSpawnQuantity;

	SerializedProperty propSpawnPrefab;

	SerializedProperty propCustomDiscColor;
	SerializedProperty propCustomDiscThickness;

	float selectionBuffer = 10f;

	int hotControlID = 0;

	private void OnEnable()
	{
		so = serializedObject;

		propSpawnRadius = so.FindProperty("SpawnRadius");
		propSpawnpointType = so.FindProperty("spawnType");

		propMaxSpawnQuantity = so.FindProperty("MaxSpawnQuantity");
		propMinSpawnQuantity = so.FindProperty("MinSpawnQuantity");

		propSpawnPrefab = so.FindProperty("spawnPrefab");

		propCustomDiscColor = so.FindProperty("customDiscColor");
		propCustomDiscThickness = so.FindProperty("customDiscThickness");
	}

	public override void OnInspectorGUI()
	{
		so.Update();

		EditorGUILayout.PropertyField(propSpawnRadius);

		EditorGUILayout.PropertyField(propMaxSpawnQuantity);
		EditorGUILayout.PropertyField(propMinSpawnQuantity);

		EditorGUILayout.PropertyField(propSpawnPrefab);

		so.ApplyModifiedProperties();

		so.Update();
		using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
		{
			EditorGUILayout.PropertyField(propSpawnpointType);

			if (so.ApplyModifiedProperties())
			{
				SpawnManager.UpdateAllSpawnpointTypes();
			}

			if (propSpawnpointType.enumValueIndex == (int)SpawnType.Custom)
			{
				so.Update();

				EditorGUILayout.PropertyField(propCustomDiscColor, new GUIContent("Color"));
				EditorGUILayout.PropertyField(propCustomDiscThickness, new GUIContent("Thickness"));

				so.ApplyModifiedProperties();
			}
		}
	}

	private void OnSceneGUI()
	{
		hotControlID = GUIUtility.GetControlID("Spawn Preview Circle".GetHashCode(), FocusType.Passive);

		foreach (Object targetObject in so.targetObjects)
		{
			Vector3 targetOrigin = targetObject.GetComponent<Transform>().position;

			Plane dragPlane = new Plane(Vector3.up, targetOrigin);

			float radius = propSpawnRadius.floatValue;
			DrawSpawnRadius(targetOrigin, radius, dragPlane, ref radius);
			propSpawnRadius.floatValue = radius;
		}
	}

	void DrawSpawnRadius(Vector3 centerPos, float radius, Plane draggingPlane, ref float newRadius)
	{
		float size = HandleUtility.GetHandleSize(centerPos);
		float selectionRange = selectionBuffer * size;

		if (!Camera.current) return;
		float handleCursorDistance = HandleUtility.DistanceToDisc(centerPos, Vector3.up, radius);

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
						Vector3 intersectPoint = r.GetPoint(dist);

						float radiusDist = Mathf.Max((intersectPoint - centerPos).magnitude, 0.5f);
						//Debug.Log (projectedDist);

						newRadius = radiusDist;
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
