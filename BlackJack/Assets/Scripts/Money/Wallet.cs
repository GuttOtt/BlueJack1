using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour {
    [SerializeField] private Money money;
    [SerializeField] private Text textUI;
    [SerializeField] private WalletGraphic graphic;

    private void Awake() {
        money = Money.wons(0);
        UpdateUI();
    }

    private void UpdateUI() {
        textUI.text = "$" + money.AmountToInt().ToString();
    }

    public bool TryWithdrawTo(Money amount, Wallet to) {
        if (money.IsBiggerOrEqualThan(amount)) {
            money = money.Minus(amount);
            to.Deposit(amount);
            UpdateUI();
            graphic.Minus(amount);
            return true;
        }
        return false;
    }

    public void WithdrawAllTo(Wallet to) {
        TryWithdrawTo(money, to);
    }

    public void Deposit(Money amount) {
        money = money.Plus(amount);
        UpdateUI();
        graphic.Plus(amount);
    }

    public Money CallMoney(Wallet other) {
        return CallMoney(other.money);
    }

    public Money CallMoney(Money other) {
        Money callMoney = other.Minus(money);
        return callMoney;
    }

    public Money MoneyToRaise(Wallet other, float raiseRatio) {
        Money m = other.Times(raiseRatio);
        Debug.Log("상대의 2배 :"+m.AmountToInt());
        m = m.Minus(this.money);
        Debug.Log("-potWallet" + m.AmountToInt());
        return m;
    }

    public Money Times(float percent) {
        return money.Times(percent);
    }

    public bool AffordTo(Money other) {
        return money.IsBiggerOrEqualThan(other);
    }
}

[System.Serializable]
public class Money {
    public static Money zero = wons(0);

    [SerializeField] private int amount;

    public static Money wons(int amount) {
        return new Money(amount);
    }

    Money(int amount) {
        this.amount = amount;
    }

    public Money Plus(Money other) {
        return wons(this.amount + other.amount);
    }

    public Money Minus(Money other) {
        return wons(this.amount - other.amount);
    }

    public Money Times(float percent) {
        float newAmount = this.amount;
        newAmount *= percent;
        return wons((int)newAmount); 
    }

    public bool IsLessThan(Money other) {
        return this.amount < other.amount? true : false;
    }

    public bool IsBiggerOrEqualThan(Money other) {
        return this.amount >= other.amount? true : false;
    }

    public int AmountToInt() {
        return amount;
    }
}