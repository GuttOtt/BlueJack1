using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HPGraphic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private Text numberText;
    [SerializeField] private Text changeText;
    private HP hp;
    private Image img;
    private bool pointerOn = false;

    private void Awake() {
        img = GetComponent<Image>();
        changeText.color = new Color(1, 1, 1, 0);
    }

    private void Update() {
        if (hp != null && !pointerOn)
            numberText.text = hp.ToInt().ToString();
    }

    public void SetHP(HP hp) {
        this.hp = hp;
    }
    private IEnumerator DisappearChangeText() {
        float alpha = 1;

        while (alpha > 0) {
            changeText.color = new Color(1, 1, 1, alpha);
            alpha -= Time.deltaTime / 2;
            yield return null;
        }
    }

    public void DrawHealGraphic(int amount) {
        changeText.text = "+" + amount.ToString();
        StartCoroutine(DisappearChangeText());
    }

    public void DrawDamageGraphic(int amount) {
        changeText.text = "-" + amount.ToString();
        StartCoroutine(DisappearChangeText());
    }

    public void OnPointerEnter(PointerEventData eventData) {
        pointerOn = true;
        int foldHPAmount = hp.ToInt() - SnapManager.pot;
        numberText.text = foldHPAmount.ToString();
        img.color = new Color(1, 1, 1, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        pointerOn = false;
        numberText.text = hp.ToInt().ToString();
        img.color = new Color(1, 1, 1, 1);
    }
}
