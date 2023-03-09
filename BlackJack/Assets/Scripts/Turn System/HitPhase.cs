using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPhase: MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	private Gambler player;
	private Gambler opponent;
	private Dictionary<Gambler, bool> isDone;
	private Gambler presentTurn;
	private Gambler nextTurn {
		get { return presentTurn == player ? opponent : player; }
	}

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.GetPlayer;
		opponent = turnSystem.GetOpponent;
		isDone = new Dictionary<Gambler, bool>() { {player, false}, {opponent, false}};
	}

	public void Handle(){
		isDone[player] = false;
		isDone[opponent] = false;

		presentTurn = player;//카드 비교 후 결정
		presentTurn.StartHitting();
	}

	public void EndTurn() {
		isDone[presentTurn] = true;

		if (IsAllStayed()) {
			turnSystem.ToShowDownPhase();
		}
		else {
			if (IsAllDone()) {
				if (GetComponent<BetPhase>().IsAbleToRaise()){
					turnSystem.ToBetPhase();
				}
				else {
					turnSystem.ToHitPhase();
				}
			}
			else {
				presentTurn = nextTurn;
				presentTurn.StartHitting();
			}
		}
	}

	public void EndTurnByBurst(Gambler burster) {
		turnSystem.ToBurstPhase(burster);
	}

	private bool IsAllDone() {
		bool allDone = true;
		foreach(bool done in isDone.Values) {
			if (!done) allDone = false;
		}
		return allDone;
	}

	public bool IsAllStayed() {
		if (player.isStayed && opponent.isStayed)
			return true;
		return false;
	}

	public void Initialize() {

	}
}