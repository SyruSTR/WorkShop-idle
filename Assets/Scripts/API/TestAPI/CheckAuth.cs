using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAuth : MonoBehaviour
{
    [SerializeField] AddMarketItems marketItemsScript;
    // Start is called before the first frame update
    public void AuthON()
    {
        if (TestAPI.IsHasToken)
        {
            if (marketItemsScript != null)
            {
                marketItemsScript.AddItems();
            }
            gameObject.SetActive(false);
        }
    }
}
