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

[ExecuteAlways]
public class SpawnManager : MonoBehaviour
{
	public static List<Spawner> CurrentSpawners = new List<Spawner>();

	[Range(0f, 1f)]
	public float bezierCurviness = 0.8f;
	public float bezierThickness = 2f;

	GizmoEnableCondition gizmoEnableCondition = GizmoEnableCondition.Selected;

	public static void UpdateAllSpawnpointTypes()
	{
		foreach (Spawner spawner in CurrentSpawners)
		{
			spawner.ApplySpawnType();
		}
	}

	public void UpdateGizmoEnableCondition(GizmoEnableCondition enableCondition)
	{
		gizmoEnableCondition = enableCondition;
	}

#if UNITY_EDITOR

	private void OnDrawGizmos()
	{
		if (gizmoEnableCondition != GizmoEnableCondition.Enabled)
		{
			return;
		}

		DrawBeziers();
	}

	private void OnDrawGizmosSelected()
	{
		if (gizmoEnableCondition != GizmoEnableCondition.Selected)
		{
			return;
		}

		DrawBeziers();
	}

	void DrawBeziers()
	{
		Vector3 managerPos = transform.position;
		foreach (Spawner spawner in CurrentSpawners)
		{
			Vector3 barrelPos = spawner.transform.position;
			float avgHeight = (managerPos.y + barrelPos.y) * 0.5f;

			Vector3 tangentOffset = Vector3.down * avgHeight * bezierCurviness;

			UnityEngine.Rendering.CompareFunction zTestCache = Handles.zTest;
			Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

			Handles.DrawBezier(
				managerPos,
				barrelPos,
				managerPos + tangentOffset,
				barrelPos - tangentOffset,
				spawner.editorPreviewData.discColor,
				EditorGUIUtility.whiteTexture,
				spawner.editorPreviewData.discThickness * bezierThickness
			);

			Handles.zTest = zTestCache;
		}
	}
#endif
}
