using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;

public class SQLmanager : MonoBehaviour
{

    public static string LoginAddress;
    public void Awake()
    {
        LoginAddress = "/DefenseGameDB.db";
        StartCoroutine(DBCreate(LoginAddress));
    }

    void Start()
    {
        DBConnectionCheck(LoginAddress);
    }

    IEnumerator DBCreate(string TestRoot)
    {
        string filepath = string.Empty;

        if (Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + TestRoot;
            if (!File.Exists(filepath))
            {
                UnityWebRequest unityWebRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets" + TestRoot);
                unityWebRequest.downloadedBytes.ToString();
                yield return unityWebRequest.SendWebRequest().isDone;
                File.WriteAllBytes(filepath, unityWebRequest.downloadHandler.data);
            }
        }
        else
        {
            filepath = Application.dataPath + TestRoot;

            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + TestRoot, filepath);
            }
        }
    }

    public string GetDBFilePath(string TestRoot)
    {
        string str = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            str = "URI=file:" + Application.persistentDataPath + TestRoot;
        }
        else
        {
            str = "URI=file:" + Application.dataPath + TestRoot;
        }
        return str;
    }

    public void DBConnectionCheck(string TestRoot)
    {
        try
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(TestRoot));
            dbConnection.Open(); //DB열기

            if (dbConnection.State == ConnectionState.Open)
            {
                Debug.Log("연결 성공");
            }
            else
            {
                Debug.Log("연결 실패");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void DBAllOrder(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(LoginAddress));
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }

    public int DBReadOneInt(string query, string TestRoot)
    {
        int ReturnInt = 0;
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(TestRoot));
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            ReturnInt = dataReader.GetInt32(0);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        return ReturnInt;
    }

    public int SimpleDBReadIntOne(string What, Define.DBTableName Where, string KEYCODE_NAME, string KeyCode)
    {
        string _where = Where.ToString();
        int IntReturn = DBReadOneInt($"Select {What} From {_where} Where {KEYCODE_NAME} = '{KeyCode}'", LoginAddress);
        return IntReturn;
    }

    public string DBReadOneString(string query, string TestRoot)
    {
        string Return1String = "";
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(TestRoot));
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            Return1String = dataReader.GetString(0);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        return Return1String;
    }

    public string SimpleDBReadStringOne(string What, Define.DBTableName Where, string KEYCODE_NAME, string KeyCode)
    {
        string _where = Where.ToString();
        string StringReturn = DBReadOneString($"Select {What} From {_where} Where {KEYCODE_NAME} = '{KeyCode}'", LoginAddress);
        return StringReturn;
    }
    
    public float DBReadFloatOne(string query, string TestRoot)
    {
        float ReturnInt = 0;
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(TestRoot));
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            ReturnInt = dataReader.GetFloat(0);
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        return ReturnInt;
    }

    public float SimpleDBReadFloatOne(string What, Define.DBTableName Where, string KEYCODE_NAME, string KeyCode)
    {
        string _where = Where.ToString();
        float floatReturn = DBReadFloatOne($"Select {What} From {_where} Where {KEYCODE_NAME} = '{KeyCode}'", LoginAddress);
        return floatReturn;
    }

    public int DBCount(string query, string TestRoot)
    {
        int Num = 0;
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(TestRoot));
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            Num += 1;
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        return Num;
    }

    public int SimpleDBCount(Define.DBTableName Where, string KEYCODE_NAME, string KeyCode)
    {
        string _where = Where.ToString();
        int ChedkDBCount = DBCount($"Select * From {_where} Where {KEYCODE_NAME} = '{KeyCode}'", LoginAddress);
        return ChedkDBCount;
    }

    public List<string> DBStringList(string query, string TestRoot)
    {
        List<string> ListDBCount = new List<string> { };
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(TestRoot));
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();
        while (dataReader.Read())
        {
            ListDBCount.Add(dataReader.GetString(0));
        }
        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
        return ListDBCount;
    }

    public List<string> SimpleDBStringList(Define.DBTableName Where, string KEYCODE_NAME, string KeyCode)
    {
        string _where = Where.ToString();
        List<string> ListDBCount = DBStringList($"Select * From {_where} Where {KEYCODE_NAME} = '{KeyCode}'", LoginAddress);
        return ListDBCount;
    }

    public void DBUpdateOne(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath(LoginAddress));
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }

    public void SimpleDBUpdateOne(Define.DBTableName Where, string What, string To, string KeyCode)
    {
        string _where = Where.ToString();
        DBUpdateOne($"UPDATE {_where} SET {What} = {To} WHERE KEYCODE = '{KeyCode}'");
    }

    public void SimpleDBUpdateOne(Define.DBTableName Where, string What, string To, string KEYCODE_NAME, string KeyCode)
    {
        string _where = Where.ToString();
        DBUpdateOne($"UPDATE {_where} SET {What} = {To} WHERE {KEYCODE_NAME} = '{KeyCode}'");
    }
}