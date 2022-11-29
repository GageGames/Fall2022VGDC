using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Gun))]
public class Player : MonoBehaviour
{
	[HideInInspector]
	public UnityEvent<State> OnStateChange = new UnityEvent<State>();

	public enum State
	{
		Idle,
		TetherPulling,
		TetherPushing
	}

	public State StateMachine {
		get
		{
			return stateMachine;
		}
		set
		{
			stateMachine = value;
			OnStateChange?.Invoke(stateMachine);
		}
	}

	protected State stateMachine = State.Idle;

	static Bounds viewBounds = new Bounds(new Vector2 (0.5f, 0.5f), Vector3.one * 1.2f);

	Gun gun;
	GameplayTuningValues val;

	private void Awake()
	{
		gun = GetComponent<Gun>();
	}

	private void Start()
	{
		val = Singleton<GlobalData>.Instance.GlobalConfigInstance.PrimaryGameplayTuningValues;
		GetComponent<HealthEntity>().OnDeath.AddListener(OnDeath);
	}

	void Update()
	{
		switch (StateMachine)
		{
			case State.Idle:
				gun.DetectionRadius = val.PlayerGunDetectionRadius;
				gun.DetectionMask = val.PlayerGunDetectionMask;
				if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
				{
					FireGun(val.PlayerGunPullStrength, true);
					StateMachine = State.TetherPulling;
				} else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Q))
				{
					FireGun(val.PlayerGunPushStrength, false);
					StateMachine = State.TetherPushing;
				}
				break;
			case State.TetherPulling:
				if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.E))
				{
					gun.Detach();
					StateMachine = State.Idle;
				}
				DetachIfOutsideView();
				break;
			case State.TetherPushing:
				if (!Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.Q))
				{
					gun.Detach();
					StateMachine = State.Idle;
				}
				DetachIfOutsideView();
				break;
		}
	}

	void FireGun(float strength, bool pulling)
	{
		gun.Strength = strength;

		// TODO: replace with InputHandler.GetMouseWorldPos
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			gun.Fire(hit.point, pulling);
		}
	}

	void DetachIfOutsideView()
	{
		if (gun.ActiveTether == null) return;
		
		Vector3 pos = gun.ActiveTether.Recipient.Position;
		Vector2 projectedPos = Camera.main.WorldToViewportPoint(pos);

		if (!viewBounds.Contains(projectedPos))
		{
			//print($"{projectedPos} is outside view bounds {viewBounds}!");
			gun.Detach();
		}
	}

	void OnDeath(HealthEntity healthEntity)
	{
		Singleton<GameManager>.Instance.EndGame();
	}
}
