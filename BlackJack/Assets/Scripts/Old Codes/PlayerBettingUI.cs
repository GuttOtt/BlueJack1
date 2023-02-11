using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBettingClient {
	void StartBetting();
	void EndBetting();

}


public class PlayerBettingUI : MonoBehaviour, IBettingClient {
	[SerializeField] private Betting betting;
	[SerializeField] private Button callButton;
	[SerializeField] private Button raiseButton;
	[SerializeField] private Button foldButton;
	[SerializeField] private BettingManager manager;
	private List<Button> allButton;


	public enum BetInput {	
		None,
		Call,
		Raise,
		Fold 
	}

	private BetInput input = BetInput.None;
	
	private void Awake() {
		allButton = new List<Button>() { callButton, raiseButton, foldButton };
		callButton.onClick.AddListener(CallInput);
		raiseButton.onClick.AddListener(RaiseInput);
		foldButton.onClick.AddListener(FoldInput);

	}

	public void StartBetting() {
		foreach(Button btn in allButton) {
			btn.gameObject.SetActive(true);
		}
	}

	private void CallInput() {
		if (betting.Call()) {
			manager.CountCall();
			EndBetting();
		}
	}

	private void RaiseInput() {
		if (!manager.IsAbleToRaise()) {
			Debug.Log("더이상 레이즈 할 수 없습니다");
		}
		else {
			if (betting.RaiseRatio(2f)) {
				manager.CountRaise();
				EndBetting();
			}
		}
	}

	private void FoldInput() {
		Debug.Log("Player Folds");
		manager.EndByFold(this.betting);
	}

	public void EndBetting() {
		foreach(Button btn in allButton) {
			btn.gameObject.SetActive(false);
		}
		manager.ChangeTurn();
	}
}