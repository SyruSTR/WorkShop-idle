using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsSystemUpgrade : MonoBehaviour
{
    //Unit prefab
    [SerializeField] private GameObject unit;
    [SerializeField] private bool isChopper;
    private void Start()
    {
        string buildType = "";
        if (isChopper)
            buildType = "sawmillLVL";
        else
            buildType = "mineLVL";
        int countUpdate =int.Parse(
            SQLiteBD.ExecuteQueryWithAnswer($"SELECT {buildType} FROM Players WHERE id = {GameController.PlayerID}"))
            -1;
        if (countUpdate > 0)
        {
            for (int i = 0; i < countUpdate; i++)
            {
                UpgradeUnit();
            }
        }
    }
    public void UpgradeUnit()
    {
        Vector3 lastUnitPosition = transform.GetChild(transform.childCount-1).transform.position;
        GameObject newUnit = Instantiate(unit, lastUnitPosition,Quaternion.identity,transform);
        RectTransform rectUnit = newUnit.transform as RectTransform;
        rectUnit.localPosition = new Vector3(rectUnit.localPosition.x, rectUnit.localPosition.y - 100, rectUnit.localPosition.z);
        newUnit.name = $"{unit.name} {transform.childCount}";
    }
}
