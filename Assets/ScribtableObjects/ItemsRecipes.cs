using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Recipe", menuName = "Item Recipe", order = 52)]
public class ItemsRecipes : UpgradeRecipes
{
    [SerializeField] private int accessLevel;
    [SerializeField] private int itemID;

    public int AccessLevel { get { return accessLevel; } }
    public int ItemID { get { return itemID; } }
}
