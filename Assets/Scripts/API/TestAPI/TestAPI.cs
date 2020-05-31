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

    public static bool IsHasToken { get { return !(token == string.Empty || token == null); } }

    public static async Task<MarketItem> GetMarketItems()
    {
        if (IsHasToken)
        {
            try
            {
                using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
                {
                    Uri uri = new Uri("http://localhost:5000/api/market");
                    var request = new HttpRequestMessage(HttpMethod.Get, uri);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Beared", token);

                    HttpResponseMessage responce = await client.GetAsync(uri);

                    if (responce.IsSuccessStatusCode)
                    {

                        MarketItem items = JsonConvert.DeserializeObject
                            <MarketItem>(
                            Encoding.ASCII.GetString(
                                await responce.Content.ReadAsByteArrayAsync()));
                        return items;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        return null;
    }
    public static async Task<string> GetLol()
    {
        if (token != string.Empty)
        {
            try
            {
                using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(30) })
                {
                    Uri uri = new Uri("http://localhost:5000/api/test/lol");
                    var request = new HttpRequestMessage(HttpMethod.Get, uri);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage responce = await client.GetAsync(uri);

                    if (responce.IsSuccessStatusCode)
                    {
                        return await responce.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception)
            {

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
                Uri uri = new Uri("http://localhost:5005/connect/token");
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
    private class Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }
}
