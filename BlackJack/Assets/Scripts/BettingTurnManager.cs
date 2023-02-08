using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingTurnManager : MonoBehaviour{
	/*
	[SerializeField] private Betting playerBetting;
	[SerializeField] private Betting opponentBetting;
	[SerializeField] private PlayerBettingUI playerBettingUI;

	private bool isWatingForBetting = false;
	private Betting whosTurn;
	private Betting nextTurn {
		get {
			if (whosTurn == playerBetting)
				return opponentBetting;
			else
				return playerBetting;
		}
	}
	
	private void Start() {
		whosTurn = opponentBetting;
		StartBettingTurn();
	}

	public void StartBettingTurn() {
		whosTurn = nextTurn;
		if (whosTurn == playerBetting) {
			playerBettingUI.DrawUI();
		}
		else {
			playerBettingUI.EraseUI();
		}
		isWatingForBetting = true;
		StartCoroutine(WaitForBetting());
	}

	private IEnumerator WaitForBetting() {
		while(true) {
			if (isWatingForBetting) {
				yield return null;
			}
			else {
				StartBettingTurn();
				yield break;
			}
		}
	}

	public void EndTurn() {
		isWatingForBetting = false;
	}
	*/
}
