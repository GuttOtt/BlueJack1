using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstPhase : MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
	}

	public void Handle() {
		Gambler burster = turnSystem.loser;
		turnSystem.basePotMoney = burster.PotMoney;
		burster.BurstProcess(turnSystem.basePotMoney);
		burster.OpenHiddens();
		turnSystem.ToEndPhase();
	}
}
