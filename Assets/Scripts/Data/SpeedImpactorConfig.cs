using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeedImpactorDataConfig", menuName = "Configs/Speed Impactor Data Config")]
[System.Serializable]
public class SpeedImpactorConfig : ImpactorConfig
{
	[Tooltip("The amount of force needed to apply contact damage")]
	public float ImpactThreshold = 1;
}
