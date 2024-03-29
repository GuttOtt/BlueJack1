using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TurnManager : Singleton<TurnManager> {
	[SerializeField] private Text phaseText;
	[SerializeField] public readonly int maxTurnPhaseCount = 3;
	public IPhase start, turn, interval, burst, fold, showdown, victory, stayed;
	private PhaseContext context;
	private BlackJackSceneManager blackjackSceneManager;
	public IPhase currentPhase { get => context.CurrentPhase; }
	public PlayerTC player;
	public PlayerTC enemy;
	public bool PlayerDone, EnemyDone;
	public bool BothDone { get => PlayerDone && EnemyDone ? true : false; }
	public PlayerTC loser;
	public PlayerTC winner {
		get => loser == null ? null : loser == player ? enemy : player;
	}
	public bool isShowdowned = false;
    public int turnPhaseCount = 0;

    protected override void Awake() {
		context = gameObject.AddComponent<PhaseContext>();

		start = gameObject.AddComponent<_StartPhase>();
		turn = gameObject.AddComponent<_TurnPhase>();
		interval = gameObject.AddComponent<_IntervalPhase>();
		burst = gameObject.AddComponent<_BurstPhase>();
		fold = gameObject.AddComponent<_FoldPhase>();
		showdown = gameObject.AddComponent<_ShowDownPhase>();
		victory = gameObject.AddComponent<_VictoryPhase>();
		stayed = gameObject.AddComponent<_StayedPhase>();

		blackjackSceneManager = FindObjectOfType<BlackJackSceneManager>();
	}

    private void Update() {
		if (Input.GetKeyDown(KeyCode.V))
			enemy.GetComponent<BlackJacker>().TakeDamage(200);
    }

    private void OnEnable() {
		TurnEventBus.Subscribe(TurnEventType.PLAYER_END, PlayerEnd);
		TurnEventBus.Subscribe(TurnEventType.ENEMY_END, EnemyEnd);
		TurnEventBus.Subscribe(TurnEventType.PLAYER_FOLD, () => ToFoldPhase(player));
        TurnEventBus.Subscribe(TurnEventType.ENEMY_FOLD, () => ToFoldPhase(enemy));
		TurnEventBus.Subscribe(TurnEventType.PLAYER_LOSE, () => StartCoroutine( PlayerLose() ));
		TurnEventBus.Subscribe(TurnEventType.ENEMY_LOSE, () => StartCoroutine( EnemyLose() ));
    }

	private void OnDisable() {
		TurnEventBus.Unsubscribe(TurnEventType.PLAYER_END, PlayerEnd);
		TurnEventBus.Unsubscribe(TurnEventType.ENEMY_END, EnemyEnd);
	}

    private void PlayerEnd() { PlayerDone = true; }
	private void EnemyEnd() { EnemyDone = true; }


	public void ToStartPhase() {
		context.Transition(start);
	}

	public void ToTurnPhase() {
		context.Transition(turn);
	}

	public void ToIntervalPhase() {
		context.Transition(interval);
	}

	public void ToStayedPhase() {
		context.Transition(stayed);
	}
	
	public void ToShowDownPhase() {
		context.Transition(showdown);
		Debug.Log("ShowDownPhase" + Time.time);
	}

	public void ToBurstPhase(PlayerTC burster) {
		loser = burster;
		context.Transition(burst);
	}

	public void ToFoldPhase(PlayerTC folder) {
		loser = folder;
		context.Transition(fold);
	}

	public void ToVictoryPhase(PlayerTC loser) {
		this.loser = loser;
		context.Transition(victory);
	}

	public void ToVictoryPhase() {
		this.loser = null;
		context.Transition(victory);
	}

	public void ResetDone() {
		PlayerDone = false;
		EnemyDone = false;
	}

	public IEnumerator PlayerLose() {
		context.StopCurrentCoroutine();
		yield return new WaitForSeconds(2f);
		blackjackSceneManager.EndBlackjackSceneByLose();
	}

	public IEnumerator EnemyLose() {
		blackjackSceneManager.EndBlackjackSceneByWin();
		yield return new WaitForSeconds(2f);
	}

	public void ChangePhaseText(string newPhaseText) {
		phaseText.text = newPhaseText;
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
	
	public void StopCurrentCoroutine() {
		if (coroutine != null) StopCoroutine(coroutine);
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
		turnManager.ChangePhaseText("New Round");

		turnManager.turnPhaseCount = 0;
		turnManager.isShowdowned = false;

		TurnEventBus.Publish(TurnEventType.NEW_ROUND);

		turnManager.player.StartPhase();
		turnManager.enemy.StartPhase();
		yield return new WaitUntil(() => turnManager.BothDone);
		yield return new WaitForSeconds(1f);

		turnManager.ToIntervalPhase();
	}
}

public class _TurnPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

	private void Awake() {
		turnManager = GetComponent<TurnManager>();
	}

	public IEnumerator Phase() {
		if (turnManager.turnPhaseCount == turnManager.maxTurnPhaseCount) {
			turnManager.ToShowDownPhase();
			yield break;
		}
		turnManager.turnPhaseCount++;

		turnManager.ChangePhaseText("Main Phase " + turnManager.turnPhaseCount 
			+ "/" + turnManager.maxTurnPhaseCount);

		yield return StartCoroutine(turnManager.enemy.GetComponent<EnemySC>().DecideSnap());

		turnManager.player.TurnPhase();
		yield return new WaitUntil(() => turnManager.PlayerDone);
		turnManager.enemy.TurnPhase();
		yield return new WaitUntil(() => turnManager.BothDone);

		TurnEventBus.Publish(TurnEventType.TURN_END);
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
		yield return new WaitForSeconds(1f);
		turnManager.enemy.IntervalPhase();
		yield return new WaitUntil(() => turnManager.BothDone);
		yield return new WaitForSeconds(1f);

		if (turnManager.player.IsBursted) {
			turnManager.ToBurstPhase(turnManager.player);
		}
		else if (turnManager.enemy.IsBursted) {
			turnManager.ToBurstPhase(turnManager.enemy);
		}
		else if (turnManager.player.IsStayed) {
            /*if (turnManager.enemy.IsStayed) {
				turnManager.ToShowDownPhase();
			}
			else {
				turnManager.ToStayedPhase();
			}*/
            turnManager.ToStayedPhase();
        }
		else {
			turnManager.ToTurnPhase();
		}
	}
}

