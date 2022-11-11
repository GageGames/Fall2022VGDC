using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// A point that can be tethered onto

public class Anchor
{
	public Vector3 Position { get; private set; }

	public UnityEvent<Tether> OnAttachTether = new UnityEvent<Tether>();
	public UnityEvent<Tether> OnDetachTether = new UnityEvent<Tether>();

	protected List<Tether> attachedTethers = new List<Tether>();

	public Anchor(Vector3 position)
	{
		Position = position;
	}

	public void AddTether(Tether tether)
	{
		attachedTethers.Add(tether);
		OnAttachTether?.Invoke(tether);
	}

	public void RemoveTether(Tether tether)
	{
		attachedTethers.Remove(tether);
		OnDetachTether?.Invoke(tether);
	}

	public List<Tether> GetTethers()
	{
		return attachedTethers;
	}

	public void SetPosition(Vector3 pos)
	{
		Position = pos;
	}

	public void DetachAllTethers()
	{
		Tether[] tetherCache = attachedTethers.ToArray();
		foreach (Tether tether in tetherCache)
		{
			RemoveTether(tether);
		}
	}
}
