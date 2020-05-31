using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrdersController : MonoBehaviour
{
    [SerializeField] private GameObject orderPanel;
    public Order cashOrder;
    private DataTable data;
    private int[] itemsIDArray;
    private List<int> itemsIDList = new List<int>();
    public GameObject OrderPanel { get { return orderPanel; } }

    public void OrdersUpdate()
    {
        var childUpdate = GetComponentsInChildren<SetActiveOrder>();
        foreach (var script in childUpdate)
        {
            script.UpdateInfo();
        }
        orderPanel.GetComponent<AddOrderOnPanel>().UpdateActiveOrder();
    }

    public void DeleteOrder()
    {
        SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE orders SET textOrder = NULL, timeToNewOrder = '{DateTime.UtcNow.AddMinutes(5).ToString("u")}' WHERE OrderID = {GameController.activeOrderID}");
        GenerateOrders();
    }
    public void DisableAllOrder()
    {
        var childrenScript = GetComponentsInChildren<SetActiveOrder>();
        foreach (var childScript in childrenScript)
        {
            childScript.activeOrder = false;
        }
    }

    private void Awake()
    {
        GenerateOrders();
    }
    private void GenerateOrders()
    {
        //взять текущие заказы из базы данных
        data = SQLiteBD.GetTable($"SELECT TextOrder,orderID,TimeTonewOrder FROM orders WHERE playerID = {GameController.PlayerID}");

        for (int i = 0; i < data.Rows.Count; i++)
        {
            cashOrder = new Order();
            cashOrder.items = new List<Item>();
            string JSON = data.Rows[i][0].ToString();
            if (JSON == "")
            {
                // кеширование текущих предметов
                if (itemsIDArray == null)
                {
                    var itemData = SQLiteBD.GetTable("SELECT itemID FROM Items WHERE Itemtype NOT IN (4)");
                    itemsIDArray = new int[itemData.Rows.Count];
                    for (int j = 0; j < itemData.Rows.Count; j++)
                    {
                        itemsIDArray[j] = int.Parse(itemData.Rows[j][0].ToString());
                    }
                }

                foreach (var item in itemsIDArray)
                {
                    itemsIDList.Add(item);
                }

                //поставить новому заказу случайную длину от 1 до 4
                int orderLeght = Random.Range(1, 5);

                for (int j = 0; j < orderLeght; j++)
                {
                    //выбор случайного предмета
                    int itemID = itemsIDList[Random.Range(0, itemsIDList.Count)];
                    //удаление предмета из кеширивания
                    itemsIDList.Remove(itemID);
                    cashOrder.items.Add(new Item
                    {
                        itemId = itemID,
                        count = Random.Range(1, 4)
                    });
                    Debug.Log($"ItemID: {cashOrder.items[j].itemId} count: {cashOrder.items[j].count}");
                }
                itemsIDList.Clear();
                cashOrder.orderCost = cashOrder.CalculatinTheOrderCost(cashOrder);

                SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE orders SET textOrder = '{JsonUtility.ToJson(cashOrder)}' WHERE orderID = {i + 1}");
            }
            else
            {                
                cashOrder = JsonUtility.FromJson<Order>(JSON);
            }
            var child = transform.GetChild(i).GetComponent<SetActiveOrder>();
            child.order = cashOrder;
            child.orderID = int.Parse(data.Rows[i][1].ToString());

            string timeToNewOrder = data.Rows[i][2].ToString();

            if (timeToNewOrder != "")
            {
                child.SetTimeOrder(DateTime.Parse(timeToNewOrder) - DateTime.UtcNow.AddHours(8));
            }
            else
                child.SetTimeOrder(TimeSpan.Zero);

        }
    }
}



[System.Serializable]
public class Order
{
    public List<Item> items;
    public int orderCost;

    public int CalculatinTheOrderCost(Order order)
    {
        var data = GameController.GetOrderTable(order);

        int cost = 0;
        for (int i = 0; i < data.Rows.Count; i++)
        {
            for (int j = 0; j < order.items.Count; j++)
            {
                if (order.items[j].itemId == int.Parse(data.Rows[i][0].ToString()))
                {
                    cost += order.items[j].count * int.Parse(data.Rows[i][2].ToString());
                    break;
                }
            }

        }

        int _10procent = Mathf.RoundToInt(cost / 10);
        return Random.Range(cost - _10procent, cost + _10procent);
    }
}
