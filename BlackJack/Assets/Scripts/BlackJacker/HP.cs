using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HP {
    [SerializeField] private int amount;

    public HP(int amount) {
        this.amount = amount;
    }

    public static HP Zero {
        get => new HP(0);
    }

    public int ToInt() {
        return amount;
    }

    public override string ToString() {
        return amount.ToString();
    }

    public void TakeDamage(int damage) {
        amount -= damage;
    }

    public void Heal(int healAmount) {
        amount += healAmount;
    }
}
