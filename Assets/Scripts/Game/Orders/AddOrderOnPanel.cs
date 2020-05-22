using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AddOrderOnPanel : MonoBehaviour
{

    private Order selectedOrder;
    public Order SelectedOrder { get { return selectedOrder; } }
    public void LoadItems(Order order)
    {
        selectedOrder = order;
        UpdateActiveOrder();

    }

    public void UpdateActiveOrder()
    {
        Order DBOrder = GameController.GetNewOrderWromOrder(selectedOrder);

        int fullItemCount = 0;
        for (int i = 0; i < selectedOrder.items.Count; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(true);
            StartCoroutine(GetComponent<LoadItem>().LoadIemIconFromWorkshop(
                selectedOrder.items[i].itemId,
                child));

            var TMP = child.GetComponentInChildren<TextMeshProUGUI>();
            int Pcount = 0;
            for (int j = 0; j < DBOrder.items.Count; j++)
            {
                if (selectedOrder.items[i].itemId == DBOrder.items[j].itemId)
                {
                    Pcount = DBOrder.items[j].count;
                }
            }
            if (selectedOrder.items[i].count <= Pcount)
            {
                TMP.color = Color.green;
                fullItemCount++;
            }
            else
                TMP.color = Color.red;
            TMP.text = $"{selectedOrder.items[i].count}/{Pcount}";
        }
        if (fullItemCount >= selectedOrder.items.Count)
            GetComponentInChildren<Button>().interactable = true;
        else
            GetComponentInChildren<Button>().interactable = false;
        for (int i = 5; i > selectedOrder.items.Count - 1; i--)
        {
            var child = transform.GetChild(i).gameObject;
            child.SetActive(false);
        }
    }
}
