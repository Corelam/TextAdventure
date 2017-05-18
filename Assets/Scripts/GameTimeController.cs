using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour {

    public int hours = 2;
    public int minutes = 37;
    private float seconds = 0.0f;
    
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Clock();
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
}
