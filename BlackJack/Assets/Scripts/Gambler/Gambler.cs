using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambler : MonoBehaviour {
	private IGamblerState betState, hitState, waitState;
	private GamblerContext context;
	private Wallet wallet;
	public Wallet potWallet;

	private void Awake() {
		betState = GetComponent<GamblerStateBet>();
		//hitState = GetComponent<GamblerStateHit>();
		waitState = GetComponent<GamblerStateWait>();

		context = new GamblerContext();
		context.Transit(waitState);
	}

	public void Wait() {

	}

	public void StartBetting() {
		context.Transit(betState);
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