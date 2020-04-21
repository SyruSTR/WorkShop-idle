using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDelete : MonoBehaviour
{
    public void TestDeleteQuery()
    {
        SQLiteBD.ExecuteQueryWithoutAnswer("Delete from items where itemid = 3");
        Debug.Log("Success");

    }
}
