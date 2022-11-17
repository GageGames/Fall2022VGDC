using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

// Makes the camera smoothly follow the player and zoom depending on the player's speed, in a way that's configurable

public class CameraController : MonoBehaviour
{
	[SerializeField] float smoothTime;

	[Space]

	[SerializeField] Vector2 positionOffset;
	[SerializeField] float baseYPos;
	[SerializeField] float maxYOffset;
	[SerializeField] float baseFOV;
	[SerializeField] float maxFOVOffset;

	[Space]

	[SerializeField] float zoomCapSpeed;
	[SerializeField] float zoomFloorSpeed = 0;
	[SerializeField] AnimationCurve zoomEaseCurve;

	[Space]

	[SerializeField] bool camSpeedCapped;
	[ConditionalField("camSpeedCapped")]
	[SerializeField] float maxCamSpeed = 0;

	[SerializeField] bool smoothFOV;
	[ConditionalField("smoothFOV")]
	[SerializeField] float FOVLerpSpeed = 1;

	Camera cam;
	Transform player;
	Rigidbody rb;

	float yOffset;
	float FOVOffset;
	Vector3 velocity = Vector3.zero;

    void Start()
    {
		cam = GetComponent<Camera>();
		player = FindObjectOfType<Player>()?.transform;

		if (!player) return;

		rb = player.GetComponent<PhysicsData>()?.rb;
    }

    void Update()
    {
		if (!player || !rb) return;

		float zoomFactor = zoomEaseCurve.Evaluate((rb.velocity.magnitude-zoomFloorSpeed) / (zoomCapSpeed-zoomFloorSpeed));
		//set target y offset
		yOffset = zoomFactor * maxYOffset;

		//set fov
		if (smoothFOV)
		{
			FOVOffset = Mathf.Lerp(FOVOffset, zoomFactor * maxFOVOffset, FOVLerpSpeed);
		}
		else {
			FOVOffset = zoomFactor * maxFOVOffset;
		}
		cam.fieldOfView = baseFOV + FOVOffset;

		//move to target pos
		if (camSpeedCapped)
		{
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x + positionOffset.x, baseYPos + yOffset, player.position.z + positionOffset.y), ref velocity, smoothTime, maxCamSpeed);
		} else {
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x + positionOffset.x, baseYPos + yOffset, player.position.z + positionOffset.y), ref velocity, smoothTime);
		}
	}
}
