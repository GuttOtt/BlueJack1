using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEffect : MonoBehaviour, ICardEffect {
    [SerializeField] CardIcon squireIconPrefab;

	public void Activate() {
		Card card = transform.parent.GetComponent<Card>();
		Hand hand = card.owner.GetComponent<Hand>();

		GameObject obj = new GameObject("Card");
		Card newCard = obj.AddComponent<Card>() as Card;
		newCard.AddIcon(squireIconPrefab);
		newCard.Initialize(0, card.GetSuit());
		newCard.owner = card.owner;
		newCard.IsFront = true;

		hand.AddCard(newCard);
	}
}
