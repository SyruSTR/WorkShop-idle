using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using static Auth;

public static class TestAPI
{

    private static string token;
    private static readonly Uri ApiServerUri = new Uri("http://localhost:5000");
    private static readonly Uri AuthServerUri = new Uri("http://localhost:5005");
    public static bool IsHasToken { get { return !(token == string.Empty || token == null); } }


    public static bool GetTokenInDB()
    {
        var DBtoken = SQLiteBD.ExecuteQueryWithAnswer($"SELECT token FROM players where id = {GameController.PlayerID}");
        if (!(DBtoken == string.Empty || DBtoken == null))
        {
            token = DBtoken;
            return true;
        }
        return false;
    }
    public static async Task<ItemsFromServer<MarketItem>> GetPlayerItems()
    {
        if (IsHasToken)
        {
            var items = new ItemsFromServer<MarketItem>();
            try
            {
                using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
                {
                    Uri uri = new Uri(ApiServerUri, "api/market/player");
                    var requsets = new HttpRequestMessage(HttpMethod.Get, uri);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage responce = await client.GetAsync(uri);
                    if (responce.IsSuccessStatusCode)
                    {
                        items.AddItems(
                            JsonConvert.DeserializeObject
                            <MarketItem[]>(
                            Encoding.ASCII.GetString(
                                await responce.Content.ReadAsByteArrayAsync())));

                        items.authCode = AuthCode.Sucsess;
                        return items;
                    }
                    else if (responce.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        items.authCode = AuthCode.NoAuth;
                        return items;
                    }
                    items.authCode = AuthCode.NotConnect;
                    return items;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("GetItems async: " + ex.Message);
                items.authCode = AuthCode.NotConnect;
                throw;
            }
        }
        else return null;
    }
    public static async Task<ItemsFromServer<MarketItem>> GetMarketItems()
    {
        if (IsHasToken)
        {
            var items = new ItemsFromServer<MarketItem>();
            try
            {
                using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
                {
                    Uri uri = new Uri(ApiServerUri,"api/market");
                    var request = new HttpRequestMessage(HttpMethod.Get, uri);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage responce = await client.GetAsync(uri);

                    if (responce.IsSuccessStatusCode)
                    {
                        items.AddItems(
                            JsonConvert.DeserializeObject
                            <MarketItem[]>(
                            Encoding.ASCII.GetString(
                                await responce.Content.ReadAsByteArrayAsync())));

                        items.authCode = AuthCode.Sucsess;
                        return items;
                    }
                    else if (responce.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        items.authCode = AuthCode.NoAuth;
                        return items;
                    }
                    items.authCode = AuthCode.NotConnect;
                    return items;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("GetItems async: " + ex.Message);
                items.authCode = AuthCode.NotConnect;
                return items;
                throw;
            }
        }
        return null;
    }
    public static async Task<AuthCode> PostToken(string login, string password)
    {
        try
        {
            using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
            {
                Uri uri = new Uri(AuthServerUri,"connect/token");
                var request = new HttpRequestMessage(HttpMethod.Post, uri);

                Dictionary<string, string> values = new Dictionary<string, string>();

                values = new Dictionary<string, string>
                {
                    {"client_id","company-employee" },
                    {"client_secret","codemazesecret" },
                    {"grant_type", "password" },
                    {"username",login },
                    {"password",password }
                };
                request.Content = new FormUrlEncodedContent(values);
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                HttpResponseMessage responce = await client.PostAsync(uri, request.Content);

                if (responce.IsSuccessStatusCode)
                {
                    byte[] result = await responce.Content.ReadAsByteArrayAsync();
                    token = JsonConvert.DeserializeObject
                        <Token>(Encoding.ASCII.GetString(result))
                        .access_token;
                    SQLiteBD.ExecuteQueryWithoutAnswer($"UPDATE players SET token = '{token}' WHERE id = {GameController.PlayerID}");
                    return AuthCode.Sucsess;
                }
                return AuthCode.ErrorLoginAndPass;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
            return AuthCode.NotConnect;
            throw;
        }

    }
    public class ItemsFromServer<T>
    {
        public ItemsFromServer()
        {

        }
        public void AddItems(T[] items)
        {
            Items = items;
            authCode = 0;
        }
        public AuthCode authCode;
        public T[] Items;
    }
    public enum AuthCode
    {
        NoAction,
        NotConnect,
        ErrorLoginAndPass,
        NoAuth,
        Sucsess
    }
    private class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }
}
