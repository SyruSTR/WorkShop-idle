using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadItem : MonoBehaviour
{
    public IEnumerator LoadIemIconFromWorkshop(int itemID, GameObject go)
    {
        if (itemID > 0 && go != null)
        {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture($"{Application.streamingAssetsPath + "/ItemsIcons"}" +
                $"/{SQLiteBD.ExecuteQueryWithAnswer($"SELECT pathToSprite FROM Items WHERE itemID = {itemID}")}"))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log("Error: " + webRequest.error);
                }
                else
                {
                    //Debug.Log("Received: " + webRequest.downloadHandler);
                    Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                    go.transform.GetChild(1).GetComponent<Image>().sprite =
                        Sprite.Create(
                        texture,
                        new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                }
            }
        }
        else
            Debug.LogError("Error: Load Item Sprite");
        yield break;
    }
}
