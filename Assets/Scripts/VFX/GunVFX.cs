using UnityEngine;

[RequireComponent(typeof(Gun))]
public class GunVFX : MonoBehaviour
{
	[SerializeField] GameObject tetherBeamPullEffectPrefab;
	[SerializeField] GameObject tetherBeamPushEffectPrefab;
	[SerializeField] GameObject tetherHitPullEffectPrefab;

	Gun gun;
	LineRenderer ActiveTetherBeamEffect;

	private void Start()
	{
		gun = GetComponent<Gun>();
	}

	private void OnEnable()
	{
		gun.OnFire.AddListener(SpawnTetherHitEffect);
		gun.OnFire.AddListener(SpawnTetherEffect);
		gun.OnDetach.AddListener(DestroyTetherEffect);
	}

	private void OnDisable()
	{
		gun.OnFire.RemoveListener(SpawnTetherHitEffect);
		gun.OnFire.RemoveListener(SpawnTetherEffect);
		gun.OnDetach.RemoveListener(DestroyTetherEffect);
	}

	void FixedUpdate()
	{
		if (gun.ActiveTether != null)
		{
			UpdateTetherEffectPoints();
		}
	}

	void UpdateTetherEffectPoints()
	{
		ActiveTetherBeamEffect.SetPosition(0, gun.ActiveTether.Sender.Position);
		ActiveTetherBeamEffect.SetPosition(1, gun.ActiveTether.Recipient.Position);
	}

	void SpawnTetherHitEffect(FireResult result)
	{
		if (tetherHitPullEffectPrefab != null)
		{
			Instantiate(tetherHitPullEffectPrefab, result.CursorOrigin, Quaternion.identity);
		}
	}

	void SpawnTetherEffect(FireResult result)
	{
		if (gun.ActiveTether.Strength >= 0)
		{
			ActiveTetherBeamEffect = Instantiate(tetherBeamPullEffectPrefab, Vector3.zero, Quaternion.identity).GetComponentInChildren<LineRenderer>();
		}
		else
		{
			ActiveTetherBeamEffect = Instantiate(tetherBeamPushEffectPrefab, Vector3.zero, Quaternion.identity).GetComponentInChildren<LineRenderer>();
		}
	}

	void DestroyTetherEffect()
	{
		if (ActiveTetherBeamEffect != null)
		{
			Destroy(ActiveTetherBeamEffect.transform.root.gameObject);
		}
	}
}
