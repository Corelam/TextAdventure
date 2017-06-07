using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverTextManager : MonoBehaviour {

    Text text;
    //Scene currentScene;
    //string sceneName;

    void Start () {
        text = GetComponent<Text>();

        //currentScene = SceneManager.GetActiveScene();
        //sceneName = currentScene.name;
    }

    void Update()
    {
        text.text = GameOverController.GameOverText;        // set Win/Lose text
    }
}