public class _BurstPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

	private void Awake() {
		turnManager = GetComponent<TurnManager>();
	}

	public IEnumerator Phase() {
		PlayerTC burster = turnManager.loser;
		yield return StartCoroutine(burster.BurstProcess());
		yield return new WaitForSeconds(1f);
		turnManager.ToVictoryPhase(burster);
	}
}

public class _FoldPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

    private void Awake() {
		turnManager = GetComponent<TurnManager>();
    }

    public IEnumerator Phase() {
		PlayerTC folder = turnManager.loser;
		yield return new WaitForSeconds(1f);
		turnManager.ToVictoryPhase(folder);
	}
}

public class _StayedPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

    private void Awake() {
		turnManager = GetComponent<TurnManager>();
    }
    public IEnumerator Phase() {
		StartCoroutine(turnManager.player.StayedProcess());
		yield return new WaitUntil(() => turnManager.PlayerDone);
		StartCoroutine(turnManager.enemy.StayedProcess());
		yield return new WaitUntil(() => turnManager.EnemyDone);
		yield return new WaitForSeconds(1f);
		turnManager.ToTurnPhase();
	}
}

public class _ShowDownPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

	private void Awake() {
		turnManager = GetComponent<TurnManager>();
	}

	public IEnumerator Phase() {
		turnManager.ChangePhaseText("Showdown!");
		turnManager.isShowdowned = true;

		TurnEventBus.Publish(TurnEventType.VICTORY_PHASE);

		turnManager.player.ShowDownPhase();
		yield return new WaitUntil( () => turnManager.PlayerDone);
		turnManager.enemy.ShowDownPhase();
		yield return new WaitUntil( () => turnManager.EnemyDone);

		yield return new WaitForSeconds(2f);

		if (turnManager.player.GetHandTotal < turnManager.enemy.GetHandTotal) {
			turnManager.ToVictoryPhase(turnManager.player);
		}
		else if (turnManager.player.GetHandTotal > turnManager.enemy.GetHandTotal) {
			turnManager.ToVictoryPhase(turnManager.enemy);
		}
		else {
			turnManager.ToVictoryPhase();
		}
	}
}

public class _VictoryPhase : MonoBehaviour, IPhase {
	private TurnManager turnManager;

    private void Awake() {
		turnManager = GetComponent<TurnManager>();
    }

    public IEnumerator Phase() {
		PlayerTC loser = turnManager.loser;
		PlayerTC winner = turnManager.winner;
		if (loser != null) {
			if (winner == turnManager.player) turnManager.ChangePhaseText("Player Win");
			else turnManager.ChangePhaseText("Opponent Win");

            yield return StartCoroutine(winner.WinProcess());
			yield return new WaitForSeconds(1f);
            yield return StartCoroutine(loser.LoseProcess());
			yield return new WaitForSeconds(1f);
        }
		else {
			turnManager.ChangePhaseText("Draw");
		}
        turnManager.ToStartPhase();
    }
}
