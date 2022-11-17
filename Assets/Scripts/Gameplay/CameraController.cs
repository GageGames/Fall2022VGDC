using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

// makes the camera smoothly follow the player and zoom depending on the player's speed, in a way that's configurable

public class CameraController : MonoBehaviour
{
	[SerializeField] private float smoothTime;
	[SerializeField] private Vector2 positionOffset;
	[SerializeField] private float baseYPos;
	[SerializeField] private float maxYOffset;
	[SerializeField] private float baseFOV;
	[SerializeField] private float maxFOVOffset;
	[SerializeField] private float zoomCapSpeed;
	[SerializeField] private float zoomFloorSpeed = 0;
	[SerializeField] private AnimationCurve zoomEaseCurve;
	
	[SerializeField] private bool camSpeedCapped;
	[ConditionalField("camSpeedCapped")]
	[SerializeField] private float maxCamSpeed = 0;
	[SerializeField] private bool smoothFOV;
	[ConditionalField("smoothFOV")]
	[SerializeField] private float FOVLerpSpeed = 1;

	private Camera cam;
	private Transform player;
	private Rigidbody rb;
	private float yOffset;
	private float FOVOffset;
	private Vector3 velocity = Vector3.zero;

    void Start()
    {
		cam = GetComponent<Camera>();
		player = FindObjectOfType<Player>().transform;
		rb = player.GetComponent<PhysicsData>().rb;
    }

    void Update()
    {
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
