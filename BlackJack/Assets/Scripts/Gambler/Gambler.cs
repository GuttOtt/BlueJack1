using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambler : MonoBehaviour {
	[SerializeField] private Gambler opponent;
	private IGamblerState betState, hitState, waitState;
	private GamblerContext context;
	private Hand hand;
	private Deck deck;
	private Wallet wallet;
	public Wallet potWallet;
	public bool isStayed;
	public Money PotMoney {
		get => potWallet.Times(1f);
	}

	public Money basePot = Money.wons(0);

	private void Awake() {
		betState = GetComponent<GamblerStateBet>();
		hitState = GetComponent<GamblerStateHit>();
		waitState = GetComponent<GamblerStateWait>();

		hand = GetComponent<Hand>();
		deck = GetComponent<Deck>();
		wallet = GetComponent<Wallet>();

		context = new GamblerContext();
		context.Transit(waitState);
	}

	public void Wait() {

	}

	public void StartBetting() {
		context.Transit(betState);
	}

	public bool StartRound(Money ante) {
		Debug.Log(this + "StartRound " + Time.time);
		if (!wallet.TryWithdrawTo(ante, potWallet)) {
			return false;
		}
		
		hand.DiscardAll();

		deck.ClosedHit();
		deck.ClosedHit();
		deck.OpenHit();

		return true;
	}

	public void StartHitting() {
		context.Transit(hitState);
	}
	/*
	public void LosePot(Wallet roundPot) {
		basePot = potWallet.Times(1f);
		hand.ActivateAllIcon(EffectCondition.OnLose);
		potWallet.WithdrawAllTo(roundPot);
	}

	public void WinPot(Wallet roundPot) {
		roundPot.WithdrawAllTo(wallet);
		potWallet.WithdrawAllTo(wallet);
	}*/

	public void NewRoundInitialize() {
		isStayed = false;
	}


	public void Steal(Money money) {
		opponent.wallet.TryWithdrawTo(money, this.wallet);
	}

	public void Save(float percent) {
		Money saveMoney = basePot.Times(percent);
		potWallet.TryWithdrawTo(saveMoney, wallet);
		Debug.Log("Saved " + saveMoney.AmountToInt());
	}

	//End Round Processes. All should move to new class, GamblerStateEnd.
	public void FoldProcess(Money basePot) {
		this.basePot = basePot;
		hand.ActivateAllIcon(EffectCondition.OnFold);
	}

	public void BurstProcess(Money basePot) {
		this.basePot = basePot;
		hand.ActivateAllIcon(EffectCondition.OnBurst);
	}

	public void WinProcess(Money basePot, Wallet roundPot) {
		this.basePot = basePot;
		hand.ActivateAllIcon(EffectCondition.OnWin);
		roundPot.WithdrawAllTo(wallet);
		potWallet.WithdrawAllTo(wallet);
	}

	public void LoseProcess(Money basePot, Wallet roundPot) {
		this.basePot = basePot;
		hand.ActivateAllIcon(EffectCondition.OnLose);
		potWallet.WithdrawAllTo(roundPot);
	}

	public void ShowDown() {
		OpenHiddens();
		hand.ActivateAllIcon(EffectCondition.OnShowDown);
	}

	public void OpenHiddens() {
		hand.OpenHiddens();
	}
}

//Bet, Hit, Wait
public interface IGamblerState {
	void StartState();
}

public class GamblerContext {
	IGamblerState currentState;

	public void Transit(IGamblerState state) {
		currentState = state;
		currentState.StartState();
	}
}