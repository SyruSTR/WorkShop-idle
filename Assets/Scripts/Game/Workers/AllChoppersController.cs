using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllChoppersController : MonoBehaviour
{
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
        generalResources.text = (Convert.ToInt32(generalResources.text) + allgrindresources).ToString();        
        StartCoroutine(UpdateCountResources());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
