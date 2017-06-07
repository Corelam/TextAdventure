using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private TextController gameController;
    private EnemyController enemy;

    private bool bleeding;
    private bool visible;
    
    private string currentRoom;         // not used yet         // to check if enemy is in the same room as player

    // Use this for initialization
    void Start () {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<TextController>();

        GameObject enemyControllerObject = GameObject.FindWithTag("Enemy");
        enemy = enemyControllerObject.GetComponent<EnemyController>();


        bleeding = false;
        visible = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Statuses

    #region Bleeding
    public bool Bleeding()
    {
        return bleeding;
    }

    public void Bleeding_Set(bool status)
    {
        bleeding = status;
    }
    #endregion

    #region Visibility
    /// <summary> Return true if player is visible or false else. </summary>
    public bool Visible()
    {
        return visible;
    }

    public void Visibility_Set(bool status)
    {
        visible = status;
    }
    #endregion

    #endregion

    public void SetCurrentRoom(string room)
    {
        currentRoom = room;
    }

    public string CurrentRoom()
    {
        return currentRoom;
    }

    /// <summary> Method responsible for changing player's position based on current location when enemy is in the same room. </summary>
    public void PlayerEscape()
    {
        if (currentRoom.Equals("DinningRoom") || currentRoom.Equals("Kitchen") || currentRoom.Equals("Garage") || currentRoom.Equals("Chamber"))
        {
            gameController.SetState("Hall");
        }
        else if (currentRoom.Equals("Hall"))
        {
            int roomSelector;

            // loop to randomly change room except changing it back to "Hall"
            for (;;)
            {
                if (gameController.chamberUnlocked)
                {
                    roomSelector = Random.Range(0, 5);
                }
                else
                {
                    roomSelector = Random.Range(0, 4);
                }

                if (!roomSelector.Equals(2))
                {
                    break;
                }
            }
            gameController.SetState(enemy.rooms[roomSelector]);
        }
        else if (currentRoom.Equals("UpperHall"))
        {
            int roomSelector;

            for (;;)
            {
                roomSelector = Random.Range(-1, 3);

                if (!roomSelector.Equals(0))
                {
                    break;
                }
            }

            if (roomSelector.Equals(-1))
            {
                gameController.SetState("Hall");
            }
            else
            {
                gameController.SetState(enemy.rooms[roomSelector]);
            }
        }
        else if (currentRoom.Equals("Bedroom") || currentRoom.Equals("Bathroom"))
        {
            gameController.SetState("UpperHall");
        }

        enemy.EnemyMovementSpeed(-1.0f);           // speeds up enemy movement time every time he sees you - by 1 second
    }

}
