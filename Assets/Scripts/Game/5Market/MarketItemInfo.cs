using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarketItemInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI costText;

    private int marketID;
    private int itemID;
    private int cost;


    public void SetInfo(int marketID,int itemID,int cost)
    {
        this.marketID = marketID;
        this.itemID = itemID;
        this.cost = cost;
        costText.text = cost.ToString();
    }
    private void UpdateCostText()
    {
        costText.text = cost.ToString();
    }
}
