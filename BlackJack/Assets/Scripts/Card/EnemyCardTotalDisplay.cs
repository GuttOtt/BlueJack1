using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCardTotalDisplay : MonoBehaviour, ICardTotalDisplay {
    [SerializeField] private Text totalText;
    [SerializeField] private Hand hand;
    [SerializeField] private Image hiddenCardIcon;

    public void Update() {
        if (hand.IsHiddenShown) {
            DisplayTotal(hand.GetTotal());
            hiddenCardIcon.gameObject.SetActive(false);
        }
        else {
            hiddenCardIcon.gameObject.SetActive(true);
            DisplayTotal(hand.GetFieldTotal());
        }
    }

    public void DisplayTotal(int total) {
        totalText.text = total.ToString();
    }
}
