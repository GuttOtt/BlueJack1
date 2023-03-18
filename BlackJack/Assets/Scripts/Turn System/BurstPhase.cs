using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstPhase : EndPhase {
	public override void Handle() {
		Gambler burster = loser;
		basePotMoney = burster.PotMoney;
		burster.BurstProcess(basePotMoney);
		burster.OpenHiddens();
		StartCoroutine(EndRound(burster));
	}
}
