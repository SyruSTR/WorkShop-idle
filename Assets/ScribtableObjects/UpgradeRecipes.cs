using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe", order = 51)]
public class UpgradeRecipes : ScriptableObject
{
    [SerializeField] private Item[] Recipe;
    public int[] RecipesItemsID
    {
        get
        {
            int[] returnItemID = new int[Recipe.Length];
            for (int i = 0; i < Recipe.Length; i++)
            {
                returnItemID[i] = Recipe[i].itemId;
            }
            return returnItemID;
        }
    }
    public int[] RecipesCountItems
    {
        get
        {
            int[] returnCountItems = new int[Recipe.Length];
            for (int i = 0; i < Recipe.Length; i++)
            {
                returnCountItems[i] = Recipe[i].count;
            }
            return returnCountItems;
        }
    }
}

[Serializable]
public class Item
{
    [SerializeField] public int itemId;
    [SerializeField] public int count;
}
