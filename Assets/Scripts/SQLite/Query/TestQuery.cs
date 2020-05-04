using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class TestQuery : MonoBehaviour
{
    private void Start()
    {
        TestSelect();
    }
    public void TestSelect()
    {
        DataTable test = SQLiteBD.GetTable("SELECT itemId, nameItem, ItemsType.name FROM Items " +
            "INNER JOIN ItemsType ON Items.itemType = ItemsType.idItemsType;");

        //int idCountFirstItem = int.Parse(test.Rows[0][1].ToString());
        for (int i = 0; i < test.Rows.Count; i++)
        {
            string gg = "";
            for (int j = 0; j < test.Columns.Count; j++)
            {
                gg += test.Rows[i][j] + " /";
            }
            Debug.Log("Select:  "+ gg);
        }

        //Debug.Log($"Test Query = {idCountFirstItem}");
    }
    public void TestPlus()
    {
        SQLiteBD.ExecuteQueryWithoutAnswer("UPDATE Players SET sawmillLVL = (sawmillLVL+1) WHERE id = 1");
    }
}
