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

    public DialogBoxController dialog;
    public EquipmentManager equipment;
    public GameTimeController gameTime;

    private bool enemy;
    private bool enemySeen;             // not used yet

    //Awake(){}

    void Start () {
        myState = States.bedroom_0_0;
        //EquipmentManager equipment = GetComponent<EquipmentManager>();
        //equipment.
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
        text.text = "Budzisz się w nocy. Spoglądasz na budzik, leżący na stoliku nocnym. Jest " + gameTime.hours + ":" + gameTime.minutes + ".\n" +
                    "Nie możesz spać. Masz przeczucie, jakby coś tej nocy miało się wydarzyć." +
                    "\n-> spójrz przez [O]kno" +
                    "\n-> wyjdź na [K]orytarz";

        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.window_0; }
        else if (Input.GetKeyDown(KeyCode.K)) { myState = States.upperCorridor_0; }
    }

    void window_0()
    {
        text.text = "W półmroku podchodzisz do okna. Spoglądasz przez nie. Dzisiejszej nocy jest wyjątkowo ciemno. Blask księżyca niewidoczny, " +
                    "schowany za pobliskim lasem. Silny wiatr zakrzywia korony drzew swoim oddechem.\n" +
                    "Gdzieś na ulicy w oddali widać jarzącą się mocnym, żółtym światłem latarnię." +
                    "\n-> [O]dsuń się od okna";

        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.bedroom_0_1; }
    }

    void bedroom_0_1()
    {
        text.text = "Na łóżku leży rozgrzebana pościel i poduszka z odgniecionym śladem Twojej głowy.\n" +
                    "W pokoju panuje chłód. ";

        if (!equipment.Shoes_IsEnabled()) {
            text.text += "Panele są zimne, marzną Ci stopy.";
            text.text += "\n-> załóż [B]uty.";
        }

        if (!equipment.Phone_IsEnabled() && equipment.Shoes_IsEnabled()) {
            text.text += "Kiedy schylasz się po buty, zauważasz telefon." +
                         "\n-> weź [T]elefon z szafki nocnej.";
        }

        if (!equipment.Szlafrok_IsEnabled()) {
            text.text += "\n-> Otwórz szafę i załóż [S]zlafrok.";
        }
        
        text.text += "\n-> wyjdź na [K]orytarz.";

        if      (Input.GetKeyDown(KeyCode.T) && !equipment.Phone_IsEnabled())       { equipment.Phone_Enable(); myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.K))                                       { myState = States.upperCorridor_0; previousState = States.bedroom_0_1; StartCoroutine(EnemyCountdown()); }
        else if (Input.GetKeyDown(KeyCode.B) && !equipment.Shoes_IsEnabled())       { equipment.Shoes_Enable(); myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.S) && !equipment.Szlafrok_IsEnabled())    { equipment.Szlafrok_Enable(); myState = States.bedroom_0_1; dialog.Open("Otwierasz szafę i ubierasz szlafrok."); }
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

        if      (Input.GetKeyDown(KeyCode.W)) { dialog.Open("Światło nie działa. Pewnie korki wysiadły."); }
        else if (Input.GetKeyDown(KeyCode.Z)) { myState = States.hall_0; }
        else if (Input.GetKeyDown(KeyCode.S) && enemySeen) { myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.bathroom_0; }
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

    IEnumerator EnemyCountdown()
    {
        yield return new WaitForSeconds(60);
        dialog.Open("Słyszysz dziwny dźwięk...");
        yield break;
    }
}
