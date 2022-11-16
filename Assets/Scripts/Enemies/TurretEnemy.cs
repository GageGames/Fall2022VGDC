using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
	Transform player;

	void Start()
	{
		player = ((Player)FindObjectOfType(typeof(Player))).transform;
	}

	void Update()
	{
		transform.LookAt(player.position);
	}
}
