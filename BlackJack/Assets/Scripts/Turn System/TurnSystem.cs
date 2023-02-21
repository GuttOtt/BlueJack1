using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour {
	[SerializeField] private Gambler player;
	[SerializeField] private Gambler opponent;
	[SerializeField] private Text phaseText;
	public Gambler GetPlayer { get => player; }
	public Gambler GetOpponent { get => opponent; }
	private TurnSystemContext context;
	private IPhaseState startPhase, betPhase, hitPhase, endPhase;
	public IPhaseState _endPhase { get => endPhase; }

	public Gambler PlayerGambler {
		get { return player; }
	}

	public Gambler OpponentGambler { 
		get {return opponent; }
	}

	public enum WayOfEnd { ShowDown, Burst, Fold }
	public WayOfEnd wayOfEnd = WayOfEnd.ShowDown;
	public Gambler loser;

	private void Awake() {
		startPhase = GetComponent<StartPhase>();
		betPhase = GetComponent<BetPhase>();
		hitPhase = GetComponent<HitPhase>();
		endPhase = GetComponent<EndPhase>();

		context = gameObject.AddComponent<TurnSystemContext>();
	}

	private void Start() {
	}

	public void ToStartPhase() {
		context.Transit(startPhase);
		ChangePhaseText("Start Phase");
	}

	public void ToBetPhase() {
		context.Transit(betPhase);
		ChangePhaseText("Bet Phase");

	}

	public void ToHitPhase() {
		context.Transit(hitPhase);
		ChangePhaseText("Hit Phase");
	}
	

	//EndPhase를 방식에 따라 3 Class로 구분하기? (FoldPhase, ShowdownPhase, BurstPhase...)
	public void ToEndPhase() {
		wayOfEnd = WayOfEnd.ShowDown;
		context.Transit(endPhase);
		ChangePhaseText("End Phase");
	}

	public void ToEndPhase(WayOfEnd way, Gambler loser) {
		wayOfEnd = way;
		this.loser = loser;
		context.Transit(endPhase);
		ChangePhaseText("End Phase");
	}

	public bool NoMoreAction() {
		BetPhase bet = GetComponent<BetPhase>();
		HitPhase hit = GetComponent<HitPhase>();

		if (bet.IsAbleToRaise() && hit.IsAllStayed()){
			return true;
		}
		return false;
	}

	public void ChangePhaseText(string text) {
		phaseText.text = text;
	}
}

public interface IPhaseState {
	void Handle();
}

public class TurnSystemContext: MonoBehaviour {
	IPhaseState currentState;
	TurnSystem turnSystem;
	EndPhase endPhase;

	private void Awake() {
		turnSystem = GetComponent<TurnSystem>();
		endPhase = GetComponent<EndPhase>();
	}

	public void Transit(IPhaseState state) {
		StartCoroutine(TransitCoroutine(state));
	}

	private IEnumerator TransitCoroutine(IPhaseState state) {
		yield return new WaitForSeconds(1.5f);

		currentState = state;
		currentState.Handle();
	}
}