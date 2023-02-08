using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettingManager : MonoBehaviour {
	[SerializeField] IBettingClient player;
	[SerializeField] IBettingClient opponent;
	[SerializeField] RoundManager roundManager;
	private int callCount = 0;
	private int raiseCount = 0;

	private IBettingClient presentGambler;
	private IBettingClient otherGambler {
		get {
			if (presentGambler == player) return opponent;
			else return player;
		}
	}

	private void Start() {
		presentGambler = player;
	}

	public void StartBettingPhase() {
		presentGambler = player;

		player.StartBetting();
	}

	public void CountCall() {
		callCount++;
	}

	public void CountRaise() {
		callCount = 1;
		raiseCount++;
	}

	public bool IsAbleToRaise() {
		if (raiseCount < 3) {
			return true;
		}
		return false;
	}

	private void EndBettingPhase() {
		Debug.Log("EndBettingPhase");
		GetComponent<HitManager>().StartHitPhase();
	}

	public void EndByFold(Betting folder) {
		Initialize();
		roundManager.EndRound(folder);
	}

	public void ChangeTurn() {
		if (callCount == 2) 
			EndBettingPhase();
		else {
			otherGambler.StartBetting();
			presentGambler = otherGambler;
		}
	}
	
	public void SetBlackJackers(IBettingClient player, IBettingClient opponent) {
		this.player = player;
		this.opponent = opponent;
	}

	public void Initialize() {
		callCount = 0;
		raiseCount = 0;
	}
}
