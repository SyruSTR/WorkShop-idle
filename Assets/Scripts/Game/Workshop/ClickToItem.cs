using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToItem : MonoBehaviour
{
    public ItemsRecipes recipe;

    private void OnMouseDown()
    {
        StartCoroutine(CheckDrag());
    }

    private IEnumerator CheckDrag()
    {        
        var parentComponent = GetComponentInParent<AddItemsOnBoard>();
        yield return new WaitForSeconds(0.1f);
        if (parentComponent.DragActive)
            yield break;
        else
        {
            var reference = GetComponentInParent<ItemsCraftObjectReference>();
            reference.CraftPanel.SetRecipe(recipe);
            reference.AdditionalCanvas.SetActivePanel(3);
        }

    }
}
