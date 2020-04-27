using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AddItemsOnBoardForUpdate : MonoBehaviour
{
    [SerializeField] private UpgradeRecipes currentRecipe;
    private void OnEnable()
    {
        if (currentRecipe != null)
        {
            for (int i = 0; i < currentRecipe.RecipesItemsID.Length; i++)
            {
                var child = transform.GetChild(i);
                var currentID = currentRecipe.RecipesItemsID[i];
                //set sprite
#if UNITY_EDITOR
                DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath + "/ItemsIcons");
                print(directoryInfo);
                FileInfo[] allFiles = directoryInfo.GetFiles("*.*");

                foreach (var file in allFiles)
                {
                    if (file.Name.Contains(SQLiteBD.ExecuteQueryWithAnswer($"SELECT pathToSprite FROM Items WHERE itemID = {currentID}")))
                        StartCoroutine(LoadItemIcon(file, i));
                }
#endif
#if UNITY_STANDALONE
                                DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);                
#endif
#if UNITY_ANDROID
                //DirectoryInfo direcrotyInfo = new DirectoryInfo(Application.persistentDataPath);
                //DirectoryInfo direcrotyInfo = new DirectoryInfo()
                StartCoroutine(LoadItemIcon(Application.streamingAssetsPath + "/ItemsIcons", i, currentID));
#endif

                int playerCount = int.Parse(SQLiteBD.ExecuteQueryWithAnswer($"SELECT ItemCount FROM PlayersItems WHERE itemId = {currentID} AND playerId = {GameController.PlayerID}"));
                TextMeshProUGUI childTMP = child.GetChild(1).GetComponent<TextMeshProUGUI>();

                //set text color
                if (playerCount >= currentRecipe.RecipesCountItems[i])
                    childTMP.color = Color.green;
                else
                    childTMP.color = Color.red;
                    childTMP.text = $"" +
                        $"{playerCount}" +
                        $"/{currentRecipe.RecipesCountItems[i]}";
                child.gameObject.SetActive(true);
            }
        }
        else Debug.LogError("Not have a recipe");

        transform.GetChild(6).GetComponent<Button>().interactable = false;
    }

    IEnumerator LoadItemIcon(string uri, int childNumber, int currentID)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture($"{uri}/{SQLiteBD.ExecuteQueryWithAnswer($"SELECT pathToSprite FROM Items WHERE itemID = {currentID}")}"))
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
                transform.GetChild(childNumber).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            }
        }
        yield break;
    }
    IEnumerator LoadItemIcon(FileInfo playerFile, int childNumber)
    {
        if (playerFile.Name.Contains("meta"))
        {
            yield break;
        }
        else
        {
            var wwwPlayerFilePath = "file://" + playerFile.FullName.ToString();
            WWW www = new WWW(wwwPlayerFilePath);
            yield return www;

            transform.GetChild(childNumber).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(
                www.texture,
                new Rect(0, 0, www.texture.width, www.texture.height),
            new Vector2(0.5f, 0.5f));
        }
    }
}
