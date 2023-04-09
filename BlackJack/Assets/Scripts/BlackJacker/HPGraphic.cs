using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HPGraphic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private Text numberText;
    private HP hp;
    private Image img;
    private bool pointerOn = false;

    private void Awake() {
        img = GetComponent<Image>();
    }

    public void SetHP(HP hp) {
        this.hp = hp;
    }

    public void Update() {
        if (hp != null && !pointerOn) 
            numberText.text = hp.ToInt().ToString();
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
