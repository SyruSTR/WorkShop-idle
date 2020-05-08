using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UnitsSystemUpgrade : MonoBehaviour
{
    //Unit prefab
    [SerializeField] private GameObject unit;
    [SerializeField] private bool isChopper;
    private void Start()
    {
        string unitType = "";
        if (isChopper)
            unitType = "1";
        else
            unitType = "2";
        DataTable data =
            SQLiteBD.GetTable($"SELECT TimeToEnd,speed,effectivity FROM Units WHERE playerId = {GameController.PlayerID} AND unitType = {unitType} ORDER BY unitID");
        if (data.Rows.Count > 0)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                TimeSpan second;
                if (System.DateTime.Parse(data.Rows[i][0].ToString()) > System.DateTime.UtcNow)
                    second = System.DateTime.Parse(data.Rows[i][0].ToString()) - System.DateTime.UtcNow;
                else second = new TimeSpan(0, 0, 0);
                AddUnitInDB(second.Seconds, //seconds
                    float.Parse(data.Rows[i][1].ToString()), // speed
                    int.Parse(data.Rows[i][2].ToString()) // effectivity
                    );

            }
        }
    }
    public GameObject UpgradeUnit()
    {
        int childCount = transform.childCount;
        bool firstUnit = true;
        Vector3 lastUnitPosition = Vector3.zero;
        if (childCount - 1 >= 0)
        {
            lastUnitPosition = transform.GetChild(transform.childCount - 1).transform.position;
            firstUnit = false;
        }
        else if (isChopper)
            lastUnitPosition = new Vector3(-12.5f, 1.7f, 10.0f);
        else
            lastUnitPosition = new Vector3(-10.0f, 1.7f, 10.0f);
        GameObject newUnit = Instantiate(unit, lastUnitPosition, Quaternion.identity, transform);
        RectTransform rectUnit = newUnit.transform as RectTransform;
        if (firstUnit)
            rectUnit.localPosition = new Vector3(rectUnit.localPosition.x, rectUnit.localPosition.y, rectUnit.localPosition.z);
        else
            rectUnit.localPosition = new Vector3(rectUnit.localPosition.x, rectUnit.localPosition.y - 100, rectUnit.localPosition.z);
        newUnit.name = $"{unit.name} {transform.childCount}";
        return newUnit;
    }
    private void AddUnitInDB(int seconds, float speed, int effectivity)
    {
        var newUnit = UpgradeUnit().GetComponent<UnitGrindController>();
        newUnit.Effectivity = effectivity;
        newUnit._AnimationSpeed = speed;
        if (seconds > 0)
            newUnit.SpeedTranzition = seconds;
        else newUnit.SpeedTranzition = 0;
    }
}
