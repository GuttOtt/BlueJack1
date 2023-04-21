using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardIconStorage : SerializableDictionary.Storage<CardIcon[]> { }

[System.Serializable]
public class IntIntDictionary : SerializableDictionary<int, int> { }

[System.Serializable]
public class IntCardIconsDictionary
    : SerializableDictionary<int, CardIcon[], CardIconStorage> { }

[CreateAssetMenu(fileName = "Enemy Data", 
    menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject {
    public string enemyName;
    public Sprite portrait;
    public IntIntDictionary hpAmountForDifficulty;
    public IntCardIconsDictionary iconsForDifficulty;
}
