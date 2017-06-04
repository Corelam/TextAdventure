using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameDialogBoxController dialog;

    private bool enemy;
    private bool enemyDetected;             // not used yet

    // Use this for initialization
    void Start () {
        GameObject gameDialogboxControllerObject = GameObject.FindWithTag("DialogBox");
        dialog = gameDialogboxControllerObject.GetComponent<GameDialogBoxController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool EnemyDetected()
    {
        return enemyDetected;
    }

    public IEnumerator EnemyCountdown()
    {
        Debug.Log("Enemy Countdown started...");
        yield return new WaitForSeconds(15);
        enemy = true;
        dialog.Open("Słyszysz dziwny dźwięk...");
        yield break;
    }
}
