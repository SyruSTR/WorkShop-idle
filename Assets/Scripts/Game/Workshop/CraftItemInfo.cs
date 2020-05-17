using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemInfo : MonoBehaviour
{
    private int itemID;
    private int craftID;

    public int ItemID { get { return itemID; } set { itemID = value; } }
    public int CraftID { get { return craftID; } set { craftID = value; } }
}
