using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;
    private enum States {
        bedroom_0_0, window_0, bedroom_0_1, upperCorridor_0, stairs_0,
        hall_0, bathroom_0
    };
    private States myState;
    private States previousState;       // not used yet
    
    private string currentRoom;         // not used yet         // to check if enemy is in the same room as player

    //private DialogBoxController dialog;
    private EquipmentManager equipment;
    private GameTimeController gameTime;
    private EnemyController enemy;

    private GameDialogBoxController dialog;

    //Awake(){}

    void Start () {
        myState = States.bedroom_0_0;

        /*GameObject dialogboxControllerObject = GameObject.FindWithTag("DialogBox");
        if (dialogboxControllerObject != null) {
            dialog = dialogboxControllerObject.GetComponent<DialogBoxController>();
        }
        if (dialogboxControllerObject == null) {
            Debug.Log("Cannot find 'DialogBoxController' script.");
        }*/

        GameObject gameDialogboxControllerObject = GameObject.FindWithTag("DialogBox");
        if (gameDialogboxControllerObject != null) {
            dialog = gameDialogboxControllerObject.GetComponent<GameDialogBoxController>();
        }
        if (gameDialogboxControllerObject == null) {
            Debug.Log("Cannot find 'GameDialogBoxController' script.");
        }

        GameObject equipmentManagerObject = GameObject.FindWithTag("EquipmentManager");
        if (equipmentManagerObject != null) {
            equipment = equipmentManagerObject.GetComponent<EquipmentManager>();
        }
        if (equipmentManagerObject == null) {
            Debug.Log("Cannot find 'EquipmentManager' script.");
        }

        GameObject gametimeControllerObject = GameObject.FindWithTag("GameTime");
        if (gametimeControllerObject != null) {
            gameTime = gametimeControllerObject.GetComponent<GameTimeController>();
        }
        if (gametimeControllerObject == null) {
            Debug.Log("Cannot find 'GameTimeController' script.");
        }

        GameObject enemyControllerObject = GameObject.FindWithTag("Enemy");
        if (enemyControllerObject != null) {
            enemy = enemyControllerObject.GetComponent<EnemyController>();
        }
        if (enemyControllerObject == null) {
            Debug.Log("Cannot find 'EnemyController' script.");
        }
    }
	
	void Update () {
        if      (myState == States.bedroom_0_0)     { bedroom_0_0(); currentRoom = "Bedroom"; }
        else if (myState == States.window_0)        { window_0(); currentRoom = "Bedroom"; }
        else if (myState == States.bedroom_0_1)     { bedroom_0_1(); currentRoom = "Bedroom"; }
        else if (myState == States.upperCorridor_0) { upperCorridor_0(); currentRoom = "UpperCorridor"; }
        else if (myState == States.bathroom_0)      { bathroom_0(); currentRoom = "Bathroom"; }
        else if (myState == States.hall_0)          { hall_0(); currentRoom = "Hall"; }
    }

    #region Rooms methods

    void bedroom_0_0()
    {
        text.text = "Budzisz się z mokrym czołem. Pewnie przez koszmar, który przed chwilą Ci się śnił. Wszędzie jest ciemno.\n" +
                    "Spoglądasz na budzik, leżący na stoliku nocnym. Jest " + gameTime.hours + ":" + gameTime.minutes + ".\n" +
                    "\n-> spójrz przez [O]kno" +
                    "\n-> wyjdź na [K]orytarz";

        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.window_0; }
        else if (Input.GetKeyDown(KeyCode.K)) { myState = States.upperCorridor_0; }
    }

    void window_0()
    {
        text.text = "W półmroku podchodzisz do okna. Spoglądasz przez nie. Dzisiejszej nocy jest wyjątkowo ciemno. " +
                    "Księżyc ledwie widoczny spośród czarnych jak smoła chmur. Silny wiatr zakrzywia korony drzew swoim oddechem.\n" +
                    "Gdzieś na ulicy w oddali widać jarzącą się żółtym światłem latarnię.\n\n" +
                    "W momencie, gdy odwracasz się od okna, słyszysz puknięcie w szybę. Niepewnie ponownie spoglądasz przez okno. " +
                    "Okazuje się, że na parapecie leży gałąź. Myślisz sobie, że to ona uderzyła właśnie w okno.\n" +
                    "\n-> [Z]ignoruj i wróć do pokoju.";

        if (Input.GetKeyDown(KeyCode.Z)) { myState = States.bedroom_0_1; }
    }

    void bedroom_0_1()
    {
        text.text = "W blasku księżyca dostrzegasz swoje łóżko. Leży na nim rozgrzebana pościel i poduszka z odgniecionym śladem Twojej głowy. " +
                    "Jest całkowicie cicho. W pokoju panuje chłód. ";

        if (!equipment.Shoes_IsEnabled()) {
            text.text += "Panele są zimne, marzną Ci stopy.\n";
            text.text += "\n-> załóż [B]uty.";
        }

        if (!equipment.Phone_IsEnabled() && equipment.Shoes_IsEnabled()) {
            text.text += "\nPrzy zakładaniu butów kątem oka zauważasz telefon, znajdujący się na szafce nocnej.\n" +
                         "\n-> weź [T]elefon z szafki nocnej.";
        }

        if (!equipment.Szlafrok_IsEnabled()) {
            text.text += "\n-> otwórz szafę i załóż [S]zlafrok.";
        }
        
        text.text += "\n-> wyjdź na [K]orytarz.";

        if      (Input.GetKeyDown(KeyCode.T) && !equipment.Phone_IsEnabled())       { equipment.Phone_Enable(); myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.K))                                       { myState = States.upperCorridor_0; previousState = States.bedroom_0_1; StartCoroutine(enemy.EnemyCountdown()); }
        else if (Input.GetKeyDown(KeyCode.B) && !equipment.Shoes_IsEnabled())       { equipment.Shoes_Enable(); myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.S) && !equipment.Szlafrok_IsEnabled())    { equipment.Szlafrok_Enable(); myState = States.bedroom_0_1; dialog.Open("Otwierasz szafę, wyciągasz swój ulubiony szlafrok. Ubierasz go. Czujesz mięciutki dotyk materiału.\n\n\"Ah... jak przyjemnie...\""); }
    }

    void upperCorridor_0()
    {
        text.text = "Jesteś na korytarzu. ";

        if (previousState == States.bedroom_0_1)
        {
            text.text += "Po plecach przechodzą Ci ciarki. Jest o wiele ciemniej niż było w sypialni.\n";
        }

        text.text += "\n-> [W]łącz światło" +
                     "\n-> [Z]ejdź po schodach" +
                     "\n-> Idź do [S]ypialni" +
                     "\n-> Idz do [Ł]azienki";

        if      (Input.GetKeyDown(KeyCode.W))                               { dialog.Open("Światło nie działa. Pewnie korki wysiadły."); }
        else if (Input.GetKeyDown(KeyCode.Z))                               { myState = States.hall_0; }
        else if (Input.GetKeyDown(KeyCode.S) && !enemy.EnemyDetected())     { myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.L))                               { myState = States.bathroom_0; }
    }

    void hall_0()
    {
        text.text = "";

        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.window_0; }
        else if (Input.GetKeyDown(KeyCode.K)) { myState = States.upperCorridor_0; }
    }

    void bathroom_0()
    {
        text.text = "";

        if (Input.GetKeyDown(KeyCode.O)) { myState = States.window_0; }
        else if (Input.GetKeyDown(KeyCode.K)) { myState = States.upperCorridor_0; }
    }

    #endregion

}
