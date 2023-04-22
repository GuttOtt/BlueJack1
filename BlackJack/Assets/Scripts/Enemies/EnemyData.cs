using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ScriptableObject {
    public string enemyName;
    public Sprite portrait;
    public HP hp;
    public List<CardData> deckData;
}
