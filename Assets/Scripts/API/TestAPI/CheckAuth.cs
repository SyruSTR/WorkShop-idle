using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAuth : MonoBehaviour
{
    [SerializeField] AddMarketItems marketItemsScript;
    [SerializeField] AddMarketItems playerMarketItemsScript;
    // Start is called before the first frame update
    public void AuthON()
    {
        if (TestAPI.IsHasToken)
        {
            if (marketItemsScript != null && playerMarketItemsScript != null)
            {
                marketItemsScript.AddItems(false);
                playerMarketItemsScript.AddItems(true);
                gameObject.SetActive(false);
            }
        }
    }
}
