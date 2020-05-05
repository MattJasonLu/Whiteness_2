using UnityEngine;
using System.Collections;
using System.IO;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;

public class SQLiteUtil : MonoBehaviour
{
    public static SQLiteUtil _instance;

    /// <summary>
    /// SQLite数据库辅助类
    /// </summary>
    private SQLiteHelper sql;

    void Awake()
    {
        _instance = this;
        //创建名为sqlite4unity的数据库
        sql = new SQLiteHelper("data source=" + Application.dataPath + "/sqlite4unity.db");
    }

    void Start()
    {
        
        #region 表操作
        /*
        //创建名为table1的数据表
        sql.CreateTable("LEVELEXP_PLAYER", new string[] { "ID", "LEVEL", "EXP" }, new string[] { "INTEGER", "INTEGER", "INTEGER" });

        //插入两条数据
        for (int i = 1; i <= 100; i++)
        {
            int y = Mathf.FloorToInt(Mathf.Pow(i - 1, 3)) + 60;
            sql.InsertValues("LEVELEXP_PLAYER", new string[] { i + "", i + "", y + "" });
        }
        */

        //sql.InsertValues("table2", new string[] { "'2'", "'李四'", "'25'", "'Li4@163.com'" });

        /*//更新数据，将Name="张三"的记录中的Name改为"Zhang3"
        sql.UpdateValues("table2", new string[] { "Name" }, new string[] { "'Zhang3'" }, "Name", "=", "'张三'");

        //插入3条数据
        sql.InsertValues("table2", new string[] { "3", "'王五'", "25", "'Wang5@163.com'" });
        sql.InsertValues("table2", new string[] { "4", "'王五'", "26", "'Wang5@163.com'" });
        sql.InsertValues("table2", new string[] { "5", "'王五'", "27", "'Wang5@163.com'" });

        //删除Name="王五"且Age=26的记录,DeleteValuesOR方法类似
        sql.DeleteValuesAND("table2", new string[] { "Name", "Age" }, new string[] { "=", "=" }, new string[] { "'王五'", "'26'" });

        //读取整张表
        SqliteDataReader reader = sql.ReadFullTable("table2");
        while (reader.Read())
        {
            //读取ID
            Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
            //读取Name
            Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
            //读取Age
            Debug.Log(reader.GetInt32(reader.GetOrdinal("Age")));
            //读取Email
            Debug.Log(reader.GetString(reader.GetOrdinal("Email")));
        }

        //读取数据表中Age>=25的所有记录的ID和Name
        reader = sql.ReadTable("table1", new string[] { "ID", "Name" }, new string[] { "Age" }, new string[] { ">=" }, new string[] { "'25'" });
        while (reader.Read())
        {
            //读取ID
            Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
            //读取Name
            Debug.Log(reader.GetString(reader.GetOrdinal("Name")));
        }

        //自定义SQL,删除数据表中所有Name="王五"的记录
        sql.ExecuteQuery("DELETE FROM table1 WHERE NAME='王五'");*/
        #endregion

        //关闭数据库连接
        //sql.CloseConnection();
    }

    public Dictionary<int, int> GetPlayerLevelExpDict()
    {
        Dictionary<int, int> levelDict = new Dictionary<int, int>();
        SqliteDataReader reader = sql.ReadFullTable("LEVELEXP_PLAYER");
        while (reader.Read())
        {
            int level = reader.GetInt32(reader.GetOrdinal("LEVEL"));
            int exp = reader.GetInt32(reader.GetOrdinal("EXP"));
            levelDict.Add(level, exp);
        }
        return levelDict;
    }

    public Dictionary<int, int> GetEnemyLevelExpDict()
    {
        Dictionary<int, int> levelDict = new Dictionary<int, int>();
        SqliteDataReader reader = sql.ReadFullTable("LEVELEXP_ENEMY");
        while (reader.Read())
        {
            int level = reader.GetInt32(reader.GetOrdinal("LEVEL"));
            int exp = reader.GetInt32(reader.GetOrdinal("EXP"));
            levelDict.Add(level, exp);
        }
        return levelDict;
    }

