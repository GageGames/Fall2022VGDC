using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum GizmoEnableCondition
{
    Enabled,
    Selected,
    Disabled
}

public class SpawnpointManager : MonoBehaviour
{
    public static List<Spawnpoint> ActiveSpawnpoints = new List<Spawnpoint> ();

    [Range (0f, 1f)]
    public float bezierCurviness = 0.8f;
    public float bezierThickness = 2f;

    GizmoEnableCondition gizmoEnableCondition = GizmoEnableCondition.Selected;

    public static void UpdateAllSpawnpointTypes ()
    {
        foreach (Spawnpoint spawnpoint in ActiveSpawnpoints)
        {
            spawnpoint.ApplySpawnpointType ();
        }
    }

    public void UpdateGizmoEnableCondition (GizmoEnableCondition enableCondition)
    {
        gizmoEnableCondition = enableCondition;
    }

#if UNITY_EDITOR
    
    private void OnDrawGizmos ()
    {
        if (gizmoEnableCondition != GizmoEnableCondition.Enabled)
        {
            return;
        }

        DrawBeziers ();
    }

    private void OnDrawGizmosSelected ()
    {
        if (gizmoEnableCondition != GizmoEnableCondition.Selected)
        {
            return;
        }

        DrawBeziers ();
    }

    void DrawBeziers ()
    {
        Vector3 managerPos = transform.position;
        foreach (Spawnpoint spawnpoint in ActiveSpawnpoints)
        {
            Vector3 barrelPos = spawnpoint.transform.position;
            float avgHeight = (managerPos.y + barrelPos.y) * 0.5f;

            // Note: 2 is a magic value - it's the true maximum of bezierCurviness
            // That value is normalized to 0-1 for usability purposes, and scaled up here
            Vector3 tangentOffset = Vector3.down * avgHeight * bezierCurviness * 2f;

            UnityEngine.Rendering.CompareFunction zTestCache = Handles.zTest;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            Handles.DrawBezier (
                managerPos,
                barrelPos,
                managerPos + tangentOffset,
                barrelPos - tangentOffset,
                spawnpoint.editorPreviewData.discColor,
                EditorGUIUtility.whiteTexture,
                spawnpoint.editorPreviewData.discThickness * bezierThickness
            );

            Handles.zTest = zTestCache;
        }
    }
#endif
}
