using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDownPhase : MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	private Gambler player;
	private Gambler opponent;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.GetPlayer;
		opponent = turnSystem.GetOpponent;
	}

	public void Handle() {
		player.ShowDown();
		opponent.ShowDown();

		turnSystem.loser = GetLoser();
		turnSystem.basePotMoney = turnSystem.loser.PotMoney;
		turnSystem.ToEndPhase();
	}

	private Gambler GetLoser() {
		Hand playerHand = player.GetComponent<Hand>();
		Hand opponentHand = opponent.GetComponent<Hand>();

		if (playerHand.GetTotal() < opponentHand.GetTotal()) {
			return player;
		}
		else {
			return opponent;
		}
	}
}
