using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Armour {
    [SerializeField] private int _amount;
    public int amount {
        get => _amount;
        set {
            if (value < 0) _amount = 0;
            else _amount = value;
        }
    }

    public Armour(int amount) {
       this.amount = amount;
    }

    public static Armour Zero {
        get => new Armour(0);
    }

    public int ToInt() {
        return amount;
    }

    public void Gain(int amount) {
        this.amount += amount;
    }

    public int Loss(int amount) {
        int excess = amount - this.amount;
        this.amount -= amount;
        if (excess > 0) return excess;
        else return 0;
    }

    public void Reset() {
        amount = 0;
    }
}
