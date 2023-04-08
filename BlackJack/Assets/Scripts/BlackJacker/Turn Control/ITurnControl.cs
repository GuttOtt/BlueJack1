using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnControl {
	void StartPhase();
	void TurnPhase();
	void IntervalPhase();
	void Fold();
	void Snap();
	void TakeHit();
	void TakeStay();
}
