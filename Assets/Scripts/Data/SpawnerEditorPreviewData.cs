using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    Enemy,
    Friendly,
    Powerup,
    Neutral,
    Custom
}

[CreateAssetMenu (menuName = "EditorPreviewData/Spawnpoint")]
[System.Serializable]
public class SpawnerEditorPreviewData : ScriptableObject
{
    public float discThickness = 0.5f;
    public Color discColor = Color.white;
}
