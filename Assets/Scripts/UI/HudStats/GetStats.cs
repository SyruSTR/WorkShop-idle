using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetStats : MonoBehaviour
{
    [SerializeField] int itemId;
    private Text statsText;
    // Start is called before the first frame update
    private void Awake()
    {
        statsText = GetComponent<Text>();
    }
    void Start()
    {
        StartCoroutine(GetStatsInBD());
    }

    private IEnumerator GetStatsInBD()
    {
        //      new Thread(new ThreadStart(() =>
        //           {
        statsText.text = SQLiteBD.ExecuteQueryWithAnswer($"SELECT itemCount FROM PlayersItems WHERE itemId = {itemId} AND playerId = {GameController.PlayerID}");
        //           })).Start();
        //Debug.Log(statsText.text);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(GetStatsInBD());

    }
}
