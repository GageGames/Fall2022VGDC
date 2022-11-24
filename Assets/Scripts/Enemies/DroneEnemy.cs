using Pathfinding;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]

// MagneticEntity get tethers (active tethers), if more than 0 start a timer.

public class DroneEnemy : MonoBehaviour
{
	[SerializeField] float timeNeeded;
	[SerializeField] GameObject droneVisualObject;

	[Expandable]
	public PathfindingBehaviorConfig pathfindingBehaviorConfig;

	float timer;
	float baseDroneHeight = 0.43f;

	Transform player;
	MagneticEntity magneticEntity;
	HealthEntity healthEntity;
	AIDestinationSetter aIDestinationSetter;
	AIPath aIPath;

	private void Awake()
	{
		aIDestinationSetter = GetComponent<AIDestinationSetter>();
		aIPath = GetComponent<AIPath>();
		magneticEntity = GetComponent<MagneticEntity>();
		healthEntity = GetComponent<HealthEntity>();
	}

	void Start()
	{
		player = FindObjectOfType<Player>()?.transform;

		if (!player)
		{
			Debug.LogError("Failed to assign player!");
			return;
		}

		aIDestinationSetter.target = player;
		aIPath.maxSpeed = pathfindingBehaviorConfig.MovementSpeed;

		timer = timeNeeded;
	}

	void Update()
	{
		//Check every frame is a tether is still attached (if so keep falling)
		foreach (Anchor anchor in magneticEntity.RetrieveActiveAnchors())
		{
			if (anchor.GetTethers().Count > 0)
			{
				droneVisualObject.transform.DOKill();
				timer -= Time.deltaTime;
				float proportion = 1-(timer/timeNeeded);
				droneVisualObject.transform.localPosition = new Vector3(droneVisualObject.transform.localPosition.x, baseDroneHeight - proportion, droneVisualObject.transform.localPosition.z);
				//Debug.Log(timer);
				if (timer <= 0f)
				{
					//Debug.Log("Falling!");
					Falling();
				}
			}
			else
			{
				if(DOTween.IsTweening(droneVisualObject.transform) == false && timer != timeNeeded)
				{
					droneVisualObject.transform.DOLocalMoveY(baseDroneHeight, 0.5f).SetEase(Ease.InOutSine);
				}
				timer = timeNeeded;
			}
		}

	}

	void Falling()
	{
		healthEntity.ApplyDamage(999);
	}
}
