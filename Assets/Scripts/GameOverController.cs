using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Static class responsible for holding Game Over Text when scene is changed to Win or Lose </summary>
public static class GameOverController {

    private static string gameOverText;

    public static string GameOverText
    {
        get
        {
            return gameOverText;
        }
        set
        {
            gameOverText = value;
        }
    }

}
