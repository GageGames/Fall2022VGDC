using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
	Transform player;
	[SerializeField] GameObject turretHead;
	void Start()
	{
		player = FindObjectOfType<Player>().transform;
	}

	void Update()
	{
		turretHead.transform.LookAt(player.position);
	}
}
