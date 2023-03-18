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

	public IEnumerator EndRound(Gambler loser) {
		yield return StartCoroutine( loser.LoseProcess(basePotMoney, roundPot) );
		yield return StartCoroutine( GetOther(loser).WinProcess(basePotMoney, roundPot) );

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