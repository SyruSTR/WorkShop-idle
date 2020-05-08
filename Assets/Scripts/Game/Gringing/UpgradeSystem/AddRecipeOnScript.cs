using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRecipeOnScript : MonoBehaviour
{
    [SerializeField] private UpgradeRecipes currentRecipe;
    [SerializeField] private RecipeController recipreController;
    public RecipeController RecipreController { get { return recipreController; } }
    public UpgradeRecipes CurrentRecipe { get { return currentRecipe; } set { currentRecipe = value; } }
    public void GetRecipe()
    {
        if (name == "Chopper")
            currentRecipe = recipreController.LastChopperUpgrade;
        else if (name == "Miner")
            currentRecipe = recipreController.LastMinerUpgrade;
    }
    private void OnEnable()
    {
        if (name == "Chopper")
            currentRecipe = recipreController.LastChopperUpgrade;
        else if (name == "Miner")
            currentRecipe = recipreController.LastMinerUpgrade;
    }

    private void OnDisable()
    {
        currentRecipe = null;
    }
}
