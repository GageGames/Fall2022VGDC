using UnityEngine;

[RequireComponent(typeof(Gun))]
public class GunVFX : MonoBehaviour
{
	[SerializeField] GameObject tetherBeamPullEffectPrefab;
	[SerializeField] GameObject tetherBeamPushEffectPrefab;
	[SerializeField] GameObject tetherHitPullEffectPrefab;
	[SerializeField] GameObject tetherHitPushEffectPrefab;

	[SerializeField] Transform pullTetherOriginTransform;
	[SerializeField] Transform pushTetherOriginTransform;

	Gun gun;
	LineRenderer ActiveTetherBeamEffect;

	bool active = false;
	bool pulling = false;

	private void Start()
	{
		gun = GetComponent<Gun>();

		gun.OnFire.AddListener(SpawnTetherEffects);
		gun.OnDetach.AddListener(DestroyTetherEffect);
	}

	void LateUpdate()
	{
		if (active)
		{
			UpdateTetherEffectPoints();
		}
	}

	void UpdateTetherEffectPoints()
	{
		ActiveTetherBeamEffect.SetPosition(0, pulling ? pullTetherOriginTransform.position : pushTetherOriginTransform.position);
		ActiveTetherBeamEffect.SetPosition(1, gun.ActiveTether.Recipient.Position);
	}

	void SpawnTetherEffects(FireResult result)
	{
		Vector3 diff = gun.ActiveTether.Sender.Position - gun.ActiveTether.Recipient.Position;
		diff.y = 0;
		Quaternion dir = Quaternion.LookRotation(diff);
		Debug.DrawRay(result.SelectedTarget.Position, dir * Vector3.forward * 5f, Color.yellow, 5);

		pulling = gun.ActiveTether.Strength >= 0;

		if (pulling)
		{
			Instantiate(tetherHitPullEffectPrefab, result.SelectedTarget.Position, dir);
			ActiveTetherBeamEffect = Instantiate(tetherBeamPullEffectPrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
		}
		else
		{
			Instantiate(tetherHitPushEffectPrefab, result.SelectedTarget.Position, dir);
			ActiveTetherBeamEffect = Instantiate(tetherBeamPushEffectPrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();
		}

		active = true;
	}

	void DestroyTetherEffect()
	{
		if (ActiveTetherBeamEffect != null)
		{
			// TODO: Transform.root is dangerous, use something more stable
			Destroy(ActiveTetherBeamEffect.transform.root.gameObject);
		}
		active = false;
	}
}
