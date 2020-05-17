using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AddNewItemInDB : MonoBehaviour
{
    [SerializeField] CreateItem ItemScript;
    private int maxCratID;
    private ItemsRecipes recipe;

    private void Start()
    {

        try
        {
            maxCratID = int.Parse(SQLiteBD.ExecuteQueryWithAnswer("SELECT MAX(craftID) FROM CraftItems"));

        }
        catch (FormatException)
        {
            Debug.LogError("FormatE");
            maxCratID = 0;
        }
        catch (ArgumentNullException)
        {
            Debug.LogError("ArgNULL");
            maxCratID = 0;
        }
    }
    private void AddItem(int itemID, DateTime timeToCraft)
    {
        if (ItemScript != null)
        {
            maxCratID++;
            SQLiteBD.ExecuteQueryWithoutAnswer($"INSERT INTO CraftItems (CraftID,ItemId,playerID,TimeToCraft) VALUES ({maxCratID},{itemID},{GameController.PlayerID},'{timeToCraft.ToString("u",CultureInfo.InvariantCulture)}')");
            ItemScript.CreateItemFunction(itemID,maxCratID, new TimeSpan(0, 0, recipe.SecondToCraft));
        }
    }

    public void AddItemButton()
    {
        recipe = GetComponent<AddRecipeOnScript>().CurrentItemRecipe;

        AddItem(recipe.ItemID, DateTime.UtcNow.Add(new TimeSpan(0, 0, recipe.SecondToCraft)));
    }
}
