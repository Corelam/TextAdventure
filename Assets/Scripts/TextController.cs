using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public Text text;
    private enum States {
        bedroom_0_0, window_0, bedroom_0_1, upperHall_0,
        bathroom_0, bathroom_sink, bathroom_mirror, bathroom_shower,
        hall_0, garage_0, chamber_0,
        kitchen_0, dinningRoom_0
    };
    private States myState;
    private States previousState;       // not well used yet, for future development

    //private DialogBoxController dialog;
    private EquipmentManager equipment;
    private GameTimeController gameTime;
    private EnemyController enemy;
    private PlayerController player;
    private LevelManager level;
    private GameDialogBoxController dialog;

    //Awake(){}
    private bool countdownStarted;
    [HideInInspector] public bool chamberUnlocked;
    private bool dinningRoomUnlocked;

    public AudioClip knifeStabSound;

    void Start () {
        myState = States.bedroom_0_0;

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

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            player = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerControllerObject == null)
        {
            Debug.Log("Cannot find 'PlayerController' script.");
        }

        GameObject levelManagerObject = GameObject.FindWithTag("LevelManager");
        if (levelManagerObject != null)
        {
            level = levelManagerObject.GetComponent<LevelManager>();
        }
        if (levelManagerObject == null)
        {
            Debug.Log("Cannot find 'LevelManager' script.");
        }
    }
	
	void Update () {
        if      (myState == States.bedroom_0_0)     { bedroom_0_0(); player.SetCurrentRoom("Bedroom"); }
        else if (myState == States.window_0)        { window_0(); player.SetCurrentRoom("Bedroom"); }
        else if (myState == States.bedroom_0_1)     { bedroom_0_1(); player.SetCurrentRoom("Bedroom"); }
        else if (myState == States.upperHall_0)     { upperHall_0(); player.SetCurrentRoom("UpperHall"); }
        else if (myState == States.bathroom_0)      { bathroom_0(); player.SetCurrentRoom("Bathroom"); }
        else if (myState == States.bathroom_sink)   { bathroom_sink(); player.SetCurrentRoom("Bathroom"); }
        else if (myState == States.bathroom_mirror) { bathroom_mirror(); player.SetCurrentRoom("Bathroom"); }
        else if (myState == States.bathroom_shower) { bathroom_shower(); player.SetCurrentRoom("Bathroom"); }
        else if (myState == States.hall_0)          { hall_0(); player.SetCurrentRoom("Hall"); }
        else if (myState == States.garage_0)        { garage_0(); player.SetCurrentRoom("Garage"); }
        else if (myState == States.kitchen_0)       { kitchen_0(); player.SetCurrentRoom("Kitchen"); }
        else if (myState == States.dinningRoom_0)   { dinningRoom_0(); player.SetCurrentRoom("DinningRoom"); }
        else if (myState == States.chamber_0)       { chamber_0(); player.SetCurrentRoom("Chamber"); }
    }

    /// <summary> Function to set state from outside of this script (ex. EnemyController) </summary>
    public void SetState(string stateName)
    {
        if (stateName.Equals("Hall"))
        {
            myState = States.hall_0;
        }
        else if (stateName.Equals("Garage"))
        {
            myState = States.garage_0;
        }
        else if (stateName.Equals("Kitchen"))
        {
            myState = States.kitchen_0;
        }
        else if (stateName.Equals("DinningRoom"))
        {
            myState = States.dinningRoom_0;
        }
        else if (stateName.Equals("Chamber"))
        {
            myState = States.chamber_0;
        }
        else if (stateName.Equals("UpperHall"))
        {
            myState = States.upperHall_0;
        }
        else if (stateName.Equals("Bathroom"))
        {
            myState = States.bathroom_0;
        }
        else if (stateName.Equals("Bedroom"))
        {
            myState = States.bedroom_0_1;
        }
    }

    #region First floor rooms

    void bedroom_0_0()
    {
        text.text = "Budzisz się z mokrym czołem. Pewnie przez koszmar, który przed chwilą Ci się śnił. Wszędzie jest ciemno.\n" +
                    "Spoglądasz na budzik, leżący na stoliku nocnym. Jest " + gameTime.hours + ":" + gameTime.minutes + ".\n" +
                    "\n-> spójrz przez [O]kno" +
                    "\n-> wyjdź na [K]orytarz";

        if      (Input.GetKeyDown(KeyCode.O)) { myState = States.window_0; }
        else if (Input.GetKeyDown(KeyCode.K)) { myState = States.upperHall_0; StartCoroutine(enemy.EnemyCountdown()); countdownStarted = true; }
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

        if (!equipment.Szlafrok_IsEnabled() && !equipment.szlafrok_thrown) {
            text.text += "\n-> otwórz szafę i załóż [S]zlafrok.";
        }
        
        text.text += "\n-> wyjdź na [K]orytarz.";

        if      (Input.GetKeyDown(KeyCode.T) && !equipment.Phone_IsEnabled())       { equipment.Phone_Enable(); myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (!countdownStarted)
            {
                countdownStarted = true;
                StartCoroutine(enemy.EnemyCountdown());
            }

            myState = States.upperHall_0;
            previousState = States.bedroom_0_1;
        }
        else if (Input.GetKeyDown(KeyCode.B) && !equipment.Shoes_IsEnabled())       { equipment.Shoes_Enable(true); myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.S) && !equipment.Szlafrok_IsEnabled())    { equipment.Szlafrok_Enable(true); myState = States.bedroom_0_1; dialog.Open("Otwierasz szafę, wyciągasz swój ulubiony szlafrok. Ubierasz go. Czujesz mięciutki dotyk materiału.\n\n\"Ah... jak przyjemnie...\""); }
    }

    void upperHall_0()
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
        else if (Input.GetKeyDown(KeyCode.S)) /*&& !enemy.EnemyDetected())*/{ myState = States.bedroom_0_1; }
        else if (Input.GetKeyDown(KeyCode.L))                               { myState = States.bathroom_0; }
    }

    void bathroom_0()
    {
        text.text = "W łazience również korki wysiadły. Słaby blask latarni zza oknem ledwie oświetla podłogę. " +
                    "Oczy masz już przyzwyczajone do ciemności, więc jesteś w stanie dostrzec prysznic, lustro i umywalkę.\n" +
                    "\n-> Podejdź do [U]mywalki." +
                    //"\n-> Spójrz przez [O]kno." +
                    "\n-> Podejdź do [P]rysznica." +
                    "\n-> Idź na [K]orytarz.";

        if      (Input.GetKeyDown(KeyCode.U)) { myState = States.bathroom_sink; }
        //else if (Input.GetKeyDown(KeyCode.O)) { myState = States.upperHall_0; }
        else if (Input.GetKeyDown(KeyCode.P)) { myState = States.bathroom_shower; }
        else if (Input.GetKeyDown(KeyCode.K)) { myState = States.upperHall_0; }
    }

    void bathroom_sink()
    {
        text.text = "Umywalka niczym się nie wyróżnia od zwykłych umywalek. Nad nią znajduje się lekko uchylone lustro. " +
                    "Widzisz w nim tylko swój cień. Jest za ciemno.\n" +
                    "\n-> Rozchyl [L]ustro." +
                    "\n-> [W]róć do łazienki.";

        if      (Input.GetKeyDown(KeyCode.L)) { myState = States.bathroom_mirror; }
        else if (Input.GetKeyDown(KeyCode.W)) { myState = States.bathroom_0; }
    }

    void bathroom_mirror()
    {
        text.text = "Otwierasz lustro. Wewnątrz znajduje się mała apteczka.\n";

        if (player.Bleeding())
        {
            text.text += "\n-> [W]eź apteczkę.";
        }

        text.text += "\n-> Wróć do [Ł]azienki.";

        if      (Input.GetKeyDown(KeyCode.W)) { dialog.Open("Użyłeś apteczki. Udało ci się powstrzymać krwawienie."); player.Bleeding_Set(false); }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.bathroom_0; }
    }

    void bathroom_shower()
    {
        if (player.Visible())
        {
            text.text = "Pod prysznicem nie znajdujesz nic ciekawego. Granatowa zasłona w czerwone rozgwiazdy i białe muszelki sięga do brodzika. " +
                        "Na ścianie znajdują się kosmetyki do mycia ciała.\n";
        }
        else
        {
            text.text = "Siedzisz ukryty za zasłoną.\n";

            if (enemy.enemyNearby && equipment.Knife_IsEnabled() && !enemy.enemyWounded)
            {
                text.text += "Przez myśl przechodzi Ci, żeby zaatakować przeciwnika nożem i uciec..." +
                             "\n-> [Z]aatakuj przeciwnika.";
            }
        }
        
        if (enemy.EnemyDetected() && player.Visible())
        {
            text.text += "\n-> [U]kryj się za zasłoną.";
        }

        text.text += "\n-> Wróć do [Ł]azienki.";

        if      (Input.GetKeyDown(KeyCode.U)) { dialog.Open("Chowasz się za zasłoną."); player.Visibility_Set(false); }
        else if (Input.GetKeyDown(KeyCode.L)) { myState = States.bathroom_0; player.Visibility_Set(true); }
        else if (Input.GetKeyDown(KeyCode.Z) && enemy.enemyNearby && equipment.Knife_IsEnabled() && !enemy.enemyWounded)
        {
            SoundManager.instance.PlaySingle(knifeStabSound);
            dialog.Open("Atakujesz przeciwnika nożem i uciekasz do kuchni. Zyskujesz trochę na czasie.");
            enemy.EnemyMovementSpeed(3);
            myState = States.upperHall_0;
            player.Visibility_Set(true);
        }
    }
    #endregion

    #region Ground floor rooms

    void hall_0()
    {
        text.text = "Jesteś na parterze. Tutaj atmosfera jest bardziej przygnębiająca niż na górze. Naprzeciwko schodów znajdują się drzwi wyjściowe. " +
                    "Na lewo znajduje się kuchnia, a po prawej wyjście do garażu.\n" +
                    "\n-> Idź do [K]uchni." +
                    "\n-> Idź do [G]arażu." +
                    "\n-> Wyjdź na [Z]ewnątrz." +
                    "\n-> Wejdź po [S]chodach na górę.";

        if (dinningRoomUnlocked)
        {
            text.text += "\n-> Idź do [J]adalni.";
        }

        if (chamberUnlocked)
        {
            text.text += "\n-> Idź do [P]okoju.";
        }
        else
        {
            text.text += "\n-> Otwórz [D]rzwi do pokoju.";
        }

        if      (Input.GetKeyDown(KeyCode.K)) { myState = States.kitchen_0; }
        else if (Input.GetKeyDown(KeyCode.G)) { myState = States.garage_0; }
        else if (Input.GetKeyDown(KeyCode.J)) { myState = States.dinningRoom_0; }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (chamberUnlocked)
            {
                myState = States.chamber_0;
            }
            else
            {
                dialog.Open("Drzwi są zamknięte od środka.");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!enemy.enemy)
            {
                dialog.Open("Naciskasz klamkę, żeby otworzyć drzwi. Drzwi się otwierają. Jest ciemno. Nikogo nie widać na zewnątrz. Zamykasz je za sobą.");
            }
            else if (!equipment.DoorCode_IsEnabled() && enemy.enemy)
            {
                dialog.Open("Naciskasz klamkę, żeby otworzyć drzwi. Drzwi nie otwierają się. Włączył się system antywłamaniowy i potrzebny jest kod. " +
                            "W drzwiach znajduje się prostokątne, mleczne szkło, przez które widać różne cienie.");
            }
            else if (equipment.DoorCode_IsEnabled() && enemy.enemy && enemy.EnemyDetected())
            {
                GameOverController.GameOverText = "Otwierasz drzwi. Udaje Ci się uciec...";
                level.LoadLevel("Win");
            }
        }
        else if (Input.GetKeyDown(KeyCode.S)) { myState = States.upperHall_0; }
    }

    void kitchen_0()
    {
        text.text = "Rozglądasz się po kuchni. Na podłodze cienie drzew tańczą niespokojnie. Coraz bardziej uginają się od wiatru, " +
                    "wydając złowrogi dźwięk. Patrzysz w lewo - jesteś w stanie dostrzec radio. Patrzysz na wprost - na blacie coś się połyskuje. " +
                    "Podchodzisz bliżej. Okazuje się, że to nóż kuchenny. W głębi kuchni widzisz drzwi, które prowadzą do kolejnego pomieszczenia.\n";

        if (enemy.EnemyDetected() && !equipment.Knife_IsEnabled())
        {
            text.text += "\n-> Weź [N]óż z blatu.";
        }

        text.text += "\n-> Idź do następnego [P]omieszczenia." +
                     "\n-> [W]róć na korytarz.";

        if (equipment.Knife_IsEnabled() && equipment.doorCodeLocation.Equals("Knife") && !equipment.DoorCode_IsEnabled())
        {
            text.text += "\n\n Po podniesieniu noża dostrzegasz, że coś jeszcze leży. Przyglądasz się bliżej. To jakaś mała karteczka." +
                         "\n-> Weź [K]artkę z kodem.";
        }

        if      (Input.GetKeyDown(KeyCode.N)) { equipment.Knife_Enable(); dialog.Open("Podnosisz nóż z blatu."); }
        else if (Input.GetKeyDown(KeyCode.P)) { myState = States.dinningRoom_0; }
        else if (Input.GetKeyDown(KeyCode.W)) { myState = States.hall_0; }
        else if (Input.GetKeyDown(KeyCode.K) && equipment.doorCodeLocation.Equals("Knife") && !equipment.DoorCode_IsEnabled())
        {
            equipment.DoorCode_Enable();
            dialog.Open("Podnosisz karteczkę. Okazuje się, że jest na niej zapisany kod do drzwi wyjściowych.");
        }
    }

    void dinningRoom_0()
    {
        dinningRoomUnlocked = true;

        if (player.Visible())
        {
            text.text = "W bladym świetle dostrzegasz na środku pomieszczenia okrągły stół z dwoma krzesłami. " +
                        "Jest na tyle spory, że z powodzeniem może schować się pod nim osoba. Stół pokryty jest białym, " +
                        "koronkowym obrusem sięgającym do ziemi. Za nim znajduje się kredens. To jadalnia.\n" +
                        "Na prawo znajduje się wyjście na korytarz.\n" +
                        "\n-> [W]róć do kuchni." +
                        "\n-> Zajrzyj do [K]redensu." +
                        "\n-> [I]dź na korytarz.";
        }
        else
        {
            text.text = "Nie widzisz nic innego poza obrusem zwisającym ze stołu. Wydawałoby się, że słychać jak ktoś chodzi, " +
                        "ale nie wiesz, czy to tylko omamy. To pewnie przez nienaturalny świst drzew na dworze.\n";

            if (!equipment.Matches_IsEnabled())
            {
                text.text += "Pod stołem znajdujesz paczkę zapałek." +
                             "\n-> Weź [Z]apałki";
            }

            if (enemy.enemyNearby && equipment.Knife_IsEnabled() && !enemy.enemyWounded)
            {
                text.text += "Przez myśl przechodzi Ci, żeby dźgnąć przeciwnika nożem w stopę i uciec..." +
                             "\n-> Spróbuj go [D]źgnąć.";
            }

            text.text += "\n-> Wyjdź z [U]krycia.";
        }

        if (enemy.EnemyDetected() && player.Visible())
        {
            text.text += "\n-> [S]chowaj się pod stół.";
        }

        if      (Input.GetKeyDown(KeyCode.S)) { dialog.Open("Chowasz się pod stołem."); player.Visibility_Set(false); }
        else if (Input.GetKeyDown(KeyCode.U)) { myState = States.dinningRoom_0; player.Visibility_Set(true); }
        else if (Input.GetKeyDown(KeyCode.Z) && !equipment.Matches_IsEnabled()) { dialog.Open("Podnosisz zapałki."); equipment.Matches_Enable(); }
        else if (Input.GetKeyDown(KeyCode.W)) { myState = States.kitchen_0; }
        else if (Input.GetKeyDown(KeyCode.D) && enemy.enemyNearby && equipment.Knife_IsEnabled() && !enemy.enemyWounded)
        {
            SoundManager.instance.PlaySingle(knifeStabSound);
            dialog.Open("Atakujesz przeciwnika nożem i uciekasz do kuchni. Zyskujesz trochę na czasie.");
            enemy.EnemyMovementSpeed(3);
            myState = States.kitchen_0;
            player.Visibility_Set(true);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (equipment.doorCodeLocation.Equals("Sideboard") && !equipment.DoorCode_IsEnabled())
            {
                dialog.Open("Między talerzami dostrzegasz jakąś rzecz. Sięgasz po nią. Okazuje się, że to kartka z kodem do drzwi wyjściowych. Zabierasz ją.");
                equipment.DoorCode_Enable();
            }
            else
            {
                dialog.Open("Otwierasz drzwiczki. Wydobywa się zapach starych rzeczy. Zestaw talerzy był używany z rzadka, tylko na specjalne okazje.");
            }
        }
        else if (Input.GetKeyDown(KeyCode.I)) { myState = States.hall_0; }
    }

    void garage_0()
    {
        if (!equipment.Matches_IsEnabled())
        {
            text.text = "W garażu jeszcze ciemniej niż w poprzednim pomieszczeniu. Nie za bardzo wiesz co tutaj robić.\n";
        }
        else
        {
            text.text = "Próbujesz zapalić zapałkę. Udaje Ci się dopiero za trzecim razem. W zasięgu ognia jesteś w stanie zobaczyć swoje auto, " +
                        "a obok niego rower i inne graty, które trzymasz w tym miejscu. Rzadko tu bywasz. Zastanawiasz się, co znajdziesz dalej, " +
                        "więc idziesz powoli wzdłuż ściany. Panuje tu grobowa cisza. Słyszysz jedyne kroki swoich stóp." +
                        "\n-> Wsiądź do [A]uta." +
                        "\n-> Przejdź przez [D]rzwi po prawej stronie.";
        }

        text.text += "\n-> [W]róć do korytarza.";
        
        if      (Input.GetKeyDown(KeyCode.W)) { myState = States.hall_0; }
        else if (Input.GetKeyDown(KeyCode.A)) { dialog.Open("Naciskasz klamkę, ale drzwi są zamknięte."); }
        else if (Input.GetKeyDown(KeyCode.D)) { myState = States.chamber_0; }
    }

    void chamber_0()
    {
        text.text = "Wchodzisz do nieznanego pomieszczenia. Któryś raz odpalasz zapałkę. Powoli rozglądasz się po " +
                    "pokoju. W jej słabym świetle dostrzegasz biurko, a na nim skrzynkę na narzędzia oraz porozrzucane " +
                    "różne papiery. To warsztat. Idziesz dalej. Zaraz obok biurka znajdują się drzwi.";

        if (equipment.doorCodeLocation.Equals("Chamber_Table") && !equipment.DoorCode_IsEnabled())
        {
            text.text += "\n-> Na biurku dostrzegasz [K]arteczkę.";
        }

        if (!chamberUnlocked)
        {
            text.text += "\n-> [O]dblokuj drzwi.";
        }

        if (chamberUnlocked)
        {
            text.text += "\n-> [W]róc na korytarz";
        }

        if      (Input.GetKeyDown(KeyCode.K) && equipment.doorCodeLocation.Equals("Chamber_Table"))
        {
            equipment.DoorCode_Enable();
            dialog.Open("Sięgasz po karteczkę. Okazuje się, że jest to kartka z kodem do drzwi wyjściowych. Zabierasz ją.");
        }
        else if (Input.GetKeyDown(KeyCode.O)) { chamberUnlocked = true; }
        else if (Input.GetKeyDown(KeyCode.W)) { myState = States.hall_0; }
    }

    #endregion
}
