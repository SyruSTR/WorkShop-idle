using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsSystemUpgrade : MonoBehaviour
{
    //Unit prefab
    [SerializeField] private GameObject unit;
    public void UpgradeUnit()
    {
        var lastUnitPosition = transform.GetChild(transform.childCount-1).transform.position;
        var newUnit = Instantiate(unit, lastUnitPosition,Quaternion.identity,transform);
        var rectUnit = newUnit.transform as RectTransform;
        rectUnit.localPosition = new Vector3(rectUnit.localPosition.x, rectUnit.localPosition.y - 100, rectUnit.localPosition.z);
        newUnit.name = $"{unit.name} {transform.childCount}";
    }
}
