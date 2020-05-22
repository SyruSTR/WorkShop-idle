using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

static public class GameController
{
    static public int PlayerID;
    static public int activeSceen;
    static public int activeOrderID;

    public static Order GetNewOrderWromOrder(Order order)
    {
        var data = GetOrderTable(order);

        Order DBOrder = new Order();
        DBOrder.items = new List<Item>();

        for (int i = 0; i < data.Rows.Count; i++)
        {
            DBOrder.items.Add(new Item
            {
                itemId = int.Parse(data.Rows[i][0].ToString()),
                count = int.Parse(data.Rows[i][1].ToString())
            });
        }

        return DBOrder;
    }
    public static System.Data.DataTable GetOrderTable(Order order)
    {
        StringBuilder itemsIDStr = new StringBuilder();
        for (int i = 0; i < order.items.Count; i++)
        {
            if (itemsIDStr.Length > 0)
                itemsIDStr.Append(',');
            itemsIDStr.Append(order.items[i].itemId.ToString());
        }
        Debug.Log(itemsIDStr);
        return SQLiteBD.GetTable($"SELECT PlayersItems.itemID,itemCount,Items.itemCost FROM PlayersItems " +
            $"INNER JOIN Items ON Items.ItemID = PlayersItems.ItemID " +
            $"WHERE playerID = {PlayerID} AND Playersitems.itemID IN ({itemsIDStr})");
    }
}


