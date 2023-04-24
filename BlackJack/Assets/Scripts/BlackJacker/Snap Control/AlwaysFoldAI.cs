using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFoldAI : MonoBehaviour, ISnapFoldAI {
    public SnapDecision Decide() {
        return SnapDecision.Fold;
    }
}
