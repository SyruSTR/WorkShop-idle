using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimeToCraft : MonoBehaviour
{
    [SerializeField] private TimeSpan timeToCraft;

    

    private TextMeshProUGUI timeText;

    private void Start()
    {
        //timeToCraft = DateTime.UtcNow - new DateTime(0,0,0,0,0,30);
        //System.TimeSpan ggwp = new System.TimeSpan(0, 0, -180);
    }
    private void Awake()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTime(TimeSpan time)
    {
        timeToCraft = time;
        timeText.text = timeToCraft.ToString(@"hh\:mm\:ss");

        StartCoroutine(CountDown());
    }
    private IEnumerator CountDown()
    {
        if (timeToCraft.TotalSeconds > 0)
        {
            yield return new WaitForSeconds(1);
            timeToCraft -= TimeSpan.FromSeconds(1);
            timeText.text = timeToCraft.ToString(@"hh\:mm\:ss");
            StartCoroutine(CountDown());
        }
        else
        {
            Destroy(timeText);
            var collider = gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(50, 50);
            gameObject.AddComponent<TakeItem>();
        }

    }
}
