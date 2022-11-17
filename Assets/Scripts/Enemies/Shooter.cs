using UnityEngine;

public class Shooter : MonoBehaviour
{
	public GameObject BulletPrefab;
	[SerializeField] float Interval;
	float timer = 0f;

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			Shoot();
			timer = Interval;
		}
	}
	void Shoot()
	{
		Instantiate(BulletPrefab, transform.position, transform.rotation);
	}
}
