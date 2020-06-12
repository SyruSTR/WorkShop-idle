using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityToolbag;
using UnityEngine.UI;

public class AddMarketItems : MonoBehaviour
{
    private LoadItem loadItem;
    [SerializeField] private GameObject marketItemPrefab = null;
    [SerializeField] private Vector2 startPos = Vector2.zero;
    [SerializeField] private float _xOffset = 0f;
    [SerializeField] private float _yOffset = 0f;
    [Space]
    [SerializeField] private TextMeshProUGUI errorText = null;
    [SerializeField] private GameObject authPanel;

    TestAPI.ItemsFromServer<MarketItem> itemsFormServer;
    private void Awake()
    {
        loadItem = GetComponent<LoadItem>();
    }


    public void TestAdd()
    {
        AddItemsOnBoard();
    }
    public void AddItems(bool PlayerItems)
    {
        if (TestAPI.IsHasToken)
            new Thread(new ThreadStart(async () =>
            {
                if (!PlayerItems)
                    itemsFormServer = await TestAPI.GetMarketItems();
                else
                    itemsFormServer = await TestAPI.GetPlayerItems();
                Dispatcher.Invoke(() =>
                {
                    AddItemsOnBoard();
                });
            })).Start();
        else
            Debug.LogError("User haven't token");
    }
    private void AddItemsOnBoard()
    {
        if (itemsFormServer.Items != null)
        {
            if (itemsFormServer.Items.Length > 0)
            {
                errorText.text = "";
                int x = 0;
                int y = 0;
                for (int i = 0; i < itemsFormServer.Items.Length; i++)
                {
                    var newItem = Instantiate(marketItemPrefab,
                        new Vector3(
                            transform.position.x + startPos.x + _xOffset * x,
                            transform.position.y + startPos.y + _yOffset * y),
                        Quaternion.identity, transform);
                    var item = itemsFormServer.Items[i];
                    StartCoroutine(loadItem.LoadIemIconFromWorkshop(item.ItemID, newItem));
                    newItem.GetComponent<MarketItemInfo>().SetInfo(item.MarketID, item.ItemID, item.Cost);
                    x++;
                    if (x >= 3)
                    {
                        x = 0;
                        y++;
                    }

                }
            }
            else errorText.text = "Market Is Empty(";
            return;
        }
        if (itemsFormServer.authCode == TestAPI.AuthCode.NoAuth)
        {
            errorText.text = "Auth is bad";
            if (authPanel != null)
                StartCoroutine(VisibleAuthPanel());
        }
        else
            errorText.text = "Market Load Error";
    }
    private IEnumerator VisibleAuthPanel()
    {
        yield return new WaitForSeconds(1f);
        authPanel.SetActive(true);
    }
}
public class MarketItem
{
    public int MarketID { get; set; }
    public int ItemID { get; set; }
    public int Cost { get; set; }
    public int ItemStatus { get; set; }
}
