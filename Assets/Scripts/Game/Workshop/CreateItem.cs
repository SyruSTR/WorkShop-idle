using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    private int itemID;
    private LoadItem loadItemScript;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Vector2 startPos;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    private void Awake()
    {
        loadItemScript = GetComponent<LoadItem>();

    }
    private void Start()
    {
        CreateItemFunction(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CreateItemFunction(0);
        }
    }

    public void CreateItemFunction(int itemID)
    {
        this.itemID = itemID;
        int childCount = transform.childCount;
        Debug.Log(startPos.x + _xOffset * childCount);
        if (childCount < 1)
            Instantiate(itemPrefab, startPos, Quaternion.identity, transform);
        else
            Instantiate(itemPrefab, new Vector2(transform.GetChild(childCount-1).transform.position.x + _xOffset,
                startPos.y + _yOffset * childCount),
                Quaternion.identity, transform);
        if (childCount >= 3)
        {
            RectTransform contentRect = GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(85 * (childCount-2),
                //distance between 2 item * rows.count * yOffset
                contentRect.sizeDelta.y);
        }
    }
}
