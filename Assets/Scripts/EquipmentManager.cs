﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {

    [SerializeField] private TextController textController;
    [SerializeField] private GameTimeController gameTime;

    private bool doorKey;
    private bool closetKey;
    private bool pistol;
    private bool butterKnife;

    void Start()
    {
        phoneImage.enabled = false;
        phoneClock.enabled = false;
    }

    #region Phone
    private bool phone;
    [SerializeField] private Image phoneImage;
    [SerializeField] private Text phoneClock;

    /// <summary> Checks if player has Phone in the game. </summary>
    /// <returns> Returns true if Phone is enabled. </returns>
    public bool Phone_IsEnabled()
    {
        return phone;
    }

    /// <summary> Enables Phone item to the player. </summary>
    public void Phone_Enable()
    {
        phoneImage.enabled = true;
        phoneClock.text = "";
        phoneClock.enabled = true;
        phone = true;
    }

    /// <summary> Disables Phone item from the player. </summary>
    public void Phone_Disable()         // method not used yet
    {
        phoneImage.enabled = false;
        phoneClock.text = "";
        phoneClock.enabled = false;
        phone = false;
    }
    
    /// <summary> Shows ingame time when player has the Phone. </summary>
    public void Phone_CheckTime()
    {
        StartCoroutine(Phone_Clock());
    }

    private IEnumerator Phone_Clock()
    {
        phoneClock.enabled = true;

        for (int i = 0; i < 3; i++)
        {
            phoneClock.text = gameTime.hours + ":" + gameTime.minutes;
            yield return new WaitForSeconds(0.75F);
            phoneClock.text = gameTime.hours + " " + gameTime.minutes;
            yield return new WaitForSeconds(0.75F);
        }
        phoneClock.enabled = false;
    }
    #endregion

    #region Clothes
    private bool shoes;
    private bool szlafrok;

    #region Shoes
    public bool Shoes_IsEnabled()
    {
        return shoes;
    }

    public void Shoes_Enable()
    {
        shoes = true;
    }
    #endregion

    #region Szlafrok
    public bool Szlafrok_IsEnabled()
    {
        return szlafrok;
    }

    public void Szlafrok_Enable()
    {
        szlafrok = true;
    }
    #endregion

    #endregion

}