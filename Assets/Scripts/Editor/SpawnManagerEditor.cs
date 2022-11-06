using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(SpawnManager))]
public class SpawnManagerEditor : Editor
{
	// Specifically not serialized
	// This lets different people have different preferences
	[SerializeField] GizmoEnableCondition gizmoEnableCondition = GizmoEnableCondition.Selected;

	SerializedObject so;

	SerializedProperty propBezierCurviness;
	SerializedProperty propBezierThickness;

	private void OnEnable()
	{
		so = serializedObject;

		propBezierCurviness = so.FindProperty("bezierCurviness");
		propBezierThickness = so.FindProperty("bezierThickness");

		gizmoEnableCondition = (GizmoEnableCondition)EditorPrefs.GetInt("SpawnManagerGizmoEnableCondition", 1);
	}

	public override void OnInspectorGUI()
	{
		using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
		{
			GizmoEnableCondition enableCondition = (GizmoEnableCondition)EditorGUILayout.EnumPopup("Gizmos Enabled", gizmoEnableCondition);

			if (check.changed)
			{
				Undo.RecordObject(this, "Change Gizmos Enabled");

				gizmoEnableCondition = enableCondition;
				target.GetComponent<SpawnManager>().UpdateGizmoEnableCondition(enableCondition);

				EditorPrefs.SetInt("SpawnManagerGizmoEnableCondition", (int)gizmoEnableCondition);
			}
		}

		so.Update();

		EditorGUILayout.PropertyField(propBezierCurviness);
		EditorGUILayout.PropertyField(propBezierThickness);

		so.ApplyModifiedProperties();
	}
}
