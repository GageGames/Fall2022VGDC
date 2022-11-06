using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnpointType
{
    Enemy,
    Friendly,
    Powerup,
    Neutral,
    Custom
}

[CreateAssetMenu (menuName = "EditorPreviewData/Spawnpoint")]
[System.Serializable]
public class SpawnpointEditorPreviewData : ScriptableObject
{
    public float discThickness = 0.5f;
    public Color discColor = Color.white;
}
