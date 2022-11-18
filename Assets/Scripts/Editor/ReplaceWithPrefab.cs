using UnityEditor;
using UnityEngine;

// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010
// Taken from: https://forum.unity.com/threads/replace-game-object-with-prefab.24311/

public class ReplaceWithPrefab : EditorWindow
{
	public bool CopyScale = true;
	public bool CopyRotation = true;
	public GameObject Prefab;

	SerializedObject so;
	SerializedProperty propCopyScale;
	SerializedProperty propCopyRotation;
	SerializedProperty propPrefab;

	[MenuItem("Custom/Replace With Prefab %#r")]

	static void CreateWizard() => GetWindow(typeof(ReplaceWithPrefab));

	private void OnEnable()
	{
		so = new SerializedObject(this);

		propCopyScale = so.FindProperty("CopyScale");
		propCopyRotation = so.FindProperty("CopyRotation");
		propPrefab = so.FindProperty("Prefab");
	}

	private void OnGUI()
	{
		so.Update();

		EditorGUILayout.PropertyField(propCopyScale);
		EditorGUILayout.PropertyField(propCopyRotation);
		EditorGUILayout.PropertyField(propPrefab);

		so.ApplyModifiedProperties();

		if (GUILayout.Button("Replace"))
		{
			ReplaceSelectedWithPrefab();
		}
	}

	void ReplaceSelectedWithPrefab()
	{
		GameObject[] goCache = Selection.gameObjects;

		foreach (GameObject go in goCache)
		{
			Undo.IncrementCurrentGroup();

			GameObject newObject = (GameObject) PrefabUtility.InstantiatePrefab(Prefab, go.transform.parent);
			//Undo.RegisterCreatedObjectUndo(newObject, "Create Prefab Instance");
			Selection.activeObject = newObject;

			Undo.RegisterCompleteObjectUndo(go, "Update transform values");

			newObject.transform.position = go.transform.position;
			if (propCopyScale.boolValue)
			{
				newObject.transform.localScale = go.transform.localScale;
			}
			if (propCopyRotation.boolValue)
			{
				newObject.transform.rotation = go.transform.rotation;
			}

			Undo.DestroyObjectImmediate(go);

			Undo.SetCurrentGroupName("Replace GameObject With Prefab");
		}
	}
}
