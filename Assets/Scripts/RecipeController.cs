using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RecipeController : MonoBehaviour
{
    [SerializeField] private UpgradeRecipes[] ChoppersRecipes;
    [SerializeField] private UpgradeRecipes[] MinersRecipes;


    private void Start()
    {
        UpdateLVL();
        
    }
    public void UpdateLVL()
    {
        int sawmillLVL = int.Parse(SQLiteBD.ExecuteQueryWithAnswer($"SELECT sawmillLVL FROM Players WHERE id = {GameController.PlayerID}"));
        int mineLVL = int.Parse(SQLiteBD.ExecuteQueryWithAnswer($"SELECT mineLVL FROM Players WHERE id = {GameController.PlayerID}"));

        if (ChoppersRecipes.Length < sawmillLVL)
            ChoppersRecipes = new UpgradeRecipes[0];
        else if (sawmillLVL > 1)
        {
            ChoppersRecipes = RemoveItems(ChoppersRecipes, ChoppersRecipes.Length - (sawmillLVL - 1), sawmillLVL - 1);
        }
        if (MinersRecipes.Length < mineLVL)
            MinersRecipes = new UpgradeRecipes[0];
        else if (mineLVL > 1)
        {
            MinersRecipes = RemoveItems(MinersRecipes, MinersRecipes.Length - (mineLVL - 1), mineLVL - 1);
        }
    }
    public UpgradeRecipes LastChopperUpgrade
    {
        get
        {
            if (ChoppersRecipes.Length > 0)
                return ChoppersRecipes[ChoppersRecipes.Length - 1];
            else return null;
        }
    }
    public UpgradeRecipes LastMinerUpgrade
    {
        get
        {
            if (MinersRecipes.Length > 0)
                return MinersRecipes[MinersRecipes.Length - 1];
            else
                return null;
        }
    }

    public void DeleteLastRecipe(bool isChopper)
    {
        if (isChopper)
            ChoppersRecipes = RemoveLastItem(ChoppersRecipes);
        else
            MinersRecipes = RemoveLastItem(MinersRecipes);
    }

    [ContextMenu("Reverse Choppers")]
    void ReverseChoppers()
    {
        Debug.Log("Reverse Choppers Array");
        ReverseArray(ChoppersRecipes);
    }

    [ContextMenu("Reverse Miners")]
    void ReverseMiners()
    {
        Debug.Log("Reverse Choppers Array");
        ReverseArray(MinersRecipes);
    }
    private void ReverseArray(UpgradeRecipes[] array)
    {
        Array.Reverse(array);

    }
    private UpgradeRecipes[] RemoveLastItem(UpgradeRecipes[] array)
    {
        List<UpgradeRecipes> ggwp = array.ToList();
        ggwp.RemoveAt(ggwp.Count - 1);
        return ggwp.ToArray();
    }
    private UpgradeRecipes[] RemoveItems(UpgradeRecipes[] array, int index, int count)
    {
        if (
            index >= 0 &&
            array.Length > 0 &&
            count > 0 &&
            count < array.Length)
        {
            List<UpgradeRecipes> ggwp = array.ToList();
            ggwp.RemoveRange(index, count);
            return ggwp.ToArray();
        }
        Debug.LogError("ListError");
        return array;
    }
}
