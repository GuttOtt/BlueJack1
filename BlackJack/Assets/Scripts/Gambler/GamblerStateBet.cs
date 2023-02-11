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
		//UI or AI ��ǳ�� ����
		betInput.StartGettingInput();
	}

	private bool Call() {
		//�� ó��
		Debug.Log("Call");
		betPhase.TakeCall();
		return true;
	}

	private bool Raise() {
		if (betPhase.IsAbleToRaise()) {
			//�� ó��
			betPhase.TakeRaise();
			return true;
		}
		else {
			Debug.Log("���̻� ������ �� �� �����ϴ�");
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