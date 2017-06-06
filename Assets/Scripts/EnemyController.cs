using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameDialogBoxController dialog;
    private PlayerController player;
    private EquipmentManager equipment;
    private TextController gameController;

    public LevelManager levelManager;

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

        GameObject equipmentManagerObject = GameObject.FindWithTag("EquipmentManager");
        equipment = equipmentManagerObject.GetComponent<EquipmentManager>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<TextController>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckIfSameRoom();
    }


    /// <summary> Method for checking if player is in the same room as enemy. </summary>
    private void CheckIfSameRoom()
    {
        if (player.CurrentRoom().Equals(enemyPosition))
        {
            if (equipment.Szlafrok_IsEnabled())
            {
                dialog.Open("Zauważasz ciemną postać. Rzucasz w nią swoim szlafrokiem i uciekasz.");
                equipment.Szlafrok_Enable(false);
                PlayerEscape();
                return;
            }
            else
            {
                int escapeChance;
                escapeChance = Random.Range(0, 10);
                
                if (escapeChance <= 5)
                {
                    dialog.Open("Zauważasz ciemną postać. Nie masz się czym obronić. Udaje Ci się jednak uciec.");
                    PlayerEscape();
                }
                else
                {
                    levelManager.LoadLevel("Lose");
                }
            }
        }
    }

    /// <summary> Method responsible for changing player's position based on current location when enemy is in the same room. </summary>
    private void PlayerEscape()
    {
        if (player.CurrentRoom().Equals("DinningRoom") || player.CurrentRoom().Equals("Kitchen") || player.CurrentRoom().Equals("Garage") || player.CurrentRoom().Equals("?"))
        {
            gameController.SetState("Hall");
        }
        else if (player.CurrentRoom().Equals("Hall"))
        {
            int roomSelector;

            // loop to randomly change room except changing it back to "Hall"
            for (;;)
            {
                roomSelector = Random.Range(0, 5);

                if (!roomSelector.Equals(2))
                {
                    break;
                }
            }
            gameController.SetState(groundRooms[roomSelector]);
        }
    }

    public bool EnemyDetected()
    {
        return enemyDetected;
    }


    /// <summary> Start countdown before enemy shows up in the scene. </summary>
    public IEnumerator EnemyCountdown()
    {
        Debug.Log("Enemy Countdown started...");
        yield return new WaitForSeconds(5);
        enemy = true;
        dialog.Open("Słyszysz dziwny dźwięk...");       // System antywłamaniowy, który zablokował od środka wszystkie drzwi i musisz znaleźć kod, ponieważ go nie pamiętasz
        StartCoroutine(EnemyMove());
        yield break;
    }


    /// <summary> Method responsible for moving the enemy to the rooms. </summary>
    private IEnumerator EnemyMove()
    {
        Debug.Log("Enemy started to move...");
        int arrayPosition = 2;
        enemyPosition = groundRooms[arrayPosition];

        while (true)
        {
            yield return new WaitForSeconds(5);
            
            if (enemyPosition.Equals("Hall"))   // so the enemy could move from here to other rooms, not only Garage or Kitchen
            {
                roomSelector = Random.Range(-2, 3);
            }
            else
            {
                roomSelector = Random.Range(-1, 2);
            }
            
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
