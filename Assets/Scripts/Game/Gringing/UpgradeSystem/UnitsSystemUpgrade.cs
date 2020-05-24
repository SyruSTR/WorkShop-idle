using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UnitsSystemUpgrade : MonoBehaviour
{
    //Unit prefab
    [SerializeField] private GameObject unit;
    [SerializeField] private bool isChopper;
    private int maxUnitID;
    
    
    private void Start()
    {
        string unitType = "";
        if (isChopper)
            unitType = "1";
        else
            unitType = "2";
        DataTable data =
            SQLiteBD.GetTable($"SELECT TimeToEnd,speed,effectivity,unitID FROM Units WHERE playerId = {GameController.PlayerID} AND unitType = {unitType} ORDER BY unitID");
        maxUnitID = int.Parse(SQLiteBD.ExecuteQueryWithAnswer($"SELECT MAX(unitID) FROM Units")) + 1;
        if (data.Rows.Count > 0)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                TimeSpan second;
                DateTime unitTime = DateTime.ParseExact(data.Rows[i][0].ToString(), "u", CultureInfo.InvariantCulture).AddHours(2);
                if (unitTime > DateTime.UtcNow)
                    second = unitTime - System.DateTime.UtcNow;
                else second = TimeSpan.Zero;
                AddUnitInDB(Mathf.RoundToInt(float.Parse(second.TotalSeconds.ToString())), //seconds
                    float.Parse(data.Rows[i][1].ToString()), // speed
                    int.Parse(data.Rows[i][2].ToString()), // effectivity
                    int.Parse(data.Rows[i][3].ToString())
                    );

            }
        }
    }
    private UnitGrindController newUnit;
    private void AddUnitOnBoard()
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
            lastUnitPosition = new Vector3(-6.75f, 1.7f, 10.0f);
        else
            lastUnitPosition = new Vector3(-4.5f, 1.7f, 10.0f);
        GameObject newUnit = Instantiate(unit, lastUnitPosition, Quaternion.identity, transform);
        RectTransform rectUnit = newUnit.transform as RectTransform;
        if (firstUnit)
            rectUnit.localPosition = new Vector3(rectUnit.localPosition.x, rectUnit.localPosition.y, rectUnit.localPosition.z);
        else
            rectUnit.localPosition = new Vector3(rectUnit.localPosition.x, rectUnit.localPosition.y - 100, rectUnit.localPosition.z);
        newUnit.name = $"{unit.name} {transform.childCount}";
        this.newUnit = newUnit.GetComponent<UnitGrindController>();
    }
    public void UpgradeUnit()
    {
        string unitType = "";
        if (isChopper)
            unitType = "1";
        else
            unitType = "2";
        SQLiteBD.ExecuteQueryWithoutAnswer($"INSERT INTO Units (UnitID,UnitType,playerId) VALUES ({maxUnitID},{unitType},{GameController.PlayerID})");
        AddUnitInDB(0, 1.0f, 4, maxUnitID);
        maxUnitID++;
    }
    private void AddUnitInDB(int seconds, float speed, int effectivity, int unitID)
    {
        AddUnitOnBoard();
        newUnit.Effectivity = effectivity;
        newUnit._AnimationSpeed = speed;
        newUnit.unitID = unitID;
        if (seconds > 0)
            newUnit.SpeedTranzition = seconds;
        else newUnit.SpeedTranzition = 0;
    }
}
