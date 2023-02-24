using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardEffect : MonoBehaviour, ICardEffect{
	[SerializeField] Card newCard;

	public void Activate() {
		Card card = transform.parent.GetComponent<Card>();
		Hand hand = card.owner.GetComponent<Hand>();

		hand.AddCard(newCard);
	}
}
