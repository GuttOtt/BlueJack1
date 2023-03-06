using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyEditor : MonoBehaviour {
	[SerializeField] private Button maxRaiseCountPlus, maxRaiseCountMinus;
	[SerializeField] private Button raiseRatioPlus, raiseRatioMinus;
	[SerializeField] private Button playerStartMoneyPlus, playerStartMoneyMinus, opponentStartMoneyPlus, opponentStartMoneyMinus;
	[SerializeField] private Button antePlus, anteMinus;
	[SerializeField] private Button completeButton;
	[SerializeField] private Text maxRaiseCountText, raiseRatioText, playerStartMoneyText, opponentStartMoneyText, anteText;
	private MoneySettings moneySettings;

	private void Awake() {
		maxRaiseCountPlus.onClick.AddListener(() => HandleMaxRaiseCount(1));
		maxRaiseCountMinus.onClick.AddListener(() => HandleMaxRaiseCount(-1));
		raiseRatioPlus.onClick.AddListener(() => HandleRaiseRatio(1f));
		raiseRatioMinus.onClick.AddListener(() => HandleRaiseRatio(-1f));
		playerStartMoneyPlus.onClick.AddListener(() => HandleStartMoney(Money.wons(100), true));
		playerStartMoneyMinus.onClick.AddListener(() => HandleStartMoney(Money.wons(-100), true));
		opponentStartMoneyPlus.onClick.AddListener(() => HandleStartMoney(Money.wons(100), false));
		opponentStartMoneyMinus.onClick.AddListener(() => HandleStartMoney(Money.wons(-100), false));
		antePlus.onClick.AddListener(() => HandleAnte(Money.wons(5)));
		anteMinus.onClick.AddListener(() => HandleAnte(Money.wons(-5)));
		completeButton.onClick.AddListener(CompleteEditing);

		moneySettings = new MoneySettings();
		moneySettings.maxRaiseCount = 3;
		moneySettings.raiseRatio = 2f;
		moneySettings.playerStartMoney = Money.wons(500);
		moneySettings.opponentStartMoney = Money.wons(500);
		moneySettings.ante = Money.wons(25);

		UpdateText();
	}

	private void HandleMaxRaiseCount(int number) {
		moneySettings.maxRaiseCount += number;
		UpdateText();
	}

	private void HandleRaiseRatio(float number) {
		moneySettings.raiseRatio += number;
		UpdateText();
	}

	private void HandleStartMoney(Money money, bool isPlayers) {
		if (isPlayers) {
			moneySettings.playerStartMoney = moneySettings.playerStartMoney.Plus(money);
		}
		else {
			moneySettings.opponentStartMoney = moneySettings.opponentStartMoney.Plus(money);
		}
		UpdateText();
	}

	private void HandleAnte(Money money) {
		moneySettings.ante = moneySettings.ante.Plus(money);
		UpdateText();
	}

	private void CompleteEditing() {
		SandboxManager.Instance.EndMoneyEditing(moneySettings);
	}

	private void UpdateText() {
		maxRaiseCountText.text = "최대 레이즈 회수: " + moneySettings.maxRaiseCount;
		raiseRatioText.text = "레이즈 비율: x" + moneySettings.raiseRatio;
		playerStartMoneyText.text = "플레이어 시작 금액: " + moneySettings.playerStartMoney.AmountToInt();
		opponentStartMoneyText.text = "상대 시작 금액: " + moneySettings.opponentStartMoney.AmountToInt();
		anteText.text = "앤티: " + moneySettings.ante.AmountToInt();
	}
}

public class MoneySettings {
	public int maxRaiseCount;
	public float raiseRatio;
	public Money playerStartMoney, opponentStartMoney;
	public Money ante;
}