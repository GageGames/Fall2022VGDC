using UnityEditor;
using UnityEngine;

//Pushes movable objects in a direction. (Functions like a fan)
public class Forcefield : MonoBehaviour
{
	public float strength;
	public Vector3 direction;
	public AudioClip SoundEffect;

	static ImpulseSourceType type = new ImpulseSourceType(ImpulseSourceTag.Field);

	private void Awake()
	{
		SFXManager.PlayLoopedSound(SoundEffect, () => false, transform.position, transform);
	}

	private void OnTriggerStay(Collider other)
	{
		IImpulseReceiver receiver = other.GetComponentInParent<IImpulseReceiver>();
		if (receiver != null)
		{
			receiver.ApplyImpulse(direction, strength * Time.deltaTime, type);
		}
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Color colorCache = Handles.color;
		Handles.color = Color.Lerp(Color.green, Color.red, Mathf.Clamp01(strength / 8f));
		Handles.DrawLine(transform.position, transform.position + direction * strength, 3 + 5f * Mathf.Clamp01(strength / 8f));
		Handles.color = colorCache;
	}
#endif
}
