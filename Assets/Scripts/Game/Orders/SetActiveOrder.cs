using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetActiveOrder : MonoBehaviour
{

    [SerializeField] Sprite OkSprite;
    [SerializeField] Sprite NotOkSprite;
    private bool notActivate;

    public int orderID;
    public Order order;
    public bool activeOrder;

    //public bool ActiveOrder { set { activeOrder = value; } } 

    private void OnMouseDown()
    {
        if (order != null && !activeOrder && !notActivate)
        {
            SetOrder();
        }
    }

    private void FirstUpdateOrder()
    {
        if (name[name.Length - 1] == '1' && !notActivate)
        {
            Debug.Log(name);

            SetOrder();
        }
        GetComponentInChildren<TextMeshProUGUI>().text = order.orderCost.ToString();
        UpdateInfo();
    }

    private void SetOrder()
    {
        var controller = transform.parent.GetComponent<OrdersController>();
        controller.DisableAllOrder();
        activeOrder = true;
        GameController.activeOrderID = orderID;
        GetComponentInChildren<TextMeshProUGUI>().text = order.orderCost.ToString();
        controller.OrderPanel.GetComponent<AddOrderOnPanel>().LoadItems(order);
    }
    public void SetTimeOrder(TimeSpan time)
    {
        FirstUpdateOrder();
        if (time.TotalSeconds > 0)
        {
            notActivate = true;
            ActivateTime();
            StartCoroutine(EnumratorTime(time));
        }

    }
    private IEnumerator EnumratorTime(TimeSpan time)
    {
        if (time.TotalSeconds > 0)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = time.ToString(@"hh\:mm\:ss");
            yield return new WaitForSeconds(1);
            StartCoroutine(EnumratorTime(time - new TimeSpan(0, 0, 1)));
        }
        else
        {
            notActivate = false;
            ActivateTime();
        }
    }
    private void ActivateTime()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            child.SetActive(!child.activeSelf);
        }
    }
    public void UpdateInfo()
    {
        Order DBOrder = GameController.GetNewOrderWromOrder(order);
        int trueCount = 0;
        for (int i = 0; i < DBOrder.items.Count; i++)
        {
            for (int j = 0; j < order.items.Count; j++)
            {
                if (order.items[j].itemId == DBOrder.items[i].itemId)
                {
                    Debug.Log(order.items[j].count + " / " + DBOrder.items[i].count);
                    if (order.items[j].count <= DBOrder.items[i].count)
                    {
                        trueCount++;
                        break;
                    }
                }
            }
        }

        if (trueCount >= order.items.Count)
        {
            Debug.Log(transform.name + " OrderComplete");
            transform.GetChild(3).GetComponent<Image>().sprite = OkSprite;
        }
        else
        {
            Debug.Log(transform.name + "Order Not Complete");
            transform.GetChild(3).GetComponent<Image>().sprite = NotOkSprite;
        }
    }
}
