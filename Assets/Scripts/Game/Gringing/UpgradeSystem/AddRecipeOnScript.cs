using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRecipeOnScript : MonoBehaviour
{
    [SerializeField] private UpgradeRecipes currentUpgradeRecipe;
    [SerializeField] private ItemsRecipes currentItemRecipe;
    [SerializeField] private RecipeController recipreController;
    
    public RecipeController RecipreController { get { return recipreController; } }
    public UpgradeRecipes CurrentUpgradeRecipe { get { return currentUpgradeRecipe; } set { currentUpgradeRecipe = value; } }
    public ItemsRecipes CurrentItemRecipe { get { return currentItemRecipe; } }
    
    public void GetRecipe()
    {
        if (recipreController != null)
        {
            if (name == "Chopper")
                currentUpgradeRecipe = recipreController.LastChopperUpgrade;
            else if (name == "Miner")
                currentUpgradeRecipe = recipreController.LastMinerUpgrade;
        }
    }
    public void SetRecipe(ItemsRecipes recipe)
    {
        currentItemRecipe = recipe;
    }
    private void OnEnable()
    {
        if (recipreController != null)
        {
            if (name == "Chopper")
                currentUpgradeRecipe = recipreController.LastChopperUpgrade;
            else if (name == "Miner")
                currentUpgradeRecipe = recipreController.LastMinerUpgrade;
        }
    }

    private void OnDisable()
    {
        currentUpgradeRecipe = null;
        currentItemRecipe = null;
    }
}
