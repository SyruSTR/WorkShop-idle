﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPanel : MonoBehaviour
{
    [SerializeField] float lerpSpeed;
    [SerializeField] bool show;
    [SerializeField] float pointY;

    public void Show()
    {
        show = true;
    }
    public void Hide()
    {
        show = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (show)
            if (transform.position.y < pointY)
                ShowPanel();
    }


    private void ShowPanel()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, pointY, lerpSpeed * Time.deltaTime));
    }
}
