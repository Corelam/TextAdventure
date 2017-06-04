using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDialogBoxController : MonoBehaviour {

    private bool show;              // Only show dialog if true.

    public GameObject dialog;
    [SerializeField] private Text dialogText;

    // Use this for initialization
    void Start () {
        dialog.SetActive(false);
	}

    void Update()
    {
        if (show && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            Close();
        }
    }

    /// <summary> Method to open the dialog from outside of the script. </summary>
    public void Open(string text)
    {
        show = true;
        dialog.SetActive(true);
        dialogText.text = text;
    }

    /// <summary> Method to close the dialog. </summary>
    public void Close()
    {
        show = false;
        dialog.SetActive(false);
    }
}
