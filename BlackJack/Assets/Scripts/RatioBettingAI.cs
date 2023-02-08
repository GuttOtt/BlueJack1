using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatioBettingAI : MonoBehaviour, IBettingClient {
    [SerializeField] private BettingManager manager;
	[SerializeField] private Betting betting;

	public void StartBetting() {
		Debug.Log("흠...");

		StartCoroutine(BettingDelay());
	}

	private IEnumerator BettingDelay() {
		yield return new WaitForSeconds(1f);
		DecideBetAction();
		EndBetting();
	}

	public void EndBetting() {
		Debug.Log("자네 턴일세.");
		manager.ChangeTurn();
	}

	private void DecideBetAction() {
		int r = Random.Range(0, 3);

		if (r == 0) {
			if (betting.RaiseRatio(2f)) {
				manager.CountRaise();
				return;
			}
		}
		if (r <= 1) {
			if (betting.Call()) {
				manager.CountCall();
				return;
			}
		}
		Debug.Log("AI Folds");
		manager.EndByFold(this.betting);
	}
}

