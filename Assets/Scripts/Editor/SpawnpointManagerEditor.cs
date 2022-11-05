using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CanEditMultipleObjects]
[CustomEditor (typeof (SpawnpointManager))]
public class SpawnpointManagerEditor : Editor
{
    // Specifically not serialized
    // This lets different people have different preferences
    [SerializeField] GizmoEnableCondition gizmoEnableCondition = GizmoEnableCondition.Selected;

    SerializedObject so;

    SerializedProperty propBezierCurviness;
    SerializedProperty propBezierThickness;

    private void OnEnable ()
    {
        so = serializedObject;

        propBezierCurviness = so.FindProperty ("bezierCurviness");
        propBezierThickness = so.FindProperty ("bezierThickness");

        gizmoEnableCondition = (GizmoEnableCondition) EditorPrefs.GetInt ("SpawnpointManagerGizmoEnableCondition", 1);
    }

    public override void OnInspectorGUI ()
    {
        using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope ())
        {
            GizmoEnableCondition enableCondition = (GizmoEnableCondition) EditorGUILayout.EnumPopup ("Gizmos Enabled", gizmoEnableCondition);

            if (check.changed)
            {
                Undo.RecordObject (this, "Change Gizmos Enabled");

                gizmoEnableCondition = enableCondition;
                target.GetComponent<SpawnpointManager> ().UpdateGizmoEnableCondition (enableCondition);

                EditorPrefs.SetInt ("SpawnpointManagerGizmoEnableCondition", (int) gizmoEnableCondition);
            }
        }

        so.Update ();

        EditorGUILayout.PropertyField (propBezierCurviness);
        EditorGUILayout.PropertyField (propBezierThickness);

        so.ApplyModifiedProperties ();
    }
}
