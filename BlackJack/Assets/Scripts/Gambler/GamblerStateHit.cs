using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerStateHit : MonoBehaviour, IGamblerState {
	private Gambler gambler;
	private Deck deck;
	private Hand hand;
	private IHitInputSystem hitInput;
	private HitPhase hitPhase;

	private void Awake() {
		gambler = GetComponent<Gambler>();
		deck = GetComponent<Deck>();
		hand = GetComponent<Hand>();
		hitInput = (IHitInputSystem) GetComponent<IHitInputSystem>();
		hitPhase = FindObjectOfType<HitPhase>() as HitPhase;

		HitDele hit = Hit;
		HitDele stay = Stay;
		hitInput.SetInput(hit, stay);
	}

	public void StartState() {
		if (gambler.isStayed) {
			hitPhase.EndTurn();
			return;
		}
		hitInput.StartGettingInput();
	}

	public void Hit() {
		if (gambler.isStayed)
			return;
		
		deck.OpenHit();
		EndHitting();
		if (hand.IsBursted()) {
			hitPhase.EndTurnByBurst(this.gambler);
			return;
		}
		hitPhase.EndTurn();
	}

	public void Stay() {
		if (!gambler.isStayed){
			gambler.isStayed = true;
			EndHitting();
			hitPhase.EndTurn();
		}
	}

	private void EndHitting() {
		hitInput.EndGettingInput();
	}
}