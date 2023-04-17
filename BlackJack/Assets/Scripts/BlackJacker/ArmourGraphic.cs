using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourGraphic : MonoBehaviour {
    [SerializeField] private Text numberText;
    private Armour armour;

    private void Awake() {
        if (!numberText) numberText = transform.GetChild(0).GetComponent<Text>();
    }

    private void Update() {
        numberText.text = armour.ToInt().ToString();
    }

    public void SetArmour(Armour armour) {
        this.armour = armour;
    }
}
