using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusPlayerResources : MonoBehaviour
{
    private AddRecipeOnScript recipe;
    private Order order;
    public void _MinusPlayerResources(bool isSawmill)
    {
        recipe = GetComponent<AddRecipeOnScript>();
        if (recipe.CurrentUpgradeRecipe.RecipesItemsID.Length > 0)
        {
            MinusResources();

            string buildType = "";
            if (isSawmill)
                buildType = "sawmillLVL";
            else
                buildType = "mineLVL";
            SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE Players SET {buildType} = ({buildType}+1) WHERE id = {GameController.PlayerID}");
            //recipe.RecipreController.UpdateLVL();
        }
    }

    public void _MinusPlayerResources()
    {
        recipe = GetComponent<AddRecipeOnScript>();
        if (recipe.CurrentItemRecipe.RecipesItemsID.Length > 0)
        {
            MinusResources();
        }
    }
    public void _MinusPlayerResourcesWithOrder()
    {
        order = GetComponent<AddOrderOnPanel>().SelectedOrder;
        if (order.items.Count > 0)
        {
            MinusResourcesFromOrder();
        }
    }

    private void MinusResourcesFromOrder()
    {
        for (int i = 0; i < order.items.Count + 1; i++)
        {
            int itemId = 0;
            string recousrcesCount = "";
            if (i == order.items.Count)
            {
                itemId = 1;
                recousrcesCount = $"+{order.orderCost}";
            }
            else
            {
                itemId = order.items[i].itemId;
                recousrcesCount = $"-{order.items[i].count}";
            }

            SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE PlayersItems SET itemCount = (itemCount{recousrcesCount}) WHERE playerID = {GameController.PlayerID} AND itemId = {itemId}");
        }
    }
    private void MinusResources()
    {
        int count = 0;
        if (recipe.CurrentUpgradeRecipe != null)
            count = recipe.CurrentUpgradeRecipe.RecipesItemsID.Length;
        else
            count = recipe.CurrentItemRecipe.RecipesItemsID.Length;

        for (int i = 0; i < count; i++)
        {
            int recipeResources = 0;
            int itemID = 0;
            if (recipe.CurrentUpgradeRecipe != null)
            {
                recipeResources = recipe.CurrentUpgradeRecipe.RecipesCountItems[i];
                itemID = recipe.CurrentUpgradeRecipe.RecipesItemsID[i];
            }
            else
            {
                recipeResources = recipe.CurrentItemRecipe.RecipesCountItems[i];
                itemID = recipe.CurrentItemRecipe.RecipesItemsID[i];
            }
            SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE PlayersItems SET itemCount = (itemCount-{recipeResources}) WHERE playerID = {GameController.PlayerID} AND itemId = {itemID}");
        }
    }
}
