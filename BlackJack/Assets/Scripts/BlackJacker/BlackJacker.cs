using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJacker : MonoBehaviour {
	private HP hp;
	private Hand hand;
	private Deck deck;
	public bool IsBursted { get => hand.IsBursted();	}

	private void Awake() {
		hp = GetComponent<HP>();
		hand = GetComponent<Hand>();
		deck = GetComponent<Deck>();
	}

	public void StartSetting() {
		deck.ClosedHit();
		deck.ClosedHit();
		deck.OpenHit();
	}

	public void Hit() {
		deck.OpenHit();
	}
}