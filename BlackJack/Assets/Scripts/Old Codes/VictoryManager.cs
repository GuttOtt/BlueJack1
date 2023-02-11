using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour {
	[SerializeField] private Hand playerHand;
	[SerializeField] private Hand opponentHand;

	public Hand GetWinner() {
		int playerTotal = playerHand.GetTotal();
		int opponentTotal = opponentHand.GetTotal();

		return playerTotal < opponentTotal ? opponentHand : playerHand;
	}

	public Hand GetLoser() {
		int playerTotal = playerHand.GetTotal();
		int opponentTotal = opponentHand.GetTotal();

		return playerTotal > opponentTotal ? opponentHand : playerHand;
	}
}

