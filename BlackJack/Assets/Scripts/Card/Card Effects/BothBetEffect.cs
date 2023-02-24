using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothBetEffect : MonoBehaviour, ICardEffect {
	[SerializeField] private float percent;

	public void Activate() {
		Gambler owner = transform.parent.GetComponent<Card>().owner;
		owner.BetMore(percent);
		owner.opponent.BetMore(percent);
	}
}
