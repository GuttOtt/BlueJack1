using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourGraphic : MonoBehaviour {
    [SerializeField] private Text numberText;
    [SerializeField] private Text changeText;
    private Armour armour;

    private void Awake() {
        if (!numberText) numberText = transform.GetChild(0).GetComponent<Text>();
        changeText.color = new Color(1, 1, 1, 0);
    }

    private void Update() {
        numberText.text = armour.ToInt().ToString();
    }

    private IEnumerator DisappearChangeText() {
        float alpha = 1;

        while (alpha > 0) {
            changeText.color = new Color(1, 1, 1, alpha) ;
            alpha -= Time.deltaTime / 2;
            yield return null;
        }
    }

    public void SetArmour(Armour armour) {
        this.armour = armour;
    }

    public void DrawGainGraphic(int amount) {
        changeText.text = "+" + amount.ToString();
        StartCoroutine(DisappearChangeText());
    }

    public void DrawLossGraphic(int amount) {
        changeText.text = "-" + amount.ToString();
        StartCoroutine(DisappearChangeText());
    }
}
