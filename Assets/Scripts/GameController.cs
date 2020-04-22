//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GameController : MonoBehaviour
//{
//    public Text moneyText;
//    private float _moneyCounter;
//    public Text woodText;
//   [SerializeField]  private float _woodCounter;
//    public Text rockText;
//    private float _rockCounter;

//    private void Awake()
//    {
//        Messenger<float>.AddListener(EconomicSystem.MoneyChange, MoneyChange);
//        Messenger<float>.AddListener(EconomicSystem.WoodChange, WoodChange);
//        Messenger<float>.AddListener(EconomicSystem.RockChange, RockChange);
//    }

//    private void MoneyChange(float addNumber)
//    {
//        _moneyCounter += AddNumber(addNumber, _moneyCounter);
//        moneyText.text = _moneyCounter.ToString();
//    }
//    private void WoodChange(float addNumber)
//    {
//        _woodCounter += AddNumber(addNumber, _woodCounter);
//        woodText.text = _woodCounter.ToString();
//    }
//    private void RockChange(float addNumber)
//    {
//        _rockCounter += AddNumber(addNumber, _rockCounter);
//        rockText.text = _rockCounter.ToString();
//    }
//    private float AddNumber(float number, float resourceCounter)
//    {
//        if (number > 0)
//            return number;
//        else if (number*-1 < resourceCounter)
//            return number;
//        return 0.0f;
//    }
//    private void OnApplicationQuit()
//    {
//        Messenger<float>.RemoveListener(EconomicSystem.MoneyChange, MoneyChange);
//        Messenger<float>.RemoveListener(EconomicSystem.WoodChange, WoodChange);
//        Messenger<float>.RemoveListener(EconomicSystem.RockChange, RockChange);
//    }
//}


