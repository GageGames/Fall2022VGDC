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
		foreach (GameObject go in Selection.gameObjects)
		{
			GameObject newObject;
			newObject = (GameObject)PrefabUtility.InstantiatePrefab(Prefab);
			newObject.transform.position = go.transform.position;
			newObject.transform.rotation = go.transform.rotation;
			newObject.transform.parent = go.transform.parent;

			DestroyImmediate(go);
		}
	}
}