    /// <summary>
    /// 通过角色ID获取角色基本能力值
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public RoleUnit GetRoleUnitById(string id)
    {
        RoleUnit roleUnit = new RoleUnit();
        SqliteDataReader reader = sql.ReadTable("ROLEDEF", new string[] { "NAME", "HP", "EP", "CP", "STR", 
            "DEF", "ATS", "ADF", "SPD", "DEX", "RNG", "CRT", "HIT", "ROLETYPE" }, new string[] { "ID" }, 
            new string[] { "=" }, new string[] { "'" + id + "'" });
        while (reader.Read())
        {
            roleUnit.unitName = reader.GetString(reader.GetOrdinal("NAME"));
            roleUnit.initHP = reader.GetInt32(reader.GetOrdinal("HP"));
            roleUnit.initEP = reader.GetInt32(reader.GetOrdinal("EP"));
            roleUnit.initCP = reader.GetInt32(reader.GetOrdinal("CP"));
            roleUnit.STR = reader.GetInt32(reader.GetOrdinal("STR"));
            roleUnit.DEF = reader.GetInt32(reader.GetOrdinal("DEF"));
            roleUnit.ATS = reader.GetInt32(reader.GetOrdinal("ATS"));
            roleUnit.ADF = reader.GetInt32(reader.GetOrdinal("ADF"));
            roleUnit.SPD = reader.GetInt32(reader.GetOrdinal("SPD"));
            roleUnit.DEX = reader.GetInt32(reader.GetOrdinal("DEX"));
            roleUnit.RNG = reader.GetInt32(reader.GetOrdinal("RNG"));
            roleUnit.CRT = reader.GetInt32(reader.GetOrdinal("CRT"));
            roleUnit.HIT = reader.GetInt32(reader.GetOrdinal("HIT"));
            roleUnit.roleType = reader.GetInt32(reader.GetOrdinal("ROLETYPE"));
            break;
        }
        return roleUnit;
    }

    public void Close()
    {
        //关闭数据库连接
        sql.CloseConnection();
    }
}

public class SQLiteHelper
{
    /// <summary>
    /// 数据库连接定义
    /// </summary>
    private SqliteConnection dbConnection;

    /// <summary>
    /// SQL命令定义
    /// </summary>
    private SqliteCommand dbCommand;

    /// <summary>
    /// 数据读取定义
    /// </summary>
    private SqliteDataReader dataReader;

    /// <summary>
    /// 构造函数    
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    public SQLiteHelper(string connectionString)
    {
        try
        {
            //构造数据库连接
            dbConnection = new SqliteConnection(connectionString);
            //打开数据库
            dbConnection.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// 执行SQL命令
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="queryString">SQL命令字符串</param>
    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }

    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseConnection()
    {
        //销毁Command
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;

        //销毁Reader
        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;

        //销毁Connection
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
    }

    /// <summary>
    /// 读取整张数据表
    /// </summary>
    /// <returns>The full table.</returns>
    /// <param name="tableName">数据表名称</param>
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string queryString = "SELECT * FROM " + tableName;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 向指定数据表中插入数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="values">插入的数值</param>
    public SqliteDataReader InsertValues(string tableName, string[] values)
    {
        //获取数据表中字段数目
        int fieldCount = ReadFullTable(tableName).FieldCount;
        //当插入的数据长度不等于字段数目时引发异常
        if (values.Length != fieldCount)
        {
            throw new SqliteException("values.Length!=fieldCount");
        }

        string queryString = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; i++)
        {
            queryString += ", " + values[i];
        }
        queryString += " )";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 更新指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    /// <param name="key">关键字</param>
    /// <param name="value">关键字对应的值</param>
    public SqliteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length");
        }

        string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += ", " + colNames[i] + "=" + colValues[i];
        }
        queryString += " WHERE " + key + operation + value;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += "OR " + colNames[i] + operations[0] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 创建数据表
    /// </summary> +
    /// <returns>The table.</returns>
    /// <param name="tableName">数据表名</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colTypes">字段名类型</param>
    public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
    {
        string queryString = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += ", " + colNames[i] + " " + colTypes[i];
        }
        queryString += "  ) ";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// Reads the table.
    /// </summary>
    /// <returns>The table.</returns>
    /// <param name="tableName">Table name.</param>
    /// <param name="items">Items.</param>
    /// <param name="colNames">Col names.</param>
    /// <param name="operations">Operations.</param>
    /// <param name="colValues">Col values.</param>
    public SqliteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 0; i < colNames.Length; i++)
        {
            queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
        }
        return ExecuteQuery(queryString);
    }
}