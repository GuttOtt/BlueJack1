using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase: MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	protected Gambler player;
	protected Gambler opponent;
	[SerializeField] private Wallet roundPot;
	protected Money basePotMoney;
	protected Gambler loser;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.GetPlayer;
		opponent = turnSystem.GetOpponent;
		roundPot = gameObject.GetComponent<Wallet>();
	}

	public virtual void Handle() {

	}
	
	public void SetLoser(Gambler loser){
		this.loser = loser;
	}

	public void EndRound(Gambler loser) {
		loser.LoseProcess(basePotMoney, roundPot);
		GetOther(loser).WinProcess(basePotMoney, roundPot);

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