using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambler : MonoBehaviour {
	[SerializeField] public Gambler opponent;
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
		if (!wallet.TryWithdrawTo(ante, potWallet)) {
			return false;
		}

		StartCoroutine(StartRoundCoroutine());

		return true;
	}

	public IEnumerator StartRoundCoroutine() {
		hand.DiscardAll();

		yield return new WaitForSeconds(1.1f);

		deck.ClosedHit();
		deck.ClosedHit();
		deck.OpenHit();
	}


	public void StartHitting() {
		context.Transit(hitState);
	}

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

	public void TakeMore(float percent) {
		Money takeMoney = basePot.Times(percent);
		opponent.wallet.TryWithdrawTo(takeMoney, this.wallet);
		Debug.Log("Took +" + takeMoney.AmountToInt() + " more");
	}

	public void BetMore(float percent) {
		Money betMoney = potWallet.Times(percent);
		wallet.TryWithdrawTo(betMoney, potWallet);
		Debug.Log(this + " Bet " + betMoney.AmountToInt() + "more");
	}

	//End Round Processes. All should move to new class, GamblerStateEnd.
	public IEnumerator FoldProcess(Money basePot) {
		this.basePot = basePot;
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnFold));
	}

	public IEnumerator BurstProcess(Money basePot) {
		this.basePot = basePot;
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnBurst));
	}

	public IEnumerator WinProcess(Money basePot, Wallet roundPot) {
		this.basePot = basePot;
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnWin));
		roundPot.WithdrawAllTo(wallet);
		potWallet.WithdrawAllTo(wallet);
		yield return new WaitForSeconds(1f);
	}

	public IEnumerator LoseProcess(Money basePot, Wallet roundPot) {
		this.basePot = basePot;
		yield return StartCoroutine(hand.ActivateAllField(EffectSituation.OnLose));
		potWallet.WithdrawAllTo(roundPot);
		yield return new WaitForSeconds(1f);
	}

	public IEnumerator ShowDown() {
		OpenHiddens();
		yield return StartCoroutine(hand.ActivateAllIcon(EffectSituation.OnShowDown));
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