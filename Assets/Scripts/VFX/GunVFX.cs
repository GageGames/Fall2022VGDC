using UnityEngine;

[RequireComponent(typeof(Gun))]
public class GunVFX : MonoBehaviour
{
	[SerializeField] GameObject tetherBeamPullEffectPrefab;
	[SerializeField] GameObject tetherBeamPushEffectPrefab;
	[SerializeField] GameObject tetherHitPullEffectPrefab;
	[SerializeField] GameObject tetherHitPushEffectPrefab;

	Gun gun;
	LineRenderer ActiveTetherBeamEffect;

	private void Start()
	{
		gun = GetComponent<Gun>();

		gun.OnFire.AddListener(SpawnTetherEffects);
		gun.OnDetach.AddListener(DestroyTetherEffect);
	}

	void LateUpdate()
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

	void SpawnTetherEffects(FireResult result)
	{
		Vector3 diff = gun.ActiveTether.Sender.Position - gun.ActiveTether.Recipient.Position;
		diff.y = 0;
		Quaternion dir = Quaternion.LookRotation(diff);
		Debug.DrawRay(result.SelectedTarget.Position, dir * Vector3.forward * 5f, Color.yellow, 5);
		if (gun.ActiveTether.Strength >= 0)
		{
			Instantiate(tetherHitPullEffectPrefab, result.SelectedTarget.Position, dir);
			ActiveTetherBeamEffect = Instantiate(tetherBeamPullEffectPrefab, Vector3.zero, Quaternion.identity).GetComponentInChildren<LineRenderer>();
		}
		else
		{
			Instantiate(tetherHitPushEffectPrefab, result.SelectedTarget.Position, dir);
			ActiveTetherBeamEffect = Instantiate(tetherBeamPushEffectPrefab, Vector3.zero, Quaternion.identity).GetComponentInChildren<LineRenderer>();
		}
	}

	void DestroyTetherEffect()
	{
		if (ActiveTetherBeamEffect != null)
		{
			// TODO: Transform.root is dangerous, use something more stable
			Destroy(ActiveTetherBeamEffect.transform.root.gameObject);
		}
	}
}
