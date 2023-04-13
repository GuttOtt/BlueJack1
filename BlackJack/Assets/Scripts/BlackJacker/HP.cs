using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HP {
    [SerializeField] private int amount;

    public HP(int amount) {
        this.amount = amount;
    }

    public HP Zero {
        get => new HP(0);
    }

    public int ToInt() {
        return amount;
    }

    public void TakeDamage(int damage) {
        amount -= damage;
    }

    public void Heal(int healAmount) {
        amount += healAmount;
    }
}
