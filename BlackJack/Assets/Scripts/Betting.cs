using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betting : MonoBehaviour {
	[SerializeField] private Wallet connectedWallet;
	[SerializeField] private Wallet potWallet;
	[SerializeField] private Betting opponentBetting;

	public bool Call() {
		Money moneyToCall = potWallet.CallMoney(opponentBetting.potWallet);
		return Bet(moneyToCall);
	}

	public bool RaiseRatio(float ratio) {
		Money moneyToRaise = potWallet.MoneyToRaise(opponentBetting.potWallet, ratio);
		return Bet(moneyToRaise);
	}

	private bool Bet(Money money) {
		if (connectedWallet.TryWithdrawTo(money, potWallet)) {
			return true;
		}
		else {
			Debug.Log("돈이 부족합니다!");
			return false;
		}
	}

	public bool PayAnte(Money ante) {
		return Bet(ante);
	}

	public void Lose() {
		potWallet.WithdrawAllTo(opponentBetting.potWallet);
		opponentBetting.WithdrawPot();
	}

	public void WithdrawPot() {
		potWallet.WithdrawAllTo(connectedWallet);
	}
}