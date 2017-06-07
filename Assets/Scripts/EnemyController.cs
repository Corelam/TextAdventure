using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private GameDialogBoxController dialog;
    private PlayerController player;
    private EquipmentManager equipment;
    private TextController gameController;
    public LevelManager level;

    [HideInInspector] public bool enemy;
    private bool enemyDetected;
    [HideInInspector] public bool enemyNearby;
    [HideInInspector] public bool enemyWounded;              // not used yet
    [SerializeField] [Range(1, 20)] private float enemyMovementSpeed;
    [SerializeField] [Range(0, 60)] private int enemyCountdownTime;

    [HideInInspector] public string[] rooms = { "DinningRoom", "Kitchen", "Hall", "Garage" };

    private string enemyPosition;
    private int roomSelector;       // takes int values from -1 to 1
    private int floorSelector = 0;  // 0 - ground floor, 1 - first floor
    
    [Header("Audio Clips")]
    public AudioClip doorSound;
    public AudioClip neckCrackSound;
    public AudioClip stepsSound;
    public AudioClip stairsSound;

    private bool stepsArePlaying;

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

        GameObject levelManagerObject = GameObject.FindWithTag("LevelManager");
        level = levelManagerObject.GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckIfSameRoom();
    }


    private IEnumerator PlayStepsSound()
    {
        stepsArePlaying = true;
        //dialog.Open("Słyszysz kroki...");
        SoundManager.instance.PlaySingle(stepsSound);
        yield return new WaitForSeconds(stepsSound.length);
        stepsArePlaying = false;
    }

    /// <summary> Method for checking if player is in the same room as enemy. </summary>
    private void CheckIfSameRoom()
    {
        if (player.CurrentRoom().Equals(enemyPosition))
        {
            enemyDetected = true;

            if (!player.Visible())
            {
                //dialog.Open("Słyszysz kroki...");   // zamienić to na dźwięk krokow, który się powtarza
                if (!stepsArePlaying)
                {
                    StartCoroutine(PlayStepsSound());
                }
                enemyNearby = true;
            }
            else if (player.Visible() && equipment.Szlafrok_IsEnabled())
            {
                dialog.Open("Zauważasz ciemną postać. Rzucasz w nią swoim szlafrokiem i uciekasz.");
                equipment.Szlafrok_Enable(false);
                equipment.szlafrok_thrown = true;
                player.PlayerEscape();
                return;
            }
            else
            {
                int escapeChance;
                escapeChance = Random.Range(0, 10);
                
                if (escapeChance <= 6)
                {
                    dialog.Open("Zauważasz ciemną postać. Nie masz się czym obronić. Udaje Ci się jednak uciec.");
                    player.PlayerEscape();
                }
                else
                {
                    SoundManager.instance.PlaySingle(neckCrackSound);
                    GameOverController.GameOverText = "Ciemna postać złapała Cię. Nie miałeś się czym obronić. Twój kark chrupnął jej w rękach niczym gałązka.\n\nNie żyjesz.";
                    level.LoadLevel("Lose");
                }
            }
        }
        else
        {
            enemyNearby = false;
        }
    }

    public bool EnemyDetected()
    {
        return enemyDetected;
    }

    /// <summary> Method used to increase/reduce enemy's time needed to move to the other room. </summary>
    public void EnemyMovementSpeed(float value)
    {
        enemyMovementSpeed += value;
    }

    /// <summary> Start countdown before enemy shows up in the scene. </summary>
    public IEnumerator EnemyCountdown()
    {
        Debug.Log("Enemy Countdown started...");
        yield return new WaitForSeconds(enemyCountdownTime);
        enemy = true;
        dialog.Open("Słyszysz dziwny dźwięk...");       // System antywłamaniowy, który zablokował od środka wszystkie drzwi i musisz znaleźć kod, ponieważ go nie pamiętasz
        SoundManager.instance.PlaySingle(doorSound);
        StartCoroutine(EnemyMove());
        yield break;
    }
    
    /// <summary> Method responsible for moving the enemy to the rooms. </summary>
    private IEnumerator EnemyMove()
    {
        Debug.Log("Enemy started to move...");
        int arrayPosition = 2;
        enemyPosition = rooms[arrayPosition];

        while (true)
        {
            Debug.Log("Enemy select " + enemyPosition);

            yield return new WaitForSeconds(enemyMovementSpeed);
            
            // if enemy is in Hall or UpperHall, draw floor level 
            if (enemyPosition.Equals("Hall") || enemyPosition.Equals("UpperHall"))
            {
                floorSelector = Random.Range(0, 2);

                if (!gameController.chamberUnlocked && floorSelector.Equals(0))
                {
                    arrayPosition = 2;
                    rooms = new string[] { "DinningRoom", "Kitchen", "Hall", "Garage" };
                    //Debug.Log("Enemy goes DOWNstairs.");
                }
                else if (gameController.chamberUnlocked && floorSelector.Equals(0))
                {
                    arrayPosition = 2;
                    rooms = new string[] { "DinningRoom", "Kitchen", "Hall", "Garage", "Chamber" };
                }
                else if (floorSelector.Equals(1))
                {
                    arrayPosition = 0;
                    rooms = new string[] { "UpperHall", "Bedroom", "Bathroom" };
                    //Debug.Log("Enemy goes UPstairs.");
                }
            }


            // room entering conditions
            if (enemyPosition.Equals("Hall") && floorSelector.Equals(0))   // so the enemy could move from here to other rooms, not only Garage or Kitchen
            {
                if (!gameController.chamberUnlocked)
                {
                    roomSelector = Random.Range(-2, 2);
                }
                else
                {
                    roomSelector = Random.Range(-2, 3);
                }
            }
            else if (enemyPosition.Equals("Hall") && floorSelector.Equals(1))
            {
                roomSelector = 0;
                SoundManager.instance.PlaySingle(stairsSound);
            }
            else if (enemyPosition.Equals("UpperHall") && floorSelector.Equals(0))
            {
                roomSelector = 0;
                SoundManager.instance.PlaySingle(stairsSound);
            }
            else if (floorSelector.Equals(0))
            {
                if (enemyPosition.Equals("Garage") && !gameController.chamberUnlocked)
                {
                    roomSelector = Random.Range(-1, 1);
                }
                else
                {
                    roomSelector = Random.Range(-1, 2);
                }
            }
            else if (floorSelector.Equals(1))
            {
                if (enemyPosition.Equals("UpperHall"))
                {
                    roomSelector = Random.Range(0, 3);
                }
                else if (enemyPosition.Equals("Bathroom"))
                {
                    if (Random.Range(-1, 1).Equals(0))              // stay in the same room
                    {
                        roomSelector = 0;
                    }
                    else if (Random.Range(-1, 1).Equals(-1))        // go to UpperHall (for not being able to get to bedroom from bathroom directly)
                    {
                        roomSelector = -2;
                    }
                }
                else if (enemyPosition.Equals("Bedroom"))
                {
                    roomSelector = Random.Range(-1, 1);
                }
            }
            
            
            arrayPosition += roomSelector;
            //Debug.Log(arrayPosition);

            if (floorSelector.Equals(0))
            {
                if (arrayPosition.Equals(5) || arrayPosition.Equals(-1))
                {
                    arrayPosition = 2;
                }
            }
            else if (floorSelector.Equals(1))
            {
                if (arrayPosition.Equals(3) || arrayPosition.Equals(-1) || arrayPosition.Equals(4))
                {
                    arrayPosition = 0;
                }
            }

            enemyPosition = rooms[arrayPosition];
        }
    }
}
