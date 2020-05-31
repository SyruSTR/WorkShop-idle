using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AddMarketItems : MonoBehaviour
{

    [SerializeField] Vector2 startPos;
    [SerializeField] float _xOffset;
    [SerializeField] float _yOffset;

    MarketItem item;
    private void Start()
    {
        
    }

    public void AddItems()
    {
        new Thread(new ThreadStart(async () =>
        {
            if (!TestAPI.IsHasToken)
               item = await TestAPI.GetMarketItems();
        })).Start();
    }
}
public class MarketItem
{
    public int MarketID { get; set; }
    public int ItemID { get; set; }
    public int Cost { get; set; }
}
