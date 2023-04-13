using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTotalDisplay : MonoBehaviour, ICardTotalDisplay {
    [SerializeField] Text totalText;
    [SerializeField] Hand hand;

    private void Update() {
        DisplayTotal(hand.GetTotal());
    }

    public void DisplayTotal(int total) {
        totalText.text = total.ToString();
    }
}