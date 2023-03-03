using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBetAI : MonoBehaviour, IBetInputSystem {
	private BetDele call;
	private BetDele raise;
	private BetDele fold;
	private OpponentDialogue dialogue;

	private void Awake() {
		dialogue = GetComponent<OpponentDialogue>();
	}

	public void SetInput(BetDele call, BetDele raise, BetDele fold) {
		this.call += call;
		this.raise += raise;
		this.fold += fold;
	}	

	public void StartGettingInput() {
		StartCoroutine(InputDelay());
	}
		

	private IEnumerator InputDelay() {
		yield return new WaitForSeconds(2f);

		GetInput();
	}

	private void GetInput() {
		int r = Random.Range(0, 10);

		if (8 <= r && raise()) {
			dialogue.Raise();
			return;
		}
		if (0 <= r && call()) {
			dialogue.Call();
			return;
		}
		fold();
		dialogue.Fold();
	}

	public void EndGettingInput() {

	}
}
