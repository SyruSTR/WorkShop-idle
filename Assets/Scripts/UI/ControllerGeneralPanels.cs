﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGeneralPanels : MonoBehaviour
{
    public enum Panels
    {
        None,
        Upgrade,
        Inventory
    }
    [SerializeField] private Panels _activePanel;
    [SerializeField] private MovementPanel cashActivePanel;
    private void Start()
    {
        _activePanel = Panels.None;
        cashActivePanel = null;
    }
    private void ShowActivePanel()
    {
        cashActivePanel.Show();
    }
    public void HideActivePanel()
    {
        if (cashActivePanel != null)
        {
            cashActivePanel.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 * -1);
            cashActivePanel.Hide();
            cashActivePanel = null;
            _activePanel = Panels.None;

            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void SetActivePanel(int panel)
    {
        if (panel != (int)Panels.None)
        {
            //show active panel
            _activePanel = (Panels)panel;
            transform.GetChild((int)_activePanel).gameObject.SetActive(true);
            cashActivePanel = transform.GetChild((int)_activePanel).GetComponent<MovementPanel>();
            ShowActivePanel();
            //show hide button
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
