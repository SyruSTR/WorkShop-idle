using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testbut1 : MonoBehaviour
{
    // Start is called before the first frame update
    public void test1()
    {
        Messenger<float>.Broadcast(EconomicSystem.MoneyChange, 1f);
    }
}
