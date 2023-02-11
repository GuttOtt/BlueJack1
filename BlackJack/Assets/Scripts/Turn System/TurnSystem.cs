using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour {
	[SerializeField] private Gambler player;
	[SerializeField] private Gambler opponent;
	public Gambler GetPlayer { get => player; }
	public Gambler GetOpponent { get => opponent; }
	private TurnSystemContext context;
	private IPhaseState startPhase, betPhase, hitPhase, endPhase;

	private void Awake() {
		startPhase = GetComponent<StartPhase>();
		betPhase = GetComponent<BetPhase>();
		hitPhase = GetComponent<HitPhase>();
		endPhase = GetComponent<EndPhase>();

		context = new TurnSystemContext();
	}

	private void Start() {
		context.Transit(betPhase);
	}

	public void ToStartPhase() {
		
	}

	public void ToBetPhase() {

	}

	public void ToHitPhase() {

	}

	public void ToEndPhase() {

	}

	public void ToEndPhase(Gambler loser) {

	}
}

public interface IPhaseState {
	void Handle();
}

public class TurnSystemContext {
	IPhaseState currentState;

	public void Transit(IPhaseState state) {
		currentState = state;
		currentState.Handle();
	}
}