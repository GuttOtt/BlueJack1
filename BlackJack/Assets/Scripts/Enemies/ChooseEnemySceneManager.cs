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
    private Dictionary<int, EnemyArchetype[]> actElitesDict = 
        new Dictionary<int, EnemyArchetype[]>();
    private EnemyArchetype randomNormalEnemy {
        get => normalEnemyTypes[Random.Range(0, normalEnemyTypes.Length)];
    }
    private EnemyArchetype randomElites {
        get {
            int act = GameManager.act;
            EnemyArchetype[] elitesForAct = actElitesDict[act];
            return elitesForAct[Random.Range(-0, elitesForAct.Length)];
        }
    }

    private void Awake() {
        normalEnemyTypes = Resources.LoadAll<EnemyArchetype>("Enemies/Normal Enemies");
        actElitesDict[1] = Resources.LoadAll<EnemyArchetype>("Enemies/Elites/Act1");
    }

    private void Start() {
        StartChoosingEnemy();
    }

    private void SpawnNormalEnemies(int floor) {
        foreach (EnemyChoice choice in enemyChoices) {
            choice.Initialize(randomNormalEnemy, floor);
        }
    }

    private void SpawnEliteEnemies(int floor) {
        foreach (EnemyChoice choice in enemyChoices) {
            choice.Initialize(randomElites, floor);
        }
    }

    public void StartChoosingEnemy() {
        int floor = GameManager.currentFloor;
        floorText.text = GameManager.currentFloor + "Ãþ";

        if (floor % 5 == 0) {
            SpawnEliteEnemies(floor);
            floorText.color = Color.red;
        }
        else {
            SpawnNormalEnemies(floor);
            floorText.color = Color.white;
        }
    }

    public void EndEnemyChoiceScene(EnemyData selectedEnemyData) {
        GameManager.ToBlackjackScene(selectedEnemyData);
    }
}
