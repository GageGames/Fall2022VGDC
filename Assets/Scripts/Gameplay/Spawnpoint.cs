using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class Spawnpoint : MonoBehaviour
{
    public float SpawnRadius = 3f;

    public SpawnpointType spawnpointType;
    public SpawnpointEditorPreviewData editorPreviewData;

    public Color customDiscColor;
    public float customDiscThickness;

    private void OnEnable () => SpawnpointManager.ActiveSpawnpoints.Add (this);
    private void OnDisable () => SpawnpointManager.ActiveSpawnpoints.Remove (this);

    private void OnValidate ()
    {
        ApplyCustomEditorPreviewData ();
    }

    public void ApplySpawnpointType ()
    {
        if (spawnpointType != SpawnpointType.Custom)
        {
            editorPreviewData = Resources.Load ($"Data/Spawnpoints/{spawnpointType.ToString ()}") as SpawnpointEditorPreviewData;
        }
        else
        {
            editorPreviewData = Instantiate(Resources.Load ($"Data/Spawnpoints/Neutral") as SpawnpointEditorPreviewData);
        }

        customDiscColor = editorPreviewData.discColor;
        customDiscThickness = editorPreviewData.discThickness;
    }

    public void ApplyCustomEditorPreviewData ()
    {
        editorPreviewData.discColor = customDiscColor;
        editorPreviewData.discThickness = customDiscThickness;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmosSelected ()
    {
        Color colorCache = Handles.color;
        Handles.color = editorPreviewData.discColor;
        Handles.DrawWireDisc (transform.position, transform.up, SpawnRadius, editorPreviewData.discThickness);
        Handles.color = colorCache;
    }
#endif
}
