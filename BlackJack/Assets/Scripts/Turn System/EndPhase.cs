using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPhase: MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	private Gambler player;
	private Gambler opponent;
	[SerializeField] private Wallet roundPot;
	private Money basePotMoney;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.GetPlayer;
		opponent = turnSystem.GetOpponent;
		roundPot = gameObject.GetComponent<Wallet>();
	}

	public void Handle() {
		Gambler loser = GetLoser();
		EndRoundByShowDown();
	}

	public void EndRoundByFold(Gambler folder) {
		basePotMoney = folder.PotMoney;
		folder.FoldProcess(basePotMoney);
		EndRound(folder);
	}

	public void EndRoundByBurst(Gambler burster) {
		basePotMoney = burster.PotMoney;
		burster.BurstProcess(basePotMoney);
		burster.OpenHiddens();
		EndRound(burster);
	}

	public void EndRoundByShowDown() {
		player.ShowDown();
		opponent.ShowDown();

		EndRound(GetLoser());
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
		Debug.Log("EndRoundDelay");
		yield return new WaitForSeconds(2f);
		
		Debug.Log("EndRoundDelay2");
		turnSystem.ToStartPhase();
	}

	private Gambler GetLoser() {
		Hand playerHand = player.GetComponent<Hand>();
		Hand opponentHand = player.GetComponent<Hand>();

		if (playerHand.GetTotal() < opponentHand.GetTotal()) {
			return player;
		}
		else {
			return opponent;
		}
	}

	private Gambler GetOther(Gambler gambler) {
		if (gambler == player) return opponent;
		return player;
	}
}