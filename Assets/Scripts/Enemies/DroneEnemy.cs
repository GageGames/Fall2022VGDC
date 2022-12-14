using Pathfinding;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]

// MagneticEntity get tethers (active tethers), if more than 0 start a timer.

public class DroneEnemy : MonoBehaviour
{
	[SerializeField] float timeNeeded;
	[SerializeField] GameObject droneVisualObject;
	[SerializeField] GameObject droneParticlesPrefabDesert;
	[SerializeField] GameObject droneParticlesPrefabLab;

	public GameObject thisDronesParticles;

	[Expandable]
	public PathfindingBehaviorConfig pathfindingBehaviorConfig;

	float timer;
	float baseDroneHeight = 0.6f;

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


		GameObject particlePrefab = droneParticlesPrefabLab;
		if(SceneManager.GetActiveScene().name == "Desert")
		{
			 particlePrefab = droneParticlesPrefabDesert;
		}
		
		GameObject particles = Instantiate(particlePrefab, transform.position + Vector3.down*0.6f, Quaternion.identity) as GameObject;
		thisDronesParticles = particles;
		thisDronesParticles.transform.rotation = Quaternion.Euler(-90,0,0);
		aIDestinationSetter.target = player;
		aIPath.maxSpeed = pathfindingBehaviorConfig.MovementSpeed;

		timer = timeNeeded;
	}

	void Update()
	{
		if(thisDronesParticles != null){
			thisDronesParticles.transform.position = transform.position + Vector3.down * 0.6f;
		}
		//Check every frame is a tether is still attached (if so keep falling)
		foreach (Anchor anchor in magneticEntity.RetrieveActiveAnchors())
		{
			if (anchor.GetTethers().Count > 0)
			{
				if(DOTween.IsTweening(droneVisualObject.transform) == true){
					droneVisualObject.transform.DOKill();
				}
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
	private void OnEnable()
    {
        healthEntity.OnDeath.AddListener(DeathEffects);
    }

    private void OnDisable()
    {
        healthEntity.OnDeath.RemoveListener(DeathEffects);
    }
	void DeathEffects(HealthEntity HE)
	{
		var em = thisDronesParticles.GetComponent<ParticleSystem>().emission;
		em.enabled = false;
		Destroy(thisDronesParticles,2.0f);
	}

	void Falling()
	{
		healthEntity.ApplyDamage(999);
	}
}
