using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnapInput : MonoBehaviour, ISnapInput {
    private ISnapAI snapAI;
    private IFoldAI foldAI;

    private void Awake() {
        snapAI = GetComponent<ISnapAI>();
        if (snapAI == null) {
            snapAI = gameObject.AddComponent<BasicSnapAI>();
        }
        foldAI = GetComponent<IFoldAI>();
        if (foldAI == null) {
            foldAI = gameObject.AddComponent<BasicFoldAI>();
        }
    }

    public bool GetSnapInput() {
        return true;
    }

    public bool GetFoldInput() {
        return false;
    }
}
