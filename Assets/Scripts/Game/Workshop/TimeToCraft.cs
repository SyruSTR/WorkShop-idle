using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToCraft : MonoBehaviour
{
    [SerializeField] private TimeSpan timeToCraft;

    private void Start()
    {
        //timeToCraft = DateTime.UtcNow - new DateTime(0,0,0,0,0,30);
        
    }
}
