using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Game
{
    private static GameData gameData;

    public static GameData GetGameData()
    {
        if(gameData == null)
        {
            gameData = new GameData();
        }

        return gameData;
    }
    
}
