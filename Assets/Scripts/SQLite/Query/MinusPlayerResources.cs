using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusPlayerResources : MonoBehaviour
{
    public void _MinusPlayerResources(bool isSawmill)
    {
        var recipe = GetComponent<AddRecipeOnScript>();
        if (recipe.CurrentUpgradeRecipe.RecipesItemsID.Length > 0)
        {
            for (int i = 0; i < recipe.CurrentUpgradeRecipe.RecipesItemsID.Length; i++)
            {
                int recipeResources = recipe.CurrentUpgradeRecipe.RecipesCountItems[i];
                int itemID = recipe.CurrentUpgradeRecipe.RecipesItemsID[i];
                SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE PlayersItems SET itemCount = (itemCount-{recipeResources}) WHERE playerID = {GameController.PlayerID} AND itemId = {itemID}");
            }
            string buildType = "";
            if (isSawmill)
                buildType = "sawmillLVL";
            else
                buildType = "mineLVL";
            SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE Players SET {buildType} = ({buildType}+1) WHERE id = {GameController.PlayerID}");
            //recipe.RecipreController.UpdateLVL();
        }
    }
}
