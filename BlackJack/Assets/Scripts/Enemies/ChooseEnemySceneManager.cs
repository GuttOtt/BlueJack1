using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseEnemySceneManager : MonoBehaviour {
    [SerializeField] Text floorText;
    [SerializeField] EnemyChoice[] enemyChoices = new EnemyChoice[3];
    private EnemyArchetype[] normalEnemyTypes;
    private EnemyArchetype randomNormalEnemy {
        get => normalEnemyTypes[Random.Range(0, normalEnemyTypes.Length - 1)];
    }

    private void Awake() {
        normalEnemyTypes = Resources.LoadAll<EnemyArchetype>("Enemies/Normal Enemies");
    }

    private void Start() {
        StartChoosingEnemy();
    }

    public void StartChoosingEnemy() {
        int floor = GameManager.currentFloor;
        floorText.text = GameManager.currentFloor + "Ãþ";

        foreach (EnemyChoice choice in enemyChoices) {
            choice.Initialize(randomNormalEnemy, floor);
        }
    }

    public void EndEnemyChoiceScene(EnemyData selectedEnemyData) {
        GameManager.ToBlackjackScene(selectedEnemyData);
    }
}
