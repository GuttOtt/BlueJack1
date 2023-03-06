using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPhase: MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;
	private Gambler player;
	private Gambler opponent;
	private int roundCount;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		player = turnSystem.PlayerGambler;
		opponent = turnSystem.OpponentGambler;
		roundCount = 0;
	}

	public void Handle() {
		roundCount++;

		if (MakeStart(player) && MakeStart(opponent)) {
			turnSystem.ToBetPhase();
		}
	}

	private bool MakeStart(Gambler gambler) {
		if (!gambler.StartRound(PlaySceneManager.moneySettings.ante)) {
			Debug.Log("Can't pay ante");
			return false;
		}

		return true;
	}
}