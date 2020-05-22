using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemInInventoryPanel : MonoBehaviour
{
    [SerializeField] GameObject inventoryItemPrefab;
    [SerializeField] Vector2 startPos;
    [SerializeField] float _xOffset;
    [SerializeField] float _yOffset;

    // Start is called before the first frame update
    
    private void OnEnable()
    {
        AddItems();
    }

    private void AddItems()
    {
        int x = 0;
        int y = 0;

        var data = SQLiteBD.GetTable($"SELECT itemId,itemCount FROM PlayersItems" +
            $" WHERE playerId = 1 AND itemId IN (SELECT itemID From Items WHERE itemType NOT IN (4))");
        for (int i = 0; i < data.Rows.Count; i++)
        {
            int childCount = transform.childCount;

            var newItem = Instantiate(inventoryItemPrefab,
                        new Vector3(startPos.x + _xOffset * x,
                        startPos.y + _yOffset * y,
                        transform.position.z),
                        Quaternion.identity,
                        transform);
            newItem.name = $"{inventoryItemPrefab.name} {transform.childCount}";
            StartCoroutine(GetComponent<LoadItem>().LoadIemIconFromWorkshop(
                int.Parse(data.Rows[i][0].ToString()),
                newItem));

            newItem.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = data.Rows[i][1].ToString();

            x++;
            if (x > 2)
            {
                x = 0;
                y++;
            }
        }
        //todo height content
    }
    private void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
