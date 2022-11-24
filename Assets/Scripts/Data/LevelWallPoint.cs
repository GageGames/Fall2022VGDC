using UnityEngine;

[CreateAssetMenu(fileName = "Test", menuName = "Configs/Test Wall Point")]
[System.Serializable]
public class LevelWallPoint : ScriptableObject
{
	public Vector3 Position;
	public int HotControlID;
}