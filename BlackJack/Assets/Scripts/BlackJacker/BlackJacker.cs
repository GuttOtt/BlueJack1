using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJacker : MonoBehaviour {
	private HP hp;
	private Hand hand;
	private Deck deck;
	public bool IsBursted { get => hand.IsBursted();	}

	private void Awake() {
		hp = new HP(100);
		hand = GetComponent<Hand>();
		deck = GetComponent<Deck>();
	}

	public void StartSetting() {
		hand.DiscardAll();
		deck.ClosedHit();
		deck.ClosedHit();
		deck.OpenHit();
	}

	public void Hit() {
		deck.OpenHit();
	}

	public void TakeDamage(int damage) {
		hp.TakeDamage(damage);
	}
}
