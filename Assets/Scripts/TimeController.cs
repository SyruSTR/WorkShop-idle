using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeController
{
    public static void SetDateTimeToDBForUnit(DateTime value)
    {
        string saveDateTime = value.ToString("u", CultureInfo.InvariantCulture);
        
    }
}
