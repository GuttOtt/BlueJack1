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
		switch (turnSystem.wayOfEnd) {
			case TurnSystem.WayOfEnd.Fold:
				EndRoundByFold(turnSystem.loser);
				break;
			case TurnSystem.WayOfEnd.Burst:
				EndRoundByBurst(turnSystem.loser);
				break;
			case TurnSystem.WayOfEnd.ShowDown:
				EndRoundByShowDown();
				break;
		}
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

		Gambler loser = GetLoser();
		basePotMoney = loser.PotMoney;
		EndRound(loser);
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

	private Gambler GetLoser() {
		Hand playerHand = player.GetComponent<Hand>();
		Hand opponentHand = opponent.GetComponent<Hand>();

		if (playerHand.GetTotal() < opponentHand.GetTotal()) {
			Debug.Log("Loser: "+player);
			return player;
		}
		else {
			Debug.Log("Loser: "+opponent);
			return opponent;
		}
	}

	private Gambler GetOther(Gambler gambler) {
		if (gambler == player) return opponent;
		return player;
	}
}