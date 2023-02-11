using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager: MonoBehaviour {
	[SerializeField] private VictoryManager victory;
	[SerializeField] private Betting playerBetting;
	[SerializeField] private Betting opponentBetting;
	[SerializeField] private Hand playerHand;
	[SerializeField] private Hand opponentHand;
	[SerializeField] private Text winText;

	private Money ante = Money.wons(1);
	private int round = 0;

	private void Awake() {
		victory = GetComponent<VictoryManager>();
	}

	private void Start() {
		StartRound();
	}

	public void StartRound() {
		round++;

		if (round % 3 == 0)
			ante = ante.Times(2f);

		playerBetting.PayAnte(ante);
		opponentBetting.PayAnte(ante);

		playerHand.DiscardAll();
		opponentHand.DiscardAll();

		GetComponent<HitManager>().StartSetting();
		GetComponent<BettingManager>().Initialize();
		GetComponent<BettingManager>().StartBettingPhase();

		winText.text = "";
	}

	public void EndRound() {
		Hand loserHand = victory.GetLoser();
		Betting loser = loserHand.gameObject.GetComponent<Betting>();

		StartCoroutine(EndRoundCoroutine(loser));
	}

	private IEnumerator EndRoundCoroutine(Betting loser) {	
		if (loser != playerBetting) {
			winText.text = "플레이어 승";
		}
		else {
			winText.text = "플레이어 패";
		}
		yield return new WaitForSeconds(3f);
		loser.Lose();
		StartRound();
	}

	public void EndRound(Betting loser) {
		StartCoroutine(EndRoundCoroutine(loser));
	}
}