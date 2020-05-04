using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerMono : MonoBehaviour
{
    private void Awake()
    {
        GameController.PlayerID = int.Parse(SQLiteBD.ExecuteQueryWithAnswer("SELECT id FROM Players WHERE selectedPlayer = 1"));
    }
}
