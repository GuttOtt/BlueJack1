using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseEnemyScene : Singleton<ChooseEnemyScene> {


    public void StartEnemyChoiceScene() {
        SceneManager.LoadScene("Choose Enemy Scene");
    }
}
