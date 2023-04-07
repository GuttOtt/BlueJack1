using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSnapInput : MonoBehaviour, ISnapInput {
    

    public bool GetSnapInput() {
        return true;
    }

    public bool GetFoldInput() {
        return true;
    }
}
