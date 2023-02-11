using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerStateBet: MonoBehaviour, IGamblerState {
	private Gambler gambler;
	private IBetInputSystem betInput;
	private BetPhase betPhase;
	private Wallet wallet;
	[SerializeField] private Wallet potWallet;
	[SerializeField] private GamblerStateBet opponent;

	public void Awake() {
		betPhase = FindObjectOfType<BetPhase>();
		gambler = GetComponent<Gambler>();
		wallet = GetComponent<Wallet>();
		betInput = (IBetInputSystem) GetComponent<IBetInputSystem>();

		BetDele call = Call;
		BetDele raise = Raise;
		BetDele fold = Fold;
		betInput.SetInput(call, raise, fold);
	}

	public void StartState() {
		//UI or AI 말풍선 띄우기
		betInput.StartGettingInput();
	}

	private bool Call() {
		//돈 처리
		Debug.Log("Call");
		betPhase.TakeCall();
		return true;
	}

	private bool Raise() {
		if (betPhase.IsAbleToRaise()) {
			//돈 처리
			betPhase.TakeRaise();
			return true;
		}
		else {
			Debug.Log("더이상 레이즈 할 수 없습니다");
			return false;
		}
	}

	private bool Fold() {
		Debug.Log("Fold");
		betPhase.TakeFold(gambler);
		EndBetting();
		return true;
	}

	private void EndBetting() {
		betInput.EndGettingInput();
	}
}