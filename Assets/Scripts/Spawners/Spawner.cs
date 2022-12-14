using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Spawns a random quantity of prefabs within min and max quantities, placed within a min and max radius

[ExecuteAlways]
public class Spawner : MonoBehaviour
{
	// Spawning is equally likely to occur in the entire area within the circle defined by maxRadius.
	public float MaxSpawnRadius = 2f;
	public float MinSpawnRadius = 0f;

	// Spawns at least min and at most max entities
	public int MaxSpawnQuantity;
	public int MinSpawnQuantity;

	// A reference to the prefab of the object to spawn
	public GameObject SpawnPrefab;

	public SpawnType spawnType;
	public SpawnerEditorPreviewData editorPreviewData;

	public Color customDiscColor;
	public float customDiscThickness;

	private void OnEnable() => SpawnManager.CurrentSpawners.Add(this);
	private void OnDisable() => SpawnManager.CurrentSpawners.Remove(this);

	public List<GameObject> Spawn()
	{
		List<GameObject> output = new List<GameObject>();

		Vector3 pos = gameObject.transform.position;
		for (int i = 0; i < Random.Range(MinSpawnQuantity, MaxSpawnQuantity); ++i)
		{
			float r = Mathf.Sqrt(Random.Range(MinSpawnRadius * MinSpawnRadius, MaxSpawnRadius * MaxSpawnRadius));
			float theta = Random.Range(0.0f, 2.0f * Mathf.PI);

			float relativeY = r * Mathf.Sin(theta);
			float relativeX = r * Mathf.Cos(theta);

			output.Add(Instantiate(SpawnPrefab, new Vector3(pos.x + relativeX, pos.y, pos.z + relativeY), SpawnPrefab.transform.rotation));
		}

		return output;
	}

	private void OnValidate()
	{
		ApplyCustomEditorPreviewData();
	}

	public void ApplySpawnType()
	{
		if (spawnType != SpawnType.Custom)
		{
			editorPreviewData = Resources.Load($"Data/Spawners/{spawnType.ToString()}") as SpawnerEditorPreviewData;
		}
		else
		{
			editorPreviewData = Instantiate(Resources.Load($"Data/Spawners/Neutral") as SpawnerEditorPreviewData);
		}

		customDiscColor = editorPreviewData.discColor;
		customDiscThickness = editorPreviewData.discThickness;
	}

	public void ApplyCustomEditorPreviewData()
	{
		editorPreviewData.discColor = customDiscColor;
		editorPreviewData.discThickness = customDiscThickness;
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		if (!editorPreviewData) return;

		Color colorCache = Handles.color;
		Handles.color = editorPreviewData.discColor;
		Handles.DrawWireDisc(transform.position, transform.up, MaxSpawnRadius, editorPreviewData.discThickness);
		Handles.color = editorPreviewData.discColor * new Color (1, 1, 1, 0.7f);
		Handles.DrawWireDisc(transform.position, transform.up, MinSpawnRadius, editorPreviewData.discThickness * 0.5f);
		Handles.color = colorCache;
	}
#endif
}
