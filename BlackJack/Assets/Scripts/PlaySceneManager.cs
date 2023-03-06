using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour {
	[SerializeField] private Gambler player, opponent;
	[SerializeField] private TurnSystem turnSystem;

	private void Awake() {
		SandboxManager.Instance.SetDeck(player.GetComponent<Deck>(), true);
		SandboxManager.Instance.SetDeck(opponent.GetComponent<Deck>(), false);

		//SandboxManager.Instance.SetMoney(player.GetComponent<Wallet>(), true);
		//SandboxManager.Instance.SetMoney(opponent.GetComponent<Wallet>(), false);

		//turnSystem.ToStartPhase();
	}
}
