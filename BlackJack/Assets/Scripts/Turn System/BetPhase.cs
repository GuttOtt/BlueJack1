using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetPhase: MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	private int callCount = 0;
	private int raiseCount = 0;
	private Gambler player;
	private Gambler opponent;
	private Gambler presentTurn;
	private Gambler nextTurn {
		get { return presentTurn == player ? opponent : player; }
	}

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.GetPlayer;
		opponent = turnSystem.GetOpponent;
		Initialize();
	}

	public void Handle() {
		Initialize();
		presentTurn.StartBetting();
	}

	public void TakeCall() {
		callCount++;
		ChangeTurn();
	}

	public void TakeRaise() {
		callCount = 1;
		raiseCount++;
		ChangeTurn();
	}

	public bool IsAbleToRaise() {
		if (raiseCount < 3) 
			return true;
		else
			return false;
	}

	public void TakeFold(Gambler folder) {
		turnSystem.ToEndPhase(folder);
	}

	private void ChangeTurn() {
		if (callCount == 2) {
			turnSystem.ToHitPhase();
		}
		else {
			presentTurn.Wait();
			presentTurn = nextTurn;
			presentTurn.StartBetting();
		}
	}

	private void Initialize() {
		callCount = 0;
		raiseCount = 0;
		presentTurn = player;//카드 비교 후 결정하는 것으로 변경
	}
}