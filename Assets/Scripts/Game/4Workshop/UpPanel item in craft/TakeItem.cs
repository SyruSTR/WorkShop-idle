using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    private void OnMouseDown()
    {
        var craftItemInfo = GetComponent<CraftItemInfo>();
        var data = SQLiteBD.GetTable($"SELECT * FROM PlayersItems WHERE playerID = {GameController.PlayerID} AND itemID = {craftItemInfo.ItemID}");
        if(data.Rows.Count > 0)
        {
            SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE playersItems SET itemCount = (itemCount + 1) WHERE playerID = {GameController.PlayerID} AND itemID = {craftItemInfo.ItemID}");
        }
        else
        {
            SQLiteBD.ExecuteQueryWithoutAnswer($"INSERT INTO PlayersItems VALUES ({GameController.PlayerID},{craftItemInfo.ItemID},1)");
        }

        SQLiteBD.ExecuteQueryWithoutAnswer($"DELETE FROM CraftItems WHERE craftID = {craftItemInfo.CraftID}");
        GetComponentInParent<CreateItem>().ItemUpdate();
        Destroy(gameObject);
    }
}
