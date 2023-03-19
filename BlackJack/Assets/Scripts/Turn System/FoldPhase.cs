using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldPhase : MonoBehaviour, IPhaseState {
	private TurnSystem turnSystem;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
	}
	
  public void Handle() {		
		Gambler folder = turnSystem.loser;
		turnSystem.basePotMoney = folder.PotMoney;
		folder.FoldProcess(turnSystem.basePotMoney);
		turnSystem.ToEndPhase();
	}
}
