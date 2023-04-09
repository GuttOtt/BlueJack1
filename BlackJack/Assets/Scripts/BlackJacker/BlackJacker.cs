using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJacker : MonoBehaviour {
	[SerializeField] private HPGraphic hpGraphic;
	private HP hp;
	private Hand hand;
	private Deck deck;

	private void Awake() {
		hp = new HP(100);
		hand = GetComponent<Hand>();
		deck = GetComponent<Deck>();
		hpGraphic.SetHP(hp);
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
