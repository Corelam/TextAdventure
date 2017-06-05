using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool bleeding;
    private bool visible;

	// Use this for initialization
	void Start () {
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
}
