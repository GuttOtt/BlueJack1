using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsShowdownedCondition : MonoBehaviour, IEffectCondition {
    public bool IsSatisfied() {
        return TurnManager.Instance.isShowdowned;
    }
}
