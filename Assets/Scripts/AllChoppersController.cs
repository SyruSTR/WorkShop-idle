using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllChoppersController : MonoBehaviour
{
    [SerializeField] int itemResourceId;
    [SerializeField] TextMeshProUGUI counterResources;
    private int allgrindresources;

    public int Allgrindresources
    {
        get { return allgrindresources; }
        set
        {
            allgrindresources += value;
            counterResources.text = allgrindresources.ToString() + " per sec";
        }
    }
    void Start()
    {
        StartCoroutine(UpdateCountResources());
    }

    private IEnumerator UpdateCountResources()
    {
        yield return new WaitForSeconds(1f);
        if (allgrindresources > 0)
            SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE PlayersItems SET itemCount = (itemCount+{allgrindresources}) WHERE playerId = {GameController.PlayerID} AND itemId = {itemResourceId}");
        //generalResources.text = (Convert.ToInt32(generalResources.text) + allgrindresources).ToString();        
        StartCoroutine(UpdateCountResources());
    }
}
