using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitManager : MonoBehaviour {	
	[SerializeField] private IHitClient playerHit;
	[SerializeField] private IHitClient opponentHit;
	[SerializeField] private Hand playerHand;
	[SerializeField] private Hand opponentHand;
	private IHitClient presentTurn;
	private IHitClient nextTurn {
		get {
			return presentTurn == playerHit? opponentHit : playerHit;
		}
	}

	public void StartHitPhase() {
		int playerTotal = playerHand.GetTotal();
		int opponentTotal = opponentHand.GetComponent<Hand>().GetTotal();

		presentTurn = playerTotal < opponentTotal ? opponentHit : playerHit;

		StartCoroutine(WaitForHitAction());
	}

	private IEnumerator WaitForHitAction() {
		presentTurn.StartTurn();

		while (!presentTurn.DecideHit()) {
			yield return null;			
		}
		presentTurn.EndTurn();
		
		presentTurn = nextTurn;
		presentTurn.StartTurn();
		while (!presentTurn.DecideHit()) {
			yield return null;			
		}
		presentTurn.EndTurn();

		EndHitPhase();
	}

	private void EndHitPhase() {
		Debug.Log("EndHitPhase");
		GetComponent<BettingManager>().StartBettingPhase();
	}

	public void Initialize(IHitClient playerHit, IHitClient opponentHit, 
							Hand playerHand, Hand opponentHand) {
		this.playerHit = playerHit;
		this.opponentHit = opponentHit;
		this.playerHand = playerHand;
		this.opponentHand = opponentHand;
		presentTurn = playerHit;
	}

	public void StartSetting() {
		playerHit.StartSetting();
		opponentHit.StartSetting();
	}
}