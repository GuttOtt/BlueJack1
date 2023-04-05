using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackSceneManager : MonoBehaviour {
	[SerializeField] private BlackJacker player, opponent;
	[SerializeField] private TurnManager turnManager;

	private void Start() {
		SandboxManager.Instance.DeckSetting(player.GetComponent<Deck>(), true);
		SandboxManager.Instance.DeckSetting(opponent.GetComponent<Deck>(), false);

		turnManager.ToStartPhase();
	}
}
