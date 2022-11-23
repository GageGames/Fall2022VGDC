using MyBox;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximityFader : MonoBehaviour
{
	public AnimationCurve DistToFadeCurve;
	public List<Image> FadeTargets = new List<Image>();

	float[] fadeMaxima;
	Transform player;

	private void Start()
	{
		player = FindObjectOfType<Player>().transform;

		fadeMaxima = new float[FadeTargets.Count];

		int itr = 0;
		foreach (Image target in FadeTargets)
		{
			fadeMaxima[itr] = target.color.a;
			itr++;
		}
	}

	void Update()
	{
		float fade = DistToFadeCurve.Evaluate(Vector3.Distance(transform.position, player.position));

		int itr = 0;
		foreach (Image target in FadeTargets)
		{
			target.SetAlpha(fade * fadeMaxima[itr]);
			itr++;
		}
	}
}
