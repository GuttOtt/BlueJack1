using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBetAI : MonoBehaviour, IBetInputSystem {
	private BetDele call;
	private BetDele raise;
	private BetDele fold;

	public void SetInput(BetDele call, BetDele raise, BetDele fold) {
		this.call += call;
		this.raise += raise;
		this.fold += fold;
	}	

	public void StartGettingInput() {
		int r = Random.Range(0, 10);

		if (8 <= r) {
			raise();
			return;
		}
		if (1 <= r) {
			call();
			return;
		}
		fold();
	}

	public void EndGettingInput() {

	}
}
