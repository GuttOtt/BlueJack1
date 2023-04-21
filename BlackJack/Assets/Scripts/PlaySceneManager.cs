using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour {
	[SerializeField] private Gambler player, opponent;
	[SerializeField] private TurnSystem turnSystem;
	public static MoneySettings moneySettings;


    private void Start() {
		MGContainer.Initialize();

		SandboxManager.Instance.DeckSetting(player.GetComponent<Deck>(), true);
		SandboxManager.Instance.DeckSetting(opponent.GetComponent<Deck>(), false);
		
		moneySettings = SandboxManager.Instance.GetMoneySettings();
		player.GetComponent<Wallet>().Deposit(moneySettings.playerStartMoney);
		opponent.GetComponent<Wallet>().Deposit(moneySettings.opponentStartMoney);
		player.GetComponent<MoneyGraphic>().SetMoney(moneySettings.playerStartMoney);
		opponent.GetComponent<MoneyGraphic>().SetMoney(moneySettings.opponentStartMoney);

		turnSystem.ToStartPhase();
	}
}
