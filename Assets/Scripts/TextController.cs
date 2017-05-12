using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;
    private enum States {
        cell, mirror, sheets_0, lock_0, cell_mirror, sheets_1, lock_1, 
        corridor_0
    };
    private States myState;

	// Use this for initialization
	void Start () {
        myState = States.cell;
	}
	
	// Update is called once per frame
	void Update () {
        print(myState);

        if      (myState == States.cell)        { cell(); }
        else if (myState == States.sheets_0)    { sheets_0(); }
        else if (myState == States.sheets_1)    { sheets_1(); }
        else if (myState == States.lock_0)      { lock_0(); }
        else if (myState == States.lock_1)      { lock_1(); }
        else if (myState == States.mirror)      { mirror(); }
        else if (myState == States.cell_mirror) { cell_mirror(); }
        else if (myState == States.corridor_0)  { corridor_0(); }
    }

    #region State handler methods
    void cell() {
        text.text = "Znajdujesz się w celi więziennej, z której chcesz uciec. " +
                    "Na łóżku leży brudna pościel, na ścianie wisi lustro, a drzwi są zamknięte od zewnątrz.\n\n" +
                    "Wciśnij P aby spojrzeć na Pościel, L na Lustro, Z na Zamek w drzwiach";

        if (Input.GetKeyDown(KeyCode.P))            { myState = States.sheets_0; }
        else if (Input.GetKeyDown(KeyCode.L))       { myState = States.mirror; }
        else if (Input.GetKeyDown(KeyCode.Z))       { myState = States.lock_0; }
    }

    void mirror() {
        text.text = "Stare, brudne lustro wisi na ścianie.\n\n" +
                    "Wciśnij Z żeby Zabrać lustro lub TAB żeby powrócić do widoku celi.";

        if (Input.GetKeyDown(KeyCode.Z))            { myState = States.cell_mirror; }
        else if (Input.GetKeyDown(KeyCode.Tab))     { myState = States.cell; }
    }

    void cell_mirror() {
        text.text = "Nadal jesteś w celi i wciąż chcesz stąd zwiać. Na łóżku znajdują się brudne prześcieradła, " +
                    "na ścianie hak, na którym wisiało lustro oraz drzwi na hasło, zamknięte na amen.\n\n" +
                    "Wciśnij P aby spojrzeć na Pościel, Z na Zamek";

        if (Input.GetKeyDown(KeyCode.P))            { myState = States.sheets_1; }
        else if (Input.GetKeyDown(KeyCode.Z))       { myState = States.lock_1; }
    }

    void sheets_0() {
        text.text = "Nie możesz uwierzyć, że na tym śpisz. " +
                    "Na pewno już czas, żeby ją wymienić. Zalety bycia w więzieniu...\n\n" +
                    "Wciśnij TAB, żeby powrócić do widoku celi.";

        if (Input.GetKeyDown(KeyCode.Tab))          { myState = States.cell; }
    }

    void sheets_1() {
        text.text = "Trzymanie lustra w ręku wcale nie upiększa tego prześcieradła.\n\n" +
                    "Wciśnij TAB, żeby powrócić do widoku celi.";

        if (Input.GetKeyDown(KeyCode.Tab))          { myState = States.cell_mirror; }
    }

    void lock_0() {
        text.text = "Zamek z kodem. Nie wiesz jednak, jaki jest do niego szyfr. " +
                    "Marzysz o tym, żeby móc dostrzec brud na przyciskach. Może to by pomogło.\n\n" +
                    "Wciśnij TAB, żeby powrócić do widoku celi.";

        if (Input.GetKeyDown(KeyCode.Tab))          { myState = States.cell; }
    }

    void lock_1() {
        text.text = "Ostrożnie wsuwasz lustro przez więzienne kraty i obracasz tak, żeby " +
                    "zobaczyć zamek. Widzisz, na których klawiszach farba jest bardziej zużyta. " +
                    "Próbujesz nacisnąć kombinację i nagle słyszysz klik.\n\n" +
                    "Wciśnij O aby Otworzyć drzwi lub TAB aby powrócić do swojej celi.";

        if (Input.GetKeyDown(KeyCode.O))            { myState = States.corridor_0; }
        else if (Input.GetKeyDown(KeyCode.Tab))     { myState = States.cell_mirror; }
    }

    void corridor_0()
    {
        text.text = "Wyszedłeś na korytarz.\n\n" +
                    "Wciśnij R, żeby zagrać ponownie.";

        if (Input.GetKeyDown(KeyCode.R))  { myState = States.cell; }
    }
    #endregion
}
