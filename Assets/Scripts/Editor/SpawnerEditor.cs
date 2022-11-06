using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
	SerializedObject so;
	SerializedProperty propMaxSpawnRadius;
	SerializedProperty propMinSpawnRadius;
	SerializedProperty propSpawnpointType;

	SerializedProperty propMaxSpawnQuantity;
	SerializedProperty propMinSpawnQuantity;

	SerializedProperty propSpawnPrefab;

	SerializedProperty propCustomDiscColor;
	SerializedProperty propCustomDiscThickness;

	float selectionBuffer = 10f;

	int maxHotControlID = 0;
	int minHotControlID = 0;

	private void OnEnable()
	{
		so = serializedObject;

		propMaxSpawnRadius = so.FindProperty("MaxSpawnRadius");
		propMinSpawnRadius = so.FindProperty("MinSpawnRadius");
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

		EditorGUILayout.PropertyField(propMaxSpawnRadius);
		EditorGUILayout.PropertyField(propMinSpawnRadius);

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
		maxHotControlID = GUIUtility.GetControlID("Spawn Max Preview Circle".GetHashCode(), FocusType.Passive);
		minHotControlID = GUIUtility.GetControlID("Spawn Min Preview Circle".GetHashCode(), FocusType.Passive);

		foreach (Object targetObject in so.targetObjects)
		{
			Vector3 targetOrigin = targetObject.GetComponent<Transform>().position;

			Plane dragPlane = new Plane(Vector3.up, targetOrigin);

			float maxRadius = propMaxSpawnRadius.floatValue;
			DrawSpawnRadius(maxHotControlID, targetOrigin, maxRadius, dragPlane, ref maxRadius);
			propMaxSpawnRadius.floatValue = Mathf.Max (maxRadius, 0.5f);

			float minRadius = propMinSpawnRadius.floatValue;
			DrawSpawnRadius(minHotControlID, targetOrigin, minRadius, dragPlane, ref minRadius);
			propMinSpawnRadius.floatValue = minRadius;
		}
	}

	void DrawSpawnRadius(int hotControlID, Vector3 centerPos, float radius, Plane draggingPlane, ref float newRadius)
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

						float radiusDist = (intersectPoint - centerPos).magnitude;
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
