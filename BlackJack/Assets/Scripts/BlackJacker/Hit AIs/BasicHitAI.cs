using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHitAI : MonoBehaviour, IHitAI {
	private Hand hand;

	private void Awake() {
		hand = GetComponent<Hand>();
	}

	public HitDecision DecideHit() {
		if (hand.GetTotal() < 17)
			return HitDecision.Hit;

		return HitDecision.Stay;
	}
}
