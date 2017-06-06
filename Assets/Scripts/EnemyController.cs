using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameDialogBoxController dialog;
    private PlayerController player;

    private bool enemy;
    private bool enemyDetected;             // not used yet

    private string[] groundRooms = { "DinningRoom", "Kitchen", "Hall", "Garage", "?" };
    private string[] floorRooms = { "Bedroom", "UpperCorridor", "Bathroom" };

    private string enemyPosition;
    private int roomSelector;       // takes int values from -1 to 1

    // Use this for initialization
    void Start () {
        GameObject gameDialogboxControllerObject = GameObject.FindWithTag("DialogBox");
        dialog = gameDialogboxControllerObject.GetComponent<GameDialogBoxController>();

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        player = playerControllerObject.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyPosition.Equals(player.CurrentRoom()))
        {
            dialog.Open("WIDZISZ OBCĄ OSOBĘ");
        }
	}

    public bool EnemyDetected()
    {
        return enemyDetected;
    }

    public IEnumerator EnemyCountdown()
    {
        Debug.Log("Enemy Countdown started...");
        yield return new WaitForSeconds(5);
        enemy = true;
        dialog.Open("Słyszysz dziwny dźwięk...");       // System antywłamaniowy, który zablokował od środka wszystkie drzwi i musisz znaleźć kod, ponieważ go nie pamiętasz
        StartCoroutine(EnemyMove());
        yield break;
    }

    private IEnumerator EnemyMove()
    {
        Debug.Log("Enemy started to move...");
        int arrayPosition = 2;
        enemyPosition = groundRooms[arrayPosition];

        while (true)
        {
            yield return new WaitForSeconds(5);
            roomSelector = Random.Range(-1, 2);
            arrayPosition += roomSelector;
            if (arrayPosition.Equals(5) || arrayPosition.Equals(-1))
            {
                arrayPosition = 2;
            }

            enemyPosition = groundRooms[arrayPosition];
            Debug.Log("Enemy select " + enemyPosition);
        }
    }
}
