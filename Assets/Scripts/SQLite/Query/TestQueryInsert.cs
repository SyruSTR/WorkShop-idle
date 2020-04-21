using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQueryInsert : MonoBehaviour
{
    public void TestInsert()
    {
        SQLiteBD.ExecuteQueryWithoutAnswer("Insert Into Items (itemCount,nameItem,ItemRarityID,ItemType)" +
            "VALUES (20,'Medium Rock',1,3)");
        Debug.Log("Success");
        
    }
}
