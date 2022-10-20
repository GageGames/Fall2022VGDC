using UnityEngine;
using static Unity.VisualScripting.Member;

// Connects two anchor points with a physics force and allows those anchor points to refer to each other

public class Tether : MonoBehaviour
{
	protected static Tether TetherPrefab;

	public Anchor Sender { get; private set; }
	public Anchor Recipient { get; private set; }

	// Current cached strength according to tether data
	public float Strength
	{
		get
		{
			if (!Paused)
			{
				return strength;
			}
			else
			{
				return 0f;
			}
		}
		set
		{
			strength = value;
		}
	}
	public bool Paused { get; private set; } = false;

	private float strength = 0;

	public static Tether CreateTether(Anchor source, Anchor destination)
	{
		//print("Creating Tether");

		GameObject obj = new GameObject("Tether");
		Tether newTether = obj.AddComponent<Tether>();
		newTether.Attach(source, destination);

		return newTether;
	}

	public void Attach(Anchor source, Anchor destination)
	{
		Sender = source;
		Recipient = destination;

		source.AddTether(this);
		destination.AddTether(this);
	}

	public void Pause()
	{
		Paused = true;
	}

	public void Resume()
	{
		Paused = false;
	}

	public void Detach()
	{
		Sender.RemoveTether(this);
		Recipient.RemoveTether(this);

		Sender = null;
		Recipient = null;

		// TODO: Recycle with object pooling
		Destroy(gameObject);
	}

	public Anchor GetOpposite(Anchor oneEnd)
	{
		if (oneEnd == null)
		{
			Debug.LogError("Getting the opposite anchor for a null anchor doesn't work");
			return null;
		}

		if (oneEnd == Sender)
		{
			return Recipient;
		}
		else if (oneEnd == Recipient)
		{
			return Sender;
		}
		else
		{
			Debug.LogError("Anchor not found in this tether");
			return null;
		}
	}
}
