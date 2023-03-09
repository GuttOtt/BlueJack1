using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase: MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	private Gambler player;
	private Gambler opponent;
	private Wallet roundPot;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.GetPlayer;
		opponent = turnSystem.GetOpponent;
		roundPot = gameObject.GetComponent<Wallet>();
	}

	public void Handle() {
		EndRound(turnSystem.loser);
	}

	public void EndRound(Gambler loser) {
		loser.LoseProcess(turnSystem.basePotMoney, roundPot);
		GetOther(loser).WinProcess(turnSystem.basePotMoney, roundPot);

		player.NewRoundInitialize();
		opponent.NewRoundInitialize();
		gameObject.GetComponent<BetPhase>().NewRoundInitialize();

		StartCoroutine(EndRoundDelay());
	}

	private IEnumerator EndRoundDelay() {
		yield return new WaitForSeconds(2f);
		
		turnSystem.ToStartPhase();
	}

	private Gambler GetOther(Gambler gambler) {
		if (gambler == player) return opponent;
		return player;
	}
}