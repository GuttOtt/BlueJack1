using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase: MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	protected Gambler player;
	protected Gambler opponent;
	private Wallet roundPot;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.GetPlayer;
		opponent = turnSystem.GetOpponent;
		roundPot = gameObject.GetComponent<Wallet>();
	}

	public virtual void Handle() {
		StartCoroutine(EndRound());
	}

	public IEnumerator EndRound() {
		Gambler loser = turnSystem.loser;
		yield return StartCoroutine( loser.LoseProcess(turnSystem.basePotMoney, roundPot) );
		yield return StartCoroutine( GetOther(loser).WinProcess(turnSystem.basePotMoney, roundPot) );

		player.NewRoundInitialize();
		opponent.NewRoundInitialize();
		gameObject.GetComponent<BetPhase>().NewRoundInitialize();
		
		yield return new WaitForSeconds(1f);

		turnSystem.ToStartPhase();
	}

	private Gambler GetOther(Gambler gambler) {
		if (gambler == player) return opponent;
		return player;
	}
}