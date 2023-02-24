using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnightEffect : MonoBehaviour, ICardEffect {
	[SerializeField] float percent;

	public void Activate() {
		Gambler owner = transform.parent.GetComponent<Card>().owner;
		Hand hand = owner.GetComponent<Hand>();

		if (hand.GetTotal() != 21)
			return;

		owner.TakeMore(percent);
	}
}
