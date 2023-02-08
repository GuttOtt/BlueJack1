using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : MonoBehaviour {
	private List<Card> cards = new List<Card>();	
	private Hand hand;
	[SerializeField] private GameObject deckParent;

	private void Awake() {
		hand = transform.GetComponent<Hand>();
	}

	public void Hit() {
		if (cards.Count < 1) {
			Debug.Log("더이상 카드를 뽑을 수 없습니다");
			return;
		}

		hand.AddCard(cards[cards.Count - 1]);
		cards.RemoveAt(cards.Count - 1);
	}

	public void AddCard(Card card) {
		cards.Add(card);
		card.transform.SetParent(deckParent.transform);
		card.transform.localPosition = Vector2.zero;
	}

	public void SetHand(Hand hand) {//For Debug?
		this.hand = hand;
	}

	public void Shuffle() {
		cards.Shuffle();
	}

	
}

