using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;
    private enum States {
        bedroom_0_0, window_0, bedroom_0_1, upperCorridor_0, stairs_0
    };
    private States myState;

    private int hours = 2;
    private int minutes = 37;
    private float seconds = 0.0f;

    //Awake(){}

    public Image phoneI; 
    private bool phone;
    public Text phoneClock;

    private bool doorKey;
    private bool closetKey;
    private bool pistol;
    private bool butterKnife;

    // Use this for initialization
    void Start () {
        phoneI.enabled = false;
        phoneClock.enabled = false;

        myState = States.bedroom_0_0;
	}
	
    void Clock()
    {
        seconds += Time.deltaTime;

        if (seconds >= 60)
        {
            minutes++;
            seconds = 0.0f;
        }

        if (minutes >= 60)
        {
            hours++;
            minutes = 0;
        }

        if (hours >= 24)
        {
            hours = 0;
        }
    }

	// Update is called once per frame
	void Update () {
        Clock();

        if      (myState == States.bedroom_0_0)     { bedroom_0_0(); }
        else if (myState == States.window_0)        { window_0(); }
        else if (myState == States.bedroom_0_1)     { bedroom_0_1(); }
    }

    #region Equipment functions

    #region Phone
    public void Phone_use()
    {
        StartCoroutine(PhoneClock());
    }

    IEnumerator PhoneClock()
    {
        phoneClock.enabled = true;

        for (int i = 0; i<3; i++)
        {
            phoneClock.text = hours + ":" + minutes;
            yield return new WaitForSeconds(0.75F);
            phoneClock.text = hours + " " + minutes;
            yield return new WaitForSeconds(0.75F);
        }
        phoneClock.enabled = false;
    }
    #endregion
    
    #endregion


    #region Rooms functions

    void bedroom_0_0()
    {
        text.text = "Budzisz się w nocy. Spoglądasz na zegar. Jest " + hours + ":" + minutes + ".\n" +
                    "Nie możesz spać. Masz przeczucie, jakby coś tej nocy miało się wydarzyć.\n\n" +
                    "-> spójrz przez [O]kno \n-> wyjdź na [K]orytarz";

        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.window_0; }
        else if (Input.GetKeyDown(KeyCode.K)) { myState = States.upperCorridor_0; }
    }

    void window_0()
    {
        text.text = "Spoglądasz przez okno. Dzisiejszej nocy jest wyjątkowo ciemno. Blask księżyca niewidoczny, schowany za pobliskim lasem. " +
                    "Silny wiatr zakrzywia korony drzew swoim oddechem.\n" +
                    "Gdzieś na ulicy w oddali widać jarzącą się mocnym, żółtym światłem latarnię.\n\n" +
                    "-> [O]dsuń się od okna";

        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.bedroom_0_1; }
    }

    void bedroom_0_1()
    {
        text.text = "Na łóżku leży rozgrzebana pościel i poduszka z odgniecionym śladem Twojej głowy.\n" +
                    "";

        if (phone == false)
        {
            text.text += "\n-> weź [T]elefon z szafki nocnej.";
        }

        if      (Input.GetKeyDown(KeyCode.T) && !phone) { myState = States.bedroom_0_1; phone = true; phoneI.enabled = true; }
        else if (Input.GetKeyDown(KeyCode.K))           { myState = States.upperCorridor_0; }
    }

 
    #endregion
}
