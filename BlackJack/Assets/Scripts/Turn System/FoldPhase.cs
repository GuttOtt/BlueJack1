using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldPhase : EndPhase {
    public override void Handle() {
		Gambler folder = this.loser;
		basePotMoney = folder.PotMoney;
		folder.FoldProcess(basePotMoney);
		EndRound(folder);
	}
}
