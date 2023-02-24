using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEffect : MonoBehaviour, ICardEffect {
	//OnDiscard
	public void Activate() {
		Card card = transform.parent.GetComponent<Card>();
		Discards discards = card.owner.GetComponent<Discards>();
		discards.Exclude(card);
	}
}
