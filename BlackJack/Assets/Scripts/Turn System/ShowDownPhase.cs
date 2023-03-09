using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDownPhase : EndPhase {
	public override void Handle() {
		player.ShowDown();
		opponent.ShowDown();

		Gambler loser = GetLoser();
		basePotMoney = loser.PotMoney;
		EndRound(loser);
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
