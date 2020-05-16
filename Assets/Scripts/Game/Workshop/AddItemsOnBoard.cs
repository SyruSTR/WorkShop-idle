using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AddItemsOnBoard : MonoBehaviour
{
    [SerializeField] private ItemsRecipes[] itemsRecipes;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private bool dragActive;
    private ScrollRect scrollRect;

    [Space]
    [SerializeField] private Transform additionalCanvas;
    public bool DragActive { get { return dragActive; } set { dragActive = value; } }

    private float _deltaX = 1.3f;
    private float _deltaY = 1.3f;
    [SerializeField] private Vector2 startPos;
    // Start is called before the first frame update

    public void ItemSelected(ItemsRecipes recipe)
    {
        additionalCanvas.GetComponent<ControllerGeneralPanels>().SetActivePanel(3);
    }

    void Start()
    {
        _deltaX = 1.3f;
        _deltaY = -1.3f;
        int workshopLVL = int.Parse(SQLiteBD.ExecuteQueryWithAnswer($"SELECT workshopLVL FROM players WHERE selectedPlayer = 1"));

        int x = 0;
        int y = 0;
        for (int i = 0; i < itemsRecipes.Length; i++)
        {
            if (itemsRecipes[i].AccessLevel <= workshopLVL)
            {
                var newItem = Instantiate(itemPrefab,
                    new Vector3(startPos.x + _deltaX * x,
                    startPos.y + _deltaY * y,
                    transform.position.z),
                    Quaternion.identity,
                    transform);
                newItem.name = $"{itemPrefab.name} {transform.childCount}";
                StartCoroutine(GetComponent<LoadItem>().LoadIemIconFromWorkshop(
                    Application.streamingAssetsPath + "/ItemsIcons",
                    itemsRecipes[i].ItemID,
                    newItem));
                newItem.GetComponent<ClickToItem>().recipe = itemsRecipes[i];
                //newItem.GetComponent<LoadItemRecipeOnBoard>().Recipe = itemsRecipes[i];
                x++;
                if (x > 2)
                {
                    x = 0;
                    y++;
                }
            }
        }

        if (y > 3)
        {
            RectTransform contentRect = GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x,
                //distance between 2 item * rows.count * yOffset
                60 * y + 40);
            //RectTransform contentRect = GetComponent<RectTransform>();
            //contentRect.offsetMax += new Vector2(0, -50);
            //contentRect.offsetMin -= new Vector2(0, 50);
        }
    }

}
