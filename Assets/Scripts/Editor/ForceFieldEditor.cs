using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CanEditMultipleObjects]
[CustomEditor(typeof(ForceField))]
public class ForceFieldEditor : Editor
{
	SerializedObject so;
	SerializedProperty propStrength;
	SerializedProperty propDirection;

	float selectionBuffer = 10f;

	int hotControlID = 0;

	private void OnEnable()
	{
		so = serializedObject;

		propStrength = so.FindProperty("strength");
		propDirection = so.FindProperty("direction");
	}

	public override void OnInspectorGUI()
	{
		so.Update();
		EditorGUILayout.PropertyField(propStrength);
		EditorGUILayout.PropertyField(propDirection);
		so.ApplyModifiedProperties();
	}

	private void OnSceneGUI()
	{
		hotControlID = GUIUtility.GetControlID("Force Preview Handle".GetHashCode(), FocusType.Passive);

		foreach (Object targetObject in so.targetObjects)
		{
			Vector3 targetOrigin = targetObject.GetComponent<Transform>().position;

			Plane dragPlane = new Plane(Vector3.up, targetOrigin);

			float length = propStrength.floatValue;
			Vector3 direction = propDirection.vector3Value;

			DrawForceArrow(targetOrigin, length, direction, dragPlane, ref length, ref direction);

			propStrength.floatValue = length;
			propDirection.vector3Value = direction;
		}
	}

	void DrawForceArrow(Vector3 centerPos, float length, Vector3 direction, Plane draggingPlane, ref float newStrength, ref Vector3 newDirection)
	{
		float size = HandleUtility.GetHandleSize(centerPos);
		float selectionRange = selectionBuffer * size;

		if (!Camera.current) return;
		float handleCursorDistance = HandleUtility.DistanceToLine(centerPos, centerPos + direction * length);

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

						float len = Mathf.Max((intersectPoint - centerPos).magnitude, 0.01f);
						Vector3 dir = (intersectPoint - centerPos).normalized;

						newStrength = len;
						newDirection = dir;
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
