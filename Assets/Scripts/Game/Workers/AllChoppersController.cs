using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllChoppersController : MonoBehaviour
{
    [SerializeField] int itemResourceId;
    [SerializeField] Text generalResources;
    [SerializeField] TextMeshProUGUI counterResources;
    private int allgrindresources;

    public int Allgrindresources
    {
        get { return allgrindresources; }
        set
        {
            allgrindresources += value;
            counterResources.text = allgrindresources.ToString() + " per min";
        }
    }
    void Start()
    {
        StartCoroutine(UpdateCountResources());
    }

    private IEnumerator UpdateCountResources()
    {
        yield return new WaitForSeconds(1f);
        SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE PlayersItems SET itemCount = {Convert.ToInt32(generalResources.text) + allgrindresources} WHERE playerId = 1 AND itemId = {itemResourceId}");
        //generalResources.text = (Convert.ToInt32(generalResources.text) + allgrindresources).ToString();        
        StartCoroutine(UpdateCountResources());
    }
}
