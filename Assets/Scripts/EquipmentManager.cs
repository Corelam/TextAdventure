using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {

    [SerializeField] private TextController textController;
    [SerializeField] private GameTimeController gameTime;

    private bool doorKey;
    private bool closetKey;
    private bool pistol;
    private bool knife;

    void Start()
    {
        phoneImage.enabled = false;
        phoneClock.enabled = false;
        phoneImage_full.SetActive(false);
    }

    #region Phone
    private bool phone;
    private Coroutine phoneCoroutine;
    [SerializeField] private Image phoneImage;
    [SerializeField] private GameObject phoneImage_full;
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
    public void Phone_Open()
    {
        phoneImage_full.SetActive(true);
        phoneCoroutine = StartCoroutine(Phone_Clock());
    }

    public void Phone_Close()
    {
        StopCoroutine(phoneCoroutine);
        phoneClock.enabled = false;
        phoneImage_full.SetActive(false);
    }

    private IEnumerator Phone_Clock()
    {
        phoneClock.enabled = true;

        for (;;)
        {
            phoneClock.text = gameTime.hours + ":" + gameTime.minutes;
            yield return new WaitForSeconds(0.75F);
            phoneClock.text = gameTime.hours + " " + gameTime.minutes;
            yield return new WaitForSeconds(0.75F);
        }
    }
    #endregion

    #region Clothes
    private bool shoes;
    private bool szlafrok;
    [HideInInspector] public bool szlafrok_thrown;

    #region Shoes
    public bool Shoes_IsEnabled()
    {
        return shoes;
    }

    public void Shoes_Enable(bool boolean)
    {
        shoes = boolean;
    }
    #endregion

    #region Szlafrok
    public bool Szlafrok_IsEnabled()
    {
        return szlafrok;
    }


    /// <summary> Enable or disable Szlafrok item. </summary>
    public void Szlafrok_Enable(bool boolean)
    {
        szlafrok = boolean;
    }
    #endregion

    #endregion

    public bool DoorKey_IsEnabled()
    {
        return doorKey;
    }

    public bool Knife_IsEnabled()
    {
        return knife;
    }

    public void Knife_Enable()
    {
        knife = true;
    }
}
