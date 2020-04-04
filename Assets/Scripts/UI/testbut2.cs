using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testbut2 : MonoBehaviour
{
    public void test2()
    {
        Messenger<float>.Broadcast(EconomicSystem.MoneyChange, -2f);
    }
}
