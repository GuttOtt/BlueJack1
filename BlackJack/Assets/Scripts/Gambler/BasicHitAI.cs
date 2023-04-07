using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Old {

public class BasicHitAI : MonoBehaviour, IHitInputSystem {
	private Hand hand;
	private HitDele hit;
	private HitDele stay;
	private OpponentDialogue dialogue;

	private void Awake() {
		hand = GetComponent<Hand>();
		dialogue = GetComponent<OpponentDialogue>();
	}

	public void SetInput(HitDele hit, HitDele stay) {
		this.hit += hit;
		this.stay += stay;
	}

	public void StartGettingInput() {
		if (hand.GetTotal() < 18) {
			hit();
			dialogue.Hit();
		}
		else {
			stay();
			dialogue.Stay();
		}
	}

	public void EndGettingInput() {

	}
}
}