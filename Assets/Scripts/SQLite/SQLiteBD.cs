using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

static class SQLiteBD
{
    private const string fileName = "workshop.bytes";
    private static string DBPath;
    private static SqliteConnection connection;
    private static SqliteCommand command;

    static SQLiteBD()
    {
        DBPath = GetDatabasePath();
    }
    private static string GetDatabasePath()
    {
#if UNITY_EDITOR
        return Path.Combine(Application.streamingAssetsPath, fileName);
#endif
#if UNITY_STANDALONE
        string filePath = Path.Combine(Application.dataPath, fileName);
        if (!File.Exists(filePath)) UnpackDatabase(filePath);
        return filePath;
#endif
#if UNITY_ANDROID
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(filePath)) UnpackDatabase(filePath);
        //Debug.Log(filePath);
        return filePath;
#endif
    }

    private static void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);
    }


    private static void OpenConnection()
    {
        connection = new SqliteConnection("Data Source=" + DBPath);
        command = new SqliteCommand(connection);
        connection.Open();
    }

    private static void CloseConnection()
    {
        connection.Close();
        command.Dispose();
    }

    public static void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        command.CommandText = query;
        command.ExecuteNonQuery();
        CloseConnection();
    }

    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        command.CommandText = query;
        var anser = command.ExecuteScalar();
        CloseConnection();
        if (anser != null) return anser.ToString();
        else return null;
    }

    public static DataTable GetTable(string query)
    {
        OpenConnection();

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

        DataSet DS = new DataSet();
        adapter.Fill(DS);
        adapter.Dispose();
        CloseConnection();
        return DS.Tables[0];

    }
}
