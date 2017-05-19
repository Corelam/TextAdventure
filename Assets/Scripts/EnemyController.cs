using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private DialogBoxController dialog;

    private bool enemy;
    private bool enemyDetected;             // not used yet

    // Use this for initialization
    void Start () {
        GameObject dialogboxControllerObject = GameObject.FindWithTag("DialogBox");
        dialog = dialogboxControllerObject.GetComponent<DialogBoxController>();
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
        yield return new WaitForSeconds(60);
        enemy = true;
        dialog.Open("Słyszysz dziwny dźwięk...");
        yield break;
    }
}
