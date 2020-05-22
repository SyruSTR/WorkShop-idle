using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class CreateItem : MonoBehaviour
{
    private int itemID;
    private LoadItem loadItemScript;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Vector2 startPos;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    public UnityEvent InventoryUpdate;

    private void Awake()
    {
        loadItemScript = GetComponent<LoadItem>();
    }

    private void Start()
    {
        var data = SQLiteBD.GetTable($"SELECT itemID,TimeToCraft,craftID FROM CraftItems WHERE playerID = {GameController.PlayerID}");

        for (int i = 0; i < data.Rows.Count; i++)
        {
            TimeSpan craftTime = DateTime.Parse(data.Rows[i][1].ToString()) - DateTime.UtcNow.AddHours(8);
            if (craftTime.TotalSeconds <= 0) craftTime = new TimeSpan(0, 0, 0);
            CreateItemFunction(
                int.Parse(data.Rows[i][0].ToString()),
                int.Parse(data.Rows[i][2].ToString()),
                craftTime);
        }
    }

    public void ItemUpdate()
    {
        Debug.Log("ItemTake");
        InventoryUpdate.Invoke();
    }

    public void CreateItemFunction(int itemID, int craftID, TimeSpan timeToCraft)
    {
        this.itemID = itemID;
        int childCount = transform.childCount;
        GameObject newItem = null;
        if (GameController.activeSceen == 1)
        {
            startPos.x -= 5.6f;
        }
        if (childCount < 1)
        {
            newItem = Instantiate(itemPrefab, startPos, Quaternion.identity, transform);
        }
        else
            newItem = Instantiate(itemPrefab, new Vector2(transform.GetChild(childCount - 1).transform.position.x + _xOffset,
                startPos.y + _yOffset * childCount),
                Quaternion.identity, transform);
        newItem.SetActive(false);

        newItem.name = $"{itemPrefab.name} {childCount + 1}";
        StartCoroutine(loadItemScript.LoadIemIconFromWorkshop(this.itemID, newItem));
        newItem.SetActive(true);

        var craftInfo = newItem.GetComponent<CraftItemInfo>();

        craftInfo.CraftID = craftID;
        craftInfo.ItemID = itemID;

        var itemTime = newItem.GetComponent<TimeToCraft>();
        if (itemTime != null)
            itemTime.SetTime(timeToCraft);
        if (childCount >= 3)
        {
            RectTransform contentRect = GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(85 * (childCount - 2),
                //distance between 2 item * rows.count * yOffset
                contentRect.sizeDelta.y);
        }
    }
}
