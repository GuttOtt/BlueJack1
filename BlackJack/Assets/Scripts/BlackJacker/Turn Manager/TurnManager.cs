using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : Singleton<TurnManager> { 
	public IPhase start, turn, interval, burst, fold, showdown, victory, stayed;
	private PhaseContext context;
	public IPhase currentPhase { get => context.CurrentPhase; }
	public PlayerTC player;
	public PlayerTC enemy;
	public bool PlayerDone, EnemyDone;
	public bool BothDone { get => PlayerDone && EnemyDone ? true : false; }

	protected override void Awake() {
		context = gameObject.AddComponent<PhaseContext>();

		start = gameObject.AddComponent<_StartPhase>();
		turn = gameObject.AddComponent<_TurnPhase>();
		interval = gameObject.AddComponent<_IntervalPhase>();
		burst = gameObject.AddComponent<_BurstPhase>();
		fold = gameObject.AddComponent<_FoldPhase>();
		showdown = gameObject.AddComponent<_ShowDownPhase>();
		victory = gameObject.AddComponent<_VictoryPhase>();
	}

	private void OnEnable() {
		TurnEventBus.Subscribe(TurnEventType.PLAYER_END, PlayerEnd);
		TurnEventBus.Subscribe(TurnEventType.ENEMY_END, EnemyEnd);
	}

	private void OnDisable() {
		TurnEventBus.Unsubscribe(TurnEventType.PLAYER_END, PlayerEnd);
		TurnEventBus.Unsubscribe(TurnEventType.ENEMY_END, EnemyEnd);
	}

	private void PlayerEnd() { PlayerDone = true; }
	private void EnemyEnd() { EnemyDone = true; }


	public void ToStartPhase() {
		context.Transition(start);
		Debug.Log("StartPhase");
	}

	public void ToTurnPhase() {
		context.Transition(turn);
		Debug.Log("TurnPhase");
	}

	public void ToIntervalPhase() {
		context.Transition(interval);
		Debug.Log("IntervalPhase");
	}

	public void ToBurstPhase(ITurnControl burster) {

	}

	public void ToShowDownPhase() {

	}

	public void ToFoldPhase() {

	}

	public void ToStayedPhase() {

	}

	public void ResetDone() {
		PlayerDone = false;
		EnemyDone = false;
	}
}

public class PhaseContext : MonoBehaviour {
	public IPhase CurrentPhase { get; set; }

	private TurnManager _turnManager;
	private Coroutine coroutine;

	private void Awake() {
		_turnManager = GetComponent<TurnManager>();
	}

	public void Transition() {
		if (coroutine != null) 
			StopCoroutine(coroutine);
		coroutine = StartCoroutine(CurrentPhase.Phase());
	}

	public void Transition(IPhase phase) {
		if (coroutine != null)
			StopCoroutine(coroutine);

		_turnManager.ResetDone();

		CurrentPhase = phase;
		coroutine = StartCoroutine(CurrentPhase.Phase());
	}
}

public interface IPhase {
	IEnumerator Phase();
}

public class _StartPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

	private void Awake() {
		turnManager = GetComponent<TurnManager>();
	}

	public IEnumerator Phase() {
		turnManager.player.StartPhase();
		turnManager.enemy.StartPhase();
		
		yield return new WaitUntil(() => turnManager.BothDone);

		turnManager.ToTurnPhase();
	}
}

public class _TurnPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

	private void Awake() {
		turnManager = GetComponent<TurnManager>();
	}

	public IEnumerator Phase() {
		turnManager.player.TurnPhase();
		yield return new WaitUntil(() => turnManager.PlayerDone);
		turnManager.enemy.TurnPhase();
		yield return new WaitUntil(() => turnManager.BothDone);

		turnManager.ToIntervalPhase();
	}
}


public class _IntervalPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

	private void Awake() {
		turnManager = GetComponent<TurnManager>();
	}

	public IEnumerator Phase() {
		turnManager.player.IntervalPhase();
		yield return new WaitUntil(() => turnManager.PlayerDone);
		turnManager.enemy.IntervalPhase();
		yield return new WaitUntil(() => turnManager.BothDone);

		if (turnManager.player.IsBursted) {
			turnManager.ToBurstPhase(turnManager.player);
		}
		else if (turnManager.enemy.IsBursted) {
			turnManager.ToBurstPhase(turnManager.enemy);
		}
		else if (turnManager.player.IsStayed) {
			if (turnManager.enemy.IsStayed) {
				//turnManager.ToShowDownPhase();
			}
			else {
				//turnManager.ToStayedPhase();
			}
		}
		else {
			turnManager.ToTurnPhase();
		}
	}
}

public class _BurstPhase : MonoBehaviour, IPhase {
	public IEnumerator Phase() {
		yield return null;
	}
}

public class _FoldPhase : MonoBehaviour, IPhase {
	public IEnumerator Phase() {
		yield return null;
	}
}

public class _StayedPhase : MonoBehaviour, IPhase {
	public IEnumerator Phase() {
		yield return null;
	}
}

public class _ShowDownPhase : MonoBehaviour, IPhase {
	public IEnumerator Phase() {
		yield return null;
	}
}

public class _VictoryPhase : MonoBehaviour, IPhase {
	public IEnumerator Phase() {
		yield return null;
	}
}
