using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogBoxController : MonoBehaviour {

    private Rect windowRect;
    private bool show;              // Only show dialog if true.
    private int dialogButtons;      // number of buttons in a dialog
    private string dialogText;      // text to show in a dialog

    void OnGUI()
    {
        if (show)
        {
            windowRect = new Rect((Screen.width - 300) / 2, (Screen.height - (75 + 25 * dialogButtons)) / 2, 300, (50 + 25 * dialogButtons));   // 300x(50 + 25 * number of buttons requested) px window will apear in the center of the screen.
            windowRect = GUI.Window(0, windowRect, WindowDialog, "~~~ Komunikat ~~~");
        }
    }
    

    /// <summary> This is the actual window. </summary>
    void WindowDialog(int windowID)
    {
        GUI.Label(new Rect(5, 25, windowRect.width, 20), dialogText);

        for (int i = 0; i < dialogButtons; i++)
        {
            float newY = 25 * i;    // variable to put buttons lower if more than 1 button requested

            if (GUI.Button(new Rect(5, 50 + newY, windowRect.width - 10, 20), "Ok"))
            {
                show = false;
            }
        }
    }


    /// <summary> Method to open the dialog from outside of the script. </summary>
    /// <param name="text"> Text to show in a dialog. </param>
    /// <param name="buttonsAmount"> How many buttons do you need (default 1). </param>
    public void Open(string text, int buttonsAmount=1)
    {
        dialogText = text;
        dialogButtons = buttonsAmount;

        show = true;
    }
}