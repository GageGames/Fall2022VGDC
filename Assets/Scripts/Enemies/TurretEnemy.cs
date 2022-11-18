using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
	Transform player;

	void Start()
	{
		player = FindObjectOfType<Player>().transform;
	}

	void Update()
	{
		transform.LookAt(player.position);
	}
}
