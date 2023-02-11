using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatioBettingAI : MonoBehaviour, IBettingClient {
    [SerializeField] private BettingManager manager;
	[SerializeField] private Betting betting;
	[SerializeField] private OpponentDialogue dialogue;

	public void StartBetting() {
		dialogue.ConsideringText();

		StartCoroutine(BettingDelay());
	}

	private IEnumerator BettingDelay() {
		yield return new WaitForSeconds(2f);
		DecideBetAction();
		EndBetting();
	}

	public void EndBetting() {
		manager.ChangeTurn();
	}

	private void DecideBetAction() {
		int r = Random.Range(0, 3);

		if (r == 0) {
			if (betting.RaiseRatio(2f)) {
				dialogue.RaiseText();
				manager.CountRaise();
				return;
			}
		}
		if (r <= 1) {
			if (betting.Call()) {
				dialogue.CallText();
				manager.CountCall();
				return;
			}
		}//
		dialogue.FoldText();
		manager.EndByFold(this.betting);
	}
}

