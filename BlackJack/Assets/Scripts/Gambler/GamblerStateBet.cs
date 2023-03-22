using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerStateBet: MonoBehaviour, IGamblerState {
	private Gambler gambler;
	private IBetInputSystem betInput;
	private BetPhase betPhase;
	private Wallet connectedWallet;
	[SerializeField] private Wallet potWallet;
	[SerializeField] private GamblerStateBet opponent;

	private void Awake() {
		betPhase = FindObjectOfType<BetPhase>();
		gambler = GetComponent<Gambler>();
		connectedWallet = GetComponent<Wallet>();
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
		Money moneyToCall = potWallet.CallMoney(opponent.potWallet);
		if (Bet(moneyToCall)) {
			EndBetting();
			betPhase.TakeCall();
			return true;
		}
		else {
			Debug.Log("�� �� ���� �����մϴ�");
			return false;
		}
	}

	private bool Raise() {
		Money moneyToRaise = potWallet.MoneyToRaise(opponent.potWallet, PlaySceneManager.moneySettings.raiseRatio);
		if (betPhase.IsAbleToRaise() && opponent.connectedWallet.AffordTo(moneyToRaise)) {
			if (Bet(moneyToRaise)) {
				StartCoroutine(GetComponent<Hand>().ActivateAllField(EffectCondition.OnRaise));
				EndBetting();
				betPhase.TakeRaise();
				return true;
			}
			else {
				Debug.Log("������ �� ���� �����մϴ�");
				return false;
			}
		}
		else {
			Debug.Log("���̻� ������ �� �� �����ϴ�");
			return false;
		}
	}

	private bool Fold() {
		StartCoroutine(GetComponent<Hand>().ActivateAllField(EffectCondition.OnFold));
		EndBetting();
		betPhase.TakeFold(gambler);
		return true;
	}
	
	private bool Bet(Money money) {
		if (connectedWallet.TryWithdrawTo(money, potWallet)) {
			return true;
		}
		else {
			return false;
		}
	}

	private void EndBetting() {
		betInput.EndGettingInput();
	}
}